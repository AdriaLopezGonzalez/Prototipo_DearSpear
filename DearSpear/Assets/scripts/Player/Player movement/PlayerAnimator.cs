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

    private void OnEnable()
    {
        PlayerCloseKill.ActivateCloseKillAnim += ActivateCloseKill;
        EnemyPatroling.PlayerSurrender += ActivateSurrender;
    }

    private void OnDisable()
    {
        PlayerCloseKill.ActivateCloseKillAnim -= ActivateCloseKill;
        EnemyPatroling.PlayerSurrender -= ActivateSurrender;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _launcher = GetComponentInChildren<SpearLauncher>();
    }

    private void Update()
    {
        UpdateSpear();
        UpdateState();


        switch (currentState)
        {
            case PlayerState.Running:
                _animator.SetBool("isRunning", true);
                _animator.SetBool("onAir", false);
                _animator.SetBool("isClimbing", false);
                break;
            case PlayerState.Jumping:
                _animator.SetBool("onAir", true);
                _animator.SetBool("isClimbing", false);
                _animator.SetBool("isSwinging", false);
                break;
            case PlayerState.Shooting:
                _animator.SetTrigger("Throw");
                break;
            case PlayerState.Climbing:
                _animator.SetBool("isClimbing", true);
                break;
            case PlayerState.Swinging:
                _animator.SetBool("isSwinging", true);
                break;
            case PlayerState.CloseKilling:
                break;
            case PlayerState.Surrendering:
                //_animator.SetTrigger("Surrender");
                break;
            case PlayerState.Idle:
                _animator.SetBool("isRunning", false);
                _animator.SetBool("onAir", false);
                _animator.SetBool("isClimbing", false);
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
        if (_launcher.spearActive)
        {
            if (_movement.isClimbing())
            {
                currentState = PlayerState.Climbing;
                _animator.SetBool("isRunning", _movement.IsMoving);
            }
            else if (_movement.isSwinging())
            {
                currentState = PlayerState.Swinging;
            }
            else if (!_groundDetector.isGrounded)
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
        else
        {
            if (spearNotShooted)
            {
                spearNotShooted = false;
                currentState = PlayerState.Shooting;
            }
            else if (_movement.isClimbing())
            {
                currentState = PlayerState.Climbing;
                _animator.SetBool("isRunning", _movement.IsMoving);
            }
            else if (_movement.isSwinging())
            {
                currentState = PlayerState.Swinging;
            }
            else if (!_groundDetector.isGrounded)
            {
                currentState = PlayerState.Jumping;
                //MIRAR SIN SPEARS EN ESTOSSSSSSS
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
    }

    private void UpdateSpear()
    {
        _animator.SetBool("spearActive", _launcher.spearActive);
    }

    private void ActivateCloseKill()
    {
        _animator.SetTrigger("CloseKill");
    }

    private void ActivateSurrender()
    {
        _animator.SetTrigger("Surrender");
    }
    /*public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }*/
}

public enum PlayerState
{
    Idle,
    IdleWithoutSpear,
    Running,
    RunningWithoutSpear,
    Jumping,
    Shooting,
    Climbing,
    Swinging,
    CloseKilling,
    Surrendering,
}
