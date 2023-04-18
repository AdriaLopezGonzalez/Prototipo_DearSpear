using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float MovementHorizontal { get; private set; }
    public float MovementVertical { get; private set; }

    [SerializeField]
    private float jumpForceStart = 10;

    private float jumpForce;
    [SerializeField]
    private float JumpForceAddition = 15;

    [SerializeField]
    private const float maxJumpForce = 25;

    private PlayerCollisionDetector playerCollisionDetector;

    public static Action<float> Jump;
    public static Action SetRope;
    public static Action EndRope;

    public static Action LaunchSpear;

    public static Action DrawPoints;
    public static Action ErasePoints;

    void Start()
    {
        playerCollisionDetector = gameObject.GetComponentInChildren<PlayerCollisionDetector>();

        jumpForce = jumpForceStart;
    }

    void Update()
    {
        MovementVertical = Input.GetAxis("Vertical");
        MovementHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && playerCollisionDetector.isGrounded)
        {
            jumpForce += JumpForceAddition * Time.deltaTime;
        }
        
        if (Input.GetKeyUp(KeyCode.Space) && playerCollisionDetector.isGrounded)
        {
            if(jumpForce > maxJumpForce)
            {
                jumpForce = maxJumpForce;
            }

            Jump?.Invoke(jumpForce);

            jumpForce = jumpForceStart;

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

        if (Input.GetMouseButton(0))
        {
            DrawPoints?.Invoke();
        }
        if (Input.GetMouseButtonUp(0))
        {
            LaunchSpear?.Invoke();
            ErasePoints?.Invoke();
        }
    }
}

