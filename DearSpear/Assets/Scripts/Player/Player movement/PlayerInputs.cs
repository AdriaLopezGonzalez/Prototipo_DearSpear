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
            //aimingInputs = _playerControls.Gameplay.Aim.ReadValue<Vector2>();
            //if(Mathf.Abs(aimingInputs.x) > 0.1f || Mathf.Abs(aimingInputs.y) > 0.1f)
            //{
            //    Vector3 playerAimingDirection = Vector3.right * aimingInputs.x + Vector3.forward * aimingInputs.y;
            //    if(playerAimingDirection.sqrMagnitude > 0.0f)
            //    {
            //        Quaternion newRotation = Quaternion.LookRotation(playerAimingDirection, Vector3.up);
            //        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 1000f * Time.deltaTime);
            //    }
            //}
            Vector2 mousePos = pointerPosition.action.ReadValue<Vector2>();

            return Camera.main.ScreenToWorldPoint(mousePos);
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
}

