using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float MovementHorizontal { get; private set; }

    private float jumpForce;
    [SerializeField]
    private float JumpForceAddition = 15;

    [SerializeField]
    private const float maxJumpForce = 25;

    public static Action<float> Jump;

    //void Start()
    //{
    //    collisionDetected = gameObject.GetComponent<CollisionDetected>();
    //}

    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) /*&& (collisionDetected.IsGrounded || collisionDetected.IsTouchingRoof)*/)
        {
            jumpForce += JumpForceAddition * Time.deltaTime;
            Debug.Log(jumpForce);
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(jumpForce > maxJumpForce)
            {
                jumpForce = maxJumpForce;
            }

            Jump?.Invoke(jumpForce);

            jumpForce = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }
}

