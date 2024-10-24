using Language.Lua;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum HackParamTypes { String, Int, Float, Vector3 }
public enum HackableTypes { Remote, Direct, Both }
public enum HackTypes { Quickhack, Deephack }
public enum HackState { Inactive, InProgress, Completed }
public enum HackResult { Success, Failure }

public enum HackTargetScreenLocation { TargetNullOrNotInView, TopLeft, TopCentreLeft, TopCentreRight, TopRight, BottomLeft, BottomCentreLeft, BottomCentreRight, BottomRight }

public class HackingSystem : MonoBehaviour
{
    #region OriginalHackSystemVars

    PlayerController controller;

    public static HackingSystem instance;

    [SerializeField] private IHackable currentTarget;

    [SerializeField] private Camera playerCamera;

    [SerializeField] private float validHackDistance;

    [SerializeField] private QuickhackScreen quickhackScreen;

    [SerializeField] private HackButton hackButtonPrefab;
    [SerializeField] private HackSubButton hackSubButtonPrefab;

    [SerializeField] private List<HackScriptable> allHacksList = new List<HackScriptable>();

    [SerializeField] private string paramFormat = "{0}: {1}";

    public IHackable CurrentTarget { get { return currentTarget; } }

    public QuickhackScreen QuickhackScreen { get { return quickhackScreen; } }

    public HackButton HackButtonPrefab { get {  return hackButtonPrefab; } }

    public HackSubButton HackSubButtonPrefab { get { return hackSubButtonPrefab; } }

    public List<HackScriptable> AllHacksList { get { return allHacksList; } }

    public string ParamFormat { get {  return paramFormat; } }

    [SerializeField] private KeyCode hackingViewKey = KeyCode.None;

    public bool hackScreenInput = false;

    private bool isHackingViewOpen = false;

    private Coroutine populateQuickhackHandle;

    private Coroutine depopulateQuickhackHandle;

    #region DELEGATES

    public delegate void OnHackingViewOpenRequest();

    public delegate void OnHackingViewCloseRequest();

    public OnHackingViewOpenRequest onHackingViewOpenRequest;

    public OnHackingViewCloseRequest onHackingViewCloseRequest;


    #endregion

    #endregion

    #region ScannableVariables

    private IScannable focussedScannable;

    [SerializeField] private float scanningRange = 0.0f;
    [SerializeField] private float remoteHackRange = 0.0f;
    [SerializeField] private float directHackRange = 0.0f;

    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;

        if (controller == null) controller = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        if (instance == null) instance = this;

