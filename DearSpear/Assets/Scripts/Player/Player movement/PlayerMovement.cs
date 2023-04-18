﻿using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool IsMoving => _isMoving;


    [SerializeField]
    private float Speed = 8f;

    private bool _isMoving;
    private bool _canClimb;
    PlayerInput _input;
    Rigidbody2D _rigidbody;

    public static Func<bool> CheckHook;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _canClimb = gameObject.GetComponentInChildren<PlayerVineDetector>().canClimb;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (CheckHook())
        {
            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity += new Vector2(_input.MovementHorizontal * Speed / 100, 0);
            _isMoving = direction.magnitude > 0.01f;
            //MIRAR LO DEL DELTA TIME Y SI NO SE PUEDE ARREGLAR LO QUITAMO
        }
        else if (_canClimb)
        {
            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _input.MovementVertical * Speed);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 0.01f;
        }
        else
        {
            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 0.01f;
        }

    }
}
