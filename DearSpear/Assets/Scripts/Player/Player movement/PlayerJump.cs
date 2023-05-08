using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerInputs _input;
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private float jumpForce = 10;

    //private CollisionDetected collisionDetection;

    //public ContactFilter2D filter;
    //
    //public static Action<Rigidbody2D> CheckCollision;
    //
    //CollisionDetected collisionDetected;

    private void OnEnable()
    {
        PlayerInputs.Jump += Jump;
    }
    
    private void OnDisable()
    {
        PlayerInputs.Jump -= Jump;
    }
    void Start()
    {
        _input = GetComponent<PlayerInputs>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //collisionDetected = gameObject.GetComponent<CollisionDetected>();
        //collisionDetection = GetComponent<CollisionDetected>();
    }

    public void Jump()
    {
        var vel = new Vector2(_rigidbody.velocity.x, jumpForce);

        _rigidbody.velocity = vel;
    }
}
