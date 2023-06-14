using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrientation : MonoBehaviour
{
    protected Camera playerCamera;

    protected Orientation camOrientation;

    protected Vector3 camAngle = Vector3.zero;
    protected Vector3 camPos = Vector3.zero;

    public Orientation CamOrientation { get { return camOrientation; } }

    public Vector2 CameraLookAngles { get { return camAngle; } }
    public Vector3 CameraPosition { get { return camPos; } }

    private void Awake()
    {
        playerCamera = GetComponent<FirstPersonController>().playerCamera;
        camAngle = playerCamera.transform.rotation.eulerAngles;
        camPos = playerCamera.transform.position;
        HelperMethods.ClampAngle(ref camAngle);
    }

    private void LateUpdate()
    {
        camAngle = playerCamera.transform.rotation.eulerAngles;
        camPos = playerCamera.transform.position;

        HelperMethods.ClampAngle(ref camAngle);

        HelperMethods.FindOrientation(CameraLookAngles, ref camOrientation);

        Debug.Log($"INCAM Camera Orientation is {camOrientation}.");
    }
}
