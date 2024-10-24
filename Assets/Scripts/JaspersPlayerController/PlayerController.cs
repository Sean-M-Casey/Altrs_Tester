using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private Rigidbody rb;

    #region Camera Control Variables

    public Camera playerCamera;

    public bool invertYLook = false;
    public bool invertXLook = false;

    public float lookSensitivity = 2f;
    public float maxLookAngle = 50f;

    public bool lockCursor = true;
    public bool crosshairEnabled = true;
    public Sprite crosshairImage;


    private bool cameraMoveLocked = false;
    private float cameraYaw = 0f;
    private float cameraPitch = 0f;
    private Image crosshairObject;

    #endregion

    #region Movement Variables

    public float baseWalkSpeed = 5f;
    public float maxAcceleration = 10f;
    public float speedReduction = .5f;

    private bool playerMoveLocked = false;
    private bool isWalking = false;
    private bool isGrounded = true;


    public float jumpStrength = 5f;

    #endregion

    #region Control Delegates

    public delegate void UINavigateRequest(Vector3 direction);
    public delegate void UINavigateSelect();
    public delegate void UINavigateCancel();

    public UINavigateRequest navigateRequest;
    public UINavigateSelect navigateSelect;
    public UINavigateCancel navigateCancel;

    #endregion

    #region Grabbed Input Variables

    private Vector3 inputVelocity;
    private Vector2 inputCameraLook;

    #endregion

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerInputActions.Enable(); 
        playerInputActions.PlayerOnFoot.Look.started += OnLook;
        //playerInputActions.PlayerOnFoot.Look.performed += OnLook;
        playerInputActions.PlayerOnFoot.Look.canceled += OnLook;
        playerInputActions.PlayerOnFoot.Move.performed += OnMove;
        playerInputActions.PlayerOnFoot.Move.canceled += OnMove;
        //playerInputActions.PlayerOnFoot.QuickhackMenuActive.started += OnQuickhackMenuActive;
        playerInputActions.PlayerOnFoot.QuickhackMenuActive.performed += OnQuickhackMenuActive;
        playerInputActions.PlayerOnFoot.QuickhackMenuActive.canceled += OnQuickhackMenuActive;

        playerInputActions.PlayerOnFoot.QuickhackMenuNavigate.performed += OnQuickhackMenuNavigate;
        playerInputActions.PlayerOnFoot.QuickhackMenuNavigate.canceled += OnQuickhackMenuNavigate;

        playerInputActions.PlayerOnFoot.QuickhackMenuSelect.started += OnQuickhackMenuSelect;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
        playerInputActions.PlayerOnFoot.Look.started -= OnLook;
        //playerInputActions.PlayerOnFoot.Look.performed -= OnLook;
        playerInputActions.PlayerOnFoot.Look.canceled -= OnLook;
        playerInputActions.PlayerOnFoot.Move.performed -= OnMove;
        playerInputActions.PlayerOnFoot.Move.canceled -= OnMove;
        //playerInputActions.PlayerOnFoot.QuickhackMenuActive.started -= OnQuickhackMenuActive;
        playerInputActions.PlayerOnFoot.QuickhackMenuActive.performed -= OnQuickhackMenuActive;
        playerInputActions.PlayerOnFoot.QuickhackMenuActive.canceled -= OnQuickhackMenuActive;

        playerInputActions.PlayerOnFoot.QuickhackMenuNavigate.performed -= OnQuickhackMenuNavigate;
        playerInputActions.PlayerOnFoot.QuickhackMenuNavigate.canceled -= OnQuickhackMenuNavigate;

        playerInputActions.PlayerOnFoot.QuickhackMenuSelect.started -= OnQuickhackMenuSelect;
    }


    private void Update()
    {
        if(cameraMoveLocked == false)
        {
            if(invertXLook == true) //For whatever reason
            {
                cameraYaw = transform.localEulerAngles.y - inputCameraLook.x * lookSensitivity;
            }
            else
            {
                cameraYaw = transform.localEulerAngles.y + inputCameraLook.x * lookSensitivity;
            }

            if(invertYLook == true)
            {
                cameraPitch += lookSensitivity * inputCameraLook.y;
            }
            else
            {
                cameraPitch -= lookSensitivity * inputCameraLook.y;
            }

            cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, cameraYaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
        }

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        if(playerMoveLocked == false)
        {
            Vector3 targetVelocity = inputVelocity;

            if((targetVelocity.x != 0 || targetVelocity.z != 0) && isGrounded == true)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }

            targetVelocity = transform.TransformDirection(targetVelocity) * baseWalkSpeed;

            Vector3 velocity = rb.velocity;
            Vector3 velocityDiff = targetVelocity - velocity;

            velocityDiff.x = Mathf.Clamp(velocityDiff.x, -maxAcceleration, maxAcceleration);
            velocityDiff.z = Mathf.Clamp(velocityDiff.z, -maxAcceleration, maxAcceleration);
            velocityDiff.y = 0;

            rb.AddForce(velocityDiff, ForceMode.VelocityChange);
        }
    }

    #region Control Receiver Methods

    private void OnLook(InputAction.CallbackContext ctx)
    {
        if(cameraMoveLocked == false)
        {
            inputCameraLook = ctx.ReadValue<Vector2>();

            Debug.Log($"QUICKHICK: Look value is {inputCameraLook}");

            
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        if(playerMoveLocked == false)
        {
            Vector2 grabbedInput = ctx.ReadValue<Vector2>();

            inputVelocity = new Vector3(grabbedInput.x, 0, grabbedInput.y);

            Debug.Log($"QUICKHICK: Move value is {grabbedInput}");
        }
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if(isGrounded == true)
        {
            rb.AddForce(0f, jumpStrength, 0f, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnQuickhackMenuActive(InputAction.CallbackContext ctx)
    {
        Debug.Log($"QUICKHICK: QuickhackMenuActive stage is {ctx.phase}");

        switch(ctx.phase)
        {
            case InputActionPhase.Performed:

                HackingSystem.instance.hackScreenInput = true;

                break;
            case InputActionPhase.Canceled:

                HackingSystem.instance.hackScreenInput = false;

                break;
        }
    }

    private void OnQuickhackMenuNavigate(InputAction.CallbackContext ctx)
    {
        Vector3 scrollNav = ctx.ReadValue<Vector2>();

        navigateRequest.Invoke(scrollNav);

        Debug.Log($"QUICKHICK: QuickhackMenuNavigate value is {scrollNav}");
    }

    private void OnQuickhackMenuSelect(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) navigateSelect.Invoke();

        Debug.Log($"QUICKHICK: QuickhackMenuSelect called with stage {ctx.phase}");
    }

    private void OnQuickhackMenuCancel(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started) navigateCancel.Invoke();
    }

    #endregion



    private void CheckIfGrounded()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if(Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void TogglePlayerMoveLock()
    {
        playerMoveLocked = !playerMoveLocked;
    }

    public void TogglePlayerMoveLock(bool setLock)
    {
        playerMoveLocked = setLock;
    }

    public void ToggleCameraLookLock()
    {
        cameraMoveLocked = !cameraMoveLocked;
    }

    public void ToggleCameraLookLock(bool setLock)
    {
        cameraMoveLocked = setLock;
    }
}
