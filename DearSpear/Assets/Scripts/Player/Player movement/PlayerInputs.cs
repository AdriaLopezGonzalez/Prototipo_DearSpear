using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
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

    public static Action KillEnemy;
    public static Func<bool> CheckEnemyDistance;

    [SerializeField]
    private InputActionReference pointerPosition;

    private PlayerControls _playerControls;
    private PlayerInput _playerInput;

    public Vector2 AimSpearPosition;

    public bool usingController;

    [SerializeField]
    private Transform twistPoint;

    public bool controlChanged;
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
        
        //if (Input.GetKeyDown(KeyCode.Space) && playerCollisionDetector.isGrounded)
        //{
        //    UseJump();
        //}
        //
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    UseRope();
        //}
        //
        //if (Input.GetKeyUp(KeyCode.Mouse1))
        //{
        //    EndRope?.Invoke();
        //}
        //
        //if (Input.GetMouseButtonDown(0))
        //{
        //    UseSpear();
        //}
    }

    public void UseJump(InputAction.CallbackContext context)
    {
        if (playerCollisionDetector.isGrounded)
        {
            Jump?.Invoke();

            playerCollisionDetector.isGrounded = false;
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
            if (CheckEnemyDistance())
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

        controlChanged = true;
    }
}

