using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HackingMain : MonoBehaviour
{
    public static HackingMain instance;

    public delegate void UpdateHackScreen();

    public delegate void OnFacingDirectionChanged(ScreenPosH screenPosH, ScreenPosV screenPosV);

    public UpdateHackScreen updateHackScreen;
    public UpdateHackScreen closeHackScreen;

    public OnFacingDirectionChanged onFacingDirectionChanged;

    [SerializeField] private Camera fpsCamera;

    [SerializeField] private float hackRayDistance;

    [SerializeField] private HackingScreen hackingScreenPrefab;

    [SerializeField] private HackExec hackExecPrefab;

    public List<HackExec> hackExecs = new List<HackExec>();
    
    private bool hacksOpen = false;

    private HackableObject hackTarget;

    public Camera FPSCamera { get { return fpsCamera; } }
    public HackableObject HackTarget { get { return hackTarget; } }
    public HackingScreen HackingScreenPrefab { get { return hackingScreenPrefab; } }
    public HackExec HackExecPrefab { get { return hackExecPrefab; } }

    private Coroutine openCoroutine;

    RaycastHit hit;

    [SerializeField] bool toggleHacking = false;

    [Range(-180, 180)]public float verticalAngle;
    [Range(-180, 180)] public float horizontalAngle;

    Selectable selected;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {

        //Toggles Hacking State
        if (Input.GetKeyDown(KeyCode.Tab)) toggleHacking = !toggleHacking;

        if (toggleHacking == true)
        {
            Debug.Log("HACKTEST: ToggleActive");

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hit, hackRayDistance))
            {
                Debug.Log("HACKTEST: Ray is Cast");
                if (hit.collider.gameObject.GetComponent<HackableObject>() is IHackable)
                {
                    if (hackTarget == null || hackTarget != hit.collider.gameObject.GetComponent<HackableObject>())
                    {
                        if (openCoroutine == null) openCoroutine = StartCoroutine(ScanHackable(0.5f));
                    }



                }
                else 
                {
                    if (openCoroutine != null) 
                    {
                        StopCoroutine(openCoroutine); 
                        openCoroutine = null;
                    }
                }

            }

        }
        else if(hacksOpen)
        {
            Debug.Log("HACKTEST: HacksOpen WOOOOO");
            closeHackScreen?.Invoke();
            hacksOpen = false;
            hackTarget = null;
        }

    }

    /// <summary>Buffers opening of Hack Screen for a new Hackable.</summary>
    /// <param name="scanTime">Time to open Hackable's Screen.</param>
    /// <returns>Returns a coroutine.</returns>
    public IEnumerator ScanHackable(float scanTime)
    {
        hacksOpen = true;

        if(hackTarget == null)
        {
            updateHackScreen += hit.collider.gameObject.GetComponent<HackableObject>().OnHackingStart;
            Debug.Log("HACKTEST: Added OnHackingStart");
        }
        else if(hackTarget != hit.collider.gameObject.GetComponent<HackableObject>())
        {
            updateHackScreen += hackTarget.OnHackingFinish;
            updateHackScreen += hit.collider.gameObject.GetComponent<HackableObject>().OnHackingStart;
            Debug.Log("HACKTEST: Added OnHackingFinish and OnHackingStart");
        }

        yield return new WaitForSecondsRealtime(scanTime);

        hackTarget = hit.collider.gameObject.GetComponent<HackableObject>();
        updateHackScreen?.Invoke();
    }

    ScreenPosH CheckHorizontalDirectionChange(HackableObject target)
    {
        Vector3 directionFromTarget = (target.transform.position - fpsCamera.transform.position).normalized;

        Vector3 flattenedDirection = new Vector3(directionFromTarget.x, 0, directionFromTarget.z).normalized;
        Vector3 flattenedCamDirection = new Vector3(fpsCamera.transform.forward.x, 0, fpsCamera.transform.forward.z).normalized;

        Debug.Log($"HACKTEST: Horizontal FlatDir is {directionFromTarget}");
        Debug.Log($"HACKTEST: Horizontal FlatCamDir is {flattenedCamDirection}");

        horizontalAngle = Vector3.SignedAngle(directionFromTarget, flattenedCamDirection, Vector3.up);

        Debug.Log($"HACKTEST: Horizontal Angle is {horizontalAngle}");
        if (horizontalAngle > 10)
        {
            return ScreenPosH.Right;
        }
        else if (horizontalAngle < -10)
        {
            return ScreenPosH.Left;
        }
        else return target.ScreenInstance.ScreenPosH;

    }

    ScreenPosV CheckVerticalDirectionChange(HackableObject target)
    {
        Vector3 directionFromTarget = (fpsCamera.transform.position - target.transform.position).normalized;

        Vector3 flattenedDirection = new Vector3(0, directionFromTarget.y, directionFromTarget.z).normalized;
        Vector3 flattenedCamDirection = new Vector3(0, fpsCamera.transform.forward.y, fpsCamera.transform.forward.z).normalized;

        Debug.Log($"HACKTEST: Vertical FlatDir is {directionFromTarget}");
        Debug.Log($"HACKTEST: Vertical FlatCamDir is {flattenedCamDirection}");

        verticalAngle = Vector3.SignedAngle(directionFromTarget, flattenedCamDirection, Vector3.right);

        Debug.Log($"HACKTEST: Vertical Angle is {verticalAngle}");

        if (verticalAngle > 10)
        {
            return ScreenPosV.Bottom;
        }
        else if (verticalAngle < -10)
        {
            return ScreenPosV.Top;
        }
        else return target.ScreenInstance.ScreenPosV;

    }

    public void CheckIfDirectionChanged(HackableObject target, ScreenPosH checkPosH, ScreenPosV checkPosV)
    {
        Debug.Log($"HACKTEST: Horizontal Direction is {checkPosH}");
        Debug.Log($"HACKTEST: Vertical Direction is {checkPosV}");

        if (target.ScreenInstance.ScreenPosH != checkPosH || target.ScreenInstance.ScreenPosV != checkPosV)
        {
            onFacingDirectionChanged(checkPosH, checkPosV);
        }
    }


}

