using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenPosH { Left, Centre, Right }
public enum ScreenPosV { Top, Centre, Bottom }
public class HackingScreen : MonoBehaviour
{
    [HideInInspector] public HackableObject parent;
    [SerializeField] private GameObject hackingCanvas;

    private ScreenPosH screenPosH;
    private ScreenPosV screenPosV;

    [SerializeField] private Vector3 screenOffSet;

    private Vector3 desiredScreenPos = new Vector3(2, 0, 1);

    [SerializeField] private GameObject panel;

    public GameObject Panel { get { return panel; } }

    public ScreenPosH ScreenPosH { get { return screenPosH; } }
    public ScreenPosV ScreenPosV { get { return screenPosV; } }

    Coroutine movementCoroutine;

    private void Awake()
    {
        hackingCanvas.transform.localPosition = desiredScreenPos;
        HackingMain.instance.onFacingDirectionChanged += FindDesiredPosition;
    }

    private void OnDisable()
    {
        HackingMain.instance.onFacingDirectionChanged -= FindDesiredPosition;
    }

    private void Update()
    {
        RotateAnchor();
        RotateCanvas();
    }

    public void OpenScreen()
    {
        hackingCanvas.GetComponent<Canvas>().worldCamera = HackingMain.instance.FPSCamera.GetComponent<Camera>();
    }

    public void CloseScreen()
    {
        parent.CloseScreen();
    }

    void RotateAnchor()
    {
        transform.LookAt(HackingMain.instance.transform, Vector3.up);
    }

    void RotateCanvas()
    {
        hackingCanvas.transform.rotation = Quaternion.LookRotation(HackingMain.instance.FPSCamera.transform.forward, Vector3.up);
    }

    public void FindDesiredPosition(ScreenPosH screenPosH, ScreenPosV screenPosV)
    {
        float posH = 0f;
        float posV = 0f;

        switch(screenPosH)
        {
            case ScreenPosH.Left:

                posH = -screenOffSet.x;

                break;

            case ScreenPosH.Centre:

                posH = 0;

                break;

            case ScreenPosH.Right:

                posH = screenOffSet.x;

                break;
        }

        switch (screenPosV)
        {
            case ScreenPosV.Bottom:

                posV = -screenOffSet.y;

                break;

            case ScreenPosV.Centre:

                posV = 0;

                break;

            case ScreenPosV.Top:

                posV = screenOffSet.y;

                break;
        }

        desiredScreenPos = new Vector3 (posH, posV, desiredScreenPos.z);

        StartMove(hackingCanvas.transform, desiredScreenPos, 2f);
    }

    public void StartMove(Transform canvasTransform, Vector3 movePosition, float timeToMove)
    {
        if (movementCoroutine != null)
        { 
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
        movementCoroutine = StartCoroutine(MoveOverTime(canvasTransform, movePosition, timeToMove));
        
    }

    public IEnumerator MoveOverTime(Transform canvasTransform, Vector3 movePosition, float timeToMove)
    {
        Vector3 currentPosition = canvasTransform.localPosition;

        float interpolator = 0f;

        while (interpolator < 1)
        {
            interpolator += Time.deltaTime / timeToMove;

            canvasTransform.localPosition = Vector3.Lerp(currentPosition, movePosition, interpolator);

            yield return null;
        }
        movementCoroutine = null;
    }
}
