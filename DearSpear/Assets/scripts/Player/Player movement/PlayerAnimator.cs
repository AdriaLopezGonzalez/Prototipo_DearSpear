using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerState currentState;
    private PlayerMovement _movement;
    private SpearLauncher _launcher;
    private Animator _animator;
    public PlayerCollisionDetector _groundDetector;

    private bool spearNotShooted;

    private bool timerActive;
    private float timeToAchieve;
    private float timePast;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _launcher = GetComponentInChildren<SpearLauncher>();
    }

    private void Update()
    {
        //if (!timerActive)
        //{
            UpdateState();
        //}
        //else
        //{
            //Timer();
        //}

        switch (currentState)
        {
            case PlayerState.RunningWithSpear:
                _animator.SetBool("isRunning", true);
                _animator.SetBool("onAir", false);
                _animator.SetBool("isThrowing", false);
                break;
            case PlayerState.Jumping:
                _animator.SetBool("onAir", true);
                _animator.SetBool("isThrowing", false);
                break;
            case PlayerState.Shooting:
                _animator.SetTrigger("Throw");
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
                _animator.SetBool("isThrowing", false);
                break;
            default:
                break;
        }

        if (_launcher.spearActive)
        {
            spearNotShooted = true;
        }
    }

    private void UpdateState()
    {
        if (!_launcher.spearActive && spearNotShooted)
        {
            Debug.Log("La hago va");
            spearNotShooted = false;
            currentState = PlayerState.Shooting;

            //timerActive = true;
            timeToAchieve = 1f;
            _animator.SetTrigger("Throw");
        }
        else if (!_groundDetector.isGrounded)
        {
            currentState = PlayerState.Jumping;
        }
        else if (_movement.IsMoving)
        {
            currentState = PlayerState.RunningWithSpear;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
    }

    /*private void Timer()
    {
        timePast += Time.deltaTime;

        if (timePast > timeToAchieve)
        {
            timerActive = false;
            timeToAchieve = 0;
        }
    }*/

    /*public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }*/
}

public enum PlayerState
{
    Idle,
    RunningWithSpear,
    RunningWithoutSpear,
    Jumping,
    Shooting,
    Climbing,
    Grappling,
    CloseKilling,
    Dying,
}
