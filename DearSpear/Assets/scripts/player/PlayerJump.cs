using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerInput _input;
    private Rigidbody2D _rigidbody;

    public float SpeedVertical; //=> movemenetController.MaxSpeed

    //private CollisionDetected collisionDetection;

    //public ContactFilter2D filter;
    //
    //public static Action<Rigidbody2D> CheckCollision;
    //
    //CollisionDetected collisionDetected;

    //private void OnEnable()
    //{
    //    PlayerInput.OnJumpStarted += OnJumpStarted;
    //    PlayerInput.OnJumpFinished += OnJumpFinished;
    //}
    //
    //private void OnDisable()
    //{
    //    PlayerInput.OnJumpStarted -= OnJumpStarted;
    //    PlayerInput.OnJumpFinished -= OnJumpFinished;
    //}
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //collisionDetected = gameObject.GetComponent<CollisionDetected>();
        //collisionDetection = GetComponent<CollisionDetected>();
    }

    void FixedUpdate()
    {
        Jump();
    }

    public void Jump()
    {
        var vel = new Vector2(_rigidbody.velocity.x, _input.Jump * SpeedVertical);

        _rigidbody.velocity = vel;
        Debug.Log("jumping" + vel);
    }

    //private float DistanceToGround()
    //{
    //
    //    RaycastHit2D[] hit = new RaycastHit2D[3];
    //    Physics2D.Raycast(transform.position, Vector2.down, filter, hit, 10);
    //    return hit[0].distance;
    //}
}
