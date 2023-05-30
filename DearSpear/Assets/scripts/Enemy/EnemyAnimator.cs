using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyState currentState;
    private EnemyPatroling _movement;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyPatroling>();
    }
    
    private void Update()
    {
        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                _animator.SetBool("isMoving", false);
                break;
            case EnemyState.Patroling:
                _animator.SetBool("isMoving", true);
                break;
            default:
                break;

        }
    }

    private void UpdateState()
    {
        if (_movement.isMoving())
        {
            currentState = EnemyState.Patroling;
        }
        else
        {
            currentState = EnemyState.Idle;
        }
    }

}

public enum EnemyState
{
    Idle,
    Patroling,
}
