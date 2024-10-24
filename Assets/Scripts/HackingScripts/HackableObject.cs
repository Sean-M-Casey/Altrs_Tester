using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackableObject : MonoBehaviour, IScannable, IHackable
{
    #region HackableVariables

    [SerializeField] private HackableTypes hackableType;
    [SerializeField] private List<HackScriptable> hacks = new List<HackScriptable>();

    private GameObject hackableAsGameObject;

    #endregion

    #region ScannableVariables

    [SerializeField] private float timeToScan = 0;

    [SerializeField] private bool isScannable = true;

    [SerializeField] private bool hasBeenScanned = false; //Has this object been previously scanned?

    [SerializeField] private bool isCurrentScanTarget = false;

    private Coroutine scanRoutine;

    #endregion

    #region HackableProperties

    public HackableTypes HackableType { get { return hackableType; } }

    public List<HackScriptable> Hacks { get { return hacks; } }

    public GameObject TargetAsGameObject { get { return hackableAsGameObject; } }

    #endregion

    #region ScannableProperties

    public float TimeToScan { get { return timeToScan; } }

    public bool IsScannable { get { return isScannable; } }

    public bool HasBeenScanned { get { return hasBeenScanned; } }

    public bool IsCurrentScanTarget { get { return isCurrentScanTarget; } }

    #endregion

    #region ScannableDelegates

    public delegate void DoOnScanAttempt(IScannable scannable);
    public delegate void DoOnScanCancel(IScannable scannable);
    public delegate void DoOnScanSuccess(IScannable scannable);
    public delegate void DoOnScanEnd(IScannable scannable);

    public DoOnScanAttempt OnTryScan;
    public DoOnScanCancel OnCancelScan;
    public DoOnScanSuccess OnSuccessScan;
    public DoOnScanEnd OnEndScan;

    #endregion


    private void Awake()
    {
        hackableAsGameObject = gameObject;
    }

    #region ScannableFunctions

    public void AttemptScan()
    {
        OnTryScan?.Invoke(this);

        scanRoutine = StartCoroutine(ScanBuffer(TimeToScan));
    }

    public void OnScanCanceled()
    {
        OnCancelScan?.Invoke(this);

        StopCoroutine(scanRoutine);
        scanRoutine = null;
    }

    public void OnScanSuccess()
    {
        if(hasBeenScanned == false) hasBeenScanned = true;

        OnSuccessScan?.Invoke(this);
    }

    public void OnScanEnded()
    {
        OnEndScan?.Invoke(this);
    }

    IEnumerator ScanBuffer(float scanTimer)
    {
        float elapsedTimer = 0;

        if(HasBeenScanned == true) elapsedTimer = scanTimer;

        while (elapsedTimer < scanTimer)
        {
            elapsedTimer += Time.deltaTime;

            yield return null;
        }

        OnScanSuccess();
    }

    #endregion

    #region HackableFunctions

    public void CheckHackable()
    {

    }

    #endregion
}
