using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerState currentState;
    private PlayerMovement _movement;
    private Animator _animator;
    public PlayerCollisionDetector _groundDetector;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        UpdateState();

        switch (currentState)
        {
            case PlayerState.Running:
                _animator.SetBool("isRunning", true);
                _animator.SetBool("onAir", false);
                break;
            case PlayerState.Jumping:
                _animator.SetBool("onAir", true);
                break;
            case PlayerState.Shooting:
                break;
            case PlayerState.Climbing:
                break;
            case PlayerState.Grappling:
                break;
            case PlayerState.CloseKilling:
                break;
            case PlayerState.Dying:
                break;
            case PlayerState.Idle:
                _animator.SetBool("isRunning", false);
                _animator.SetBool("onAir", false);
                break;
            default:
                break;
        }
    }

    private void UpdateState()
    {
        if (!_groundDetector.isGrounded)
        {
            currentState = PlayerState.Jumping;
        }
        else if (_movement.IsMoving)
        {
            currentState = PlayerState.Running;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    /*public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }*/
}

public enum PlayerState
{
    Idle,
    Running,
    Jumping,
    Shooting,
    Climbing,
    Grappling,
    CloseKilling,
    Dying,
}
