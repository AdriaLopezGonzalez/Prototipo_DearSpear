using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class PlayerInputs : MonoBehaviour
{

    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }

    private PlayerCollisionDetector playerCollisionDetector;

    public static Action Jump;
    public static Action SetRope;
    public static Action EndRope;

    public static Action LaunchSpear;

    public static Action ErasePoints;
    public static Action IsAiming;

    public static Action KillEnemy;
    public static Action ActivateMenu; 

    public static Func<bool> CheckEnemyDistance;
    public static Func<bool> CheckPlayerHasSpear;

    [SerializeField]
    private InputActionReference pointerPosition;

    private PlayerControls _playerControls;
    [SerializeField]
    private PlayerInput _playerInput;

    public Vector2 AimSpearPosition;

    public bool usingController;

    [SerializeField]
    private Transform twistPoint;
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();

        _playerInput = GetComponent<PlayerInput>();
        playerCollisionDetector = gameObject.GetComponentInChildren<PlayerCollisionDetector>();
    }

    void Update()
    {
        MovementVertical = Input.GetAxis("Vertical");
        MovementHorizontal = Input.GetAxis("Horizontal");

        AimSpearPosition = AimSpear();

    }

    public void UseJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerCollisionDetector.isGrounded)
            {
                Jump?.Invoke();

                playerCollisionDetector.isGrounded = false;
            }
        }
    }

    public void UseRope(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetRope?.Invoke();
        }
        
        if (context.canceled)
        {
            EndRope?.Invoke();
        }
    }

    public void UseSpear(InputAction.CallbackContext context)
    {
        //CHECKEAR SI TENEMOS LANZA, SI NO TENEMOS NO HACE NA
        if (context.performed)
        {
            IsAiming?.Invoke();
        }
        else
        {
            ErasePoints?.Invoke();
            if (context.canceled)
            {
                if (CheckEnemyDistance() && CheckPlayerHasSpear())
                {
                    KillEnemy?.Invoke();
                }
                else
                {
                    LaunchSpear?.Invoke();
                    ErasePoints?.Invoke();
                }
            }
        }
    }

    public Vector2 AimSpear()
    {
        if (usingController)
        {
            float horizontalAxis = Input.GetAxis("HorizontalAim");
            float verticalAxis = Input.GetAxis("VerticalAim");
            /*Vector3 angle = twistPoint.transform.localEulerAngles;

            if(horizontalAxis == 0.0f && verticalAxis == 0.0f)
            {
                Vector3 currentRotation = twistPoint.transform.localEulerAngles;
                Vector3 homeRotation;

                if(currentRotation.z > 100f)
                {
                    homeRotation = new Vector3(0, 0, 359.999f);
                }
                else
                {
                    homeRotation = Vector3.zero;
                }

                twistPoint.transform.localEulerAngles = Vector3.Slerp(currentRotation, homeRotation, Time.deltaTime * 0.8f);
            }
            else
            {
                twistPoint.transform.localEulerAngles = new Vector3(0, 0, -Mathf.Atan2(horizontalAxis, verticalAxis) * Mathf.Rad2Deg + 90f);
            }*/
            float m_JoystickDistance = 20.0f;
            Vector2 l_Direction = new Vector2(horizontalAxis, verticalAxis);
            l_Direction.Normalize();
            Vector2 l_Position = twistPoint.position;

            return l_Position + l_Direction * m_JoystickDistance;
        }
        else
        {
            Vector2 mousePos = pointerPosition.action.ReadValue<Vector2>();

            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        usingController = pi.currentControlScheme.Equals("Controller") ? true : false;
    }

    public void FreezePlayer()
    {
        MovementHorizontal = 0;
        MovementVertical = 0;
    }

    public void ActivatePauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ActivateMenu?.Invoke();
        }
    }

    public void Teleport1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            transform.position = new Vector2(90.5f,1.7f);
        }
    }

    public void Teleport2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            transform.position = new Vector2(189f, 3.4f);
        }
    }

    public void Teleport3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            transform.position = new Vector2(252f, 8.4f);
        }
    }

    public void Teleport4(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            transform.position = new Vector2(370f, 22.3f);
        }
    }

    public void Teleport5(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            transform.position = new Vector2(456f, 27.4f);
        }
    }
}

