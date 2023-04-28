using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
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

    void Start()
    {
        playerCollisionDetector = gameObject.GetComponentInChildren<PlayerCollisionDetector>();
    }

    void Update()
    {
        MovementVertical = Input.GetAxis("Vertical");
        MovementHorizontal = Input.GetAxis("Horizontal");


        
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

    public void AimSpear(InputAction.CallbackContext context)
    {

    }

}