        onHackingViewOpenRequest += OpenHackingView;
        onHackingViewCloseRequest += CloseHackingView;
    }

    private void OnDisable()
    {
        onHackingViewOpenRequest -= OpenHackingView;
        onHackingViewCloseRequest -= CloseHackingView;
    }

    private void Update()
    {
        if(hackScreenInput == true && isHackingViewOpen == false)
        {
            Debug.Log($"QUICKHICK: Quickhack Input is pressed");

            onHackingViewOpenRequest.Invoke();
        }
        else if(hackScreenInput == false && isHackingViewOpen == true)
        {
            Debug.Log($"QUICKHICK: Quickhack Input is not pressed");

            onHackingViewCloseRequest.Invoke();
        }

        if(isHackingViewOpen == true || CurrentTarget != null)
        {
            HackingViewBehaviours();
        }

    }

    #region ScannableMethods

    public void FocusScannable(IScannable scannableToFocus)
    {
        focussedScannable = scannableToFocus;
    }

    public void GetScannableInfo(IScannable scannable)
    {

    }

    public void OpenScanningMode()
    {

    }

    public void CloseScanningMode()
    {

    }

    #endregion

    private void OpenHackingView()
    {
        //Open Menu here
        if (populateQuickhackHandle == null) populateQuickhackHandle = StartCoroutine(PopulateQuickhackScreen());


        isHackingViewOpen = true;

        controller.navigateRequest += QuickhackScreen.NavigateHacks;

        controller.navigateSelect += QuickhackScreen.HackExecute;

    }

    private void HackingViewBehaviours()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, validHackDistance))
        {
            if (hit.collider.gameObject.TryGetComponent(out IHackable component))
            {
                if (currentTarget == null)
                {
                    currentTarget = component;

                }
                else if (currentTarget != component)
                {
                    ResetHackOnTargetChange(component);

                    currentTarget = component;
                }
            }
        }

        HackTargetScreenLocation loc = FindTargetsScreenPosition();

        Debug.Log($"Location is: {loc}");
    }

    private void ResetHackOnTargetChange(IHackable newHackable)
    {
        if (depopulateQuickhackHandle == null) depopulateQuickhackHandle = StartCoroutine(DepopulateQuickhackScreen());

        if (populateQuickhackHandle == null) populateQuickhackHandle = StartCoroutine(PopulateQuickhackScreen(newHackable));
    }

    private void CloseHackingView()
    {
        //Close Menu Here
        if (depopulateQuickhackHandle == null) depopulateQuickhackHandle = StartCoroutine(DepopulateQuickhackScreen());

        if(currentTarget != null) { currentTarget = null; }
        isHackingViewOpen = false;

        controller.navigateRequest -= QuickhackScreen.NavigateHacks;

        controller.navigateSelect -= QuickhackScreen.HackExecute;
    }

    private HackTargetScreenLocation FindTargetsScreenPosition()
    {
        if(CurrentTarget == null) return HackTargetScreenLocation.TargetNullOrNotInView;

        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(CurrentTarget.TargetAsGameObject.transform.position);

        float viewportPointX = viewportPoint.x;
        float viewportPointY = viewportPoint.y;

        Debug.Log($"Location Point: {viewportPoint}");

        if(viewportPointY >= 0f && viewportPointY < 0.5f)
        {
            if(viewportPointX >= 0f && viewportPointX < 0.35f)
            {
                return HackTargetScreenLocation.BottomLeft;
            }
            else if (viewportPointX >= 0.35f && viewportPointX < 0.50f)
            {
                return HackTargetScreenLocation.BottomCentreLeft;
            }
            else if (viewportPointX >= 0.50f && viewportPointX < 0.65f)
            {
                return HackTargetScreenLocation.BottomCentreRight;
            }
            else if (viewportPointX >= 0.65f && viewportPointX < 1f)
            {
                return HackTargetScreenLocation.BottomRight;
            }
            else
            {
                return HackTargetScreenLocation.TargetNullOrNotInView;
            }
        }
        else if(viewportPointY >= 0.5f && viewportPointY < 1f)
        {
            if (viewportPointX >= 0f && viewportPointX < 0.35f)
            {
                return HackTargetScreenLocation.TopLeft;
            }
            else if (viewportPointX >= 0.35f && viewportPointX < 0.50f)
            {
                return HackTargetScreenLocation.TopCentreLeft;
            }
            else if (viewportPointX >= 0.50f && viewportPointX < 0.65f)
            {
                return HackTargetScreenLocation.TopCentreRight;
            }
            else if (viewportPointX >= 0.65f && viewportPointX < 1f)
            {
                return HackTargetScreenLocation.TopRight;
            }
            else
            {
                return HackTargetScreenLocation.TargetNullOrNotInView;
            }
        }
        else
        {
            return HackTargetScreenLocation.TargetNullOrNotInView;
        }


        
    }

    private List<HackScriptable> GetCompatibleHacks(IHackable target)
    {
        List<HackScriptable> compatibleHacks = new List<HackScriptable>();

        foreach(HackScriptable hack in allHacksList)
        {
            if(hack.IsHackCompatible(target)) { compatibleHacks.Add(hack); }
        }

        return compatibleHacks;
    }

    private IEnumerator PopulateQuickhackScreen()
    {
        while (currentTarget == null || quickhackScreen.IsModifyingButtons == true)
        {
            yield return null;
        }

        quickhackScreen.CreateHackButtons(GetCompatibleHacks(currentTarget));
    }

    private IEnumerator PopulateQuickhackScreen(IHackable target)
    {
        while (currentTarget == null || quickhackScreen.IsModifyingButtons == true)
        {
            yield return null;
        }

        quickhackScreen.CreateHackButtons(GetCompatibleHacks(target));
    }


    private IEnumerator DepopulateQuickhackScreen()
    {
        while(quickhackScreen.HackButtons.Count == 0 || populateQuickhackHandle != null || quickhackScreen.IsModifyingButtons == true)
        {
            yield return null;
        }

        quickhackScreen.DestroyCurrentHackButtons();
    }
}


