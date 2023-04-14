﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool IsMoving => _isMoving;


    [SerializeField]
    private float Speed = 8;

    private bool _isMoving;
    PlayerInput _input;
    Rigidbody2D _rigidbody;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {

        Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

        _rigidbody.velocity = direction;
        _isMoving = direction.magnitude > 0.01f;
    }
}
