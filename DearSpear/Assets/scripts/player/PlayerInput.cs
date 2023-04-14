using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float MovementHorizontal { get; private set; }
    public float Jump { get; private set; }

    //void Start()
    //{
    //    collisionDetected = gameObject.GetComponent<CollisionDetected>();
    //}

    void Update()
    {
        MovementHorizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");

        //if (Input.GetKeyDown(KeyCode.Space) && (collisionDetected.IsGrounded || collisionDetected.IsTouchingRoof))
        //{
        //    OnJumpStarted?.Invoke(this);
        //}
        //
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    OnJumpFinished?.Invoke(this);
        //}
    }
}

