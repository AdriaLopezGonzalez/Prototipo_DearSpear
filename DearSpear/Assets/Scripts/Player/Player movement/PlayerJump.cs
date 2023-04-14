using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerInput _input;
    private Rigidbody2D _rigidbody;


    //private CollisionDetected collisionDetection;

    //public ContactFilter2D filter;
    //
    //public static Action<Rigidbody2D> CheckCollision;
    //
    //CollisionDetected collisionDetected;

    private void OnEnable()
    {
        PlayerInput.Jump += Jump;
    }
    
    private void OnDisable()
    {
        PlayerInput.Jump -= Jump;
    }
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //collisionDetected = gameObject.GetComponent<CollisionDetected>();
        //collisionDetection = GetComponent<CollisionDetected>();
    }

    void FixedUpdate()
    {
    }

    public void Jump(float jumpForce)
    {
        var vel = new Vector2(_rigidbody.velocity.x, jumpForce);

        _rigidbody.velocity = vel;
    }
}
