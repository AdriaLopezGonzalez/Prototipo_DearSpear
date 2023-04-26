using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        playerCollisionDetector = gameObject.GetComponentInChildren<PlayerCollisionDetector>();
    }

    void Update()
    {
        MovementVertical = Input.GetAxis("Vertical");
        MovementHorizontal = Input.GetAxis("Horizontal");


        
        if (Input.GetKeyDown(KeyCode.Space) && playerCollisionDetector.isGrounded)
        {
            Jump?.Invoke();

            playerCollisionDetector.isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SetRope?.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            EndRope?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //CHECKEAR SI TENEMOS LANZA, SI NO TENEMOS NO HACE NA
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
}

