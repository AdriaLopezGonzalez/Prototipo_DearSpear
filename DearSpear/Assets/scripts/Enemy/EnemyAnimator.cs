using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyState currentState;
    private EnemyPatroling _movement;
    private EnemyDogPatroling _dogMovement;
    private Animator _animator;
    private EnemyDogVisionDetector _dogVision;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<EnemyPatroling>();
        _dogMovement = GetComponent<EnemyDogPatroling>();
        _dogVision = GetComponent<EnemyDogVisionDetector>();
    }

    private void Update()
    {
        UpdateState();

        switch (currentState)
        {
            case EnemyState.Idle:
                _animator.SetBool("isMoving", false);
                if (_dogVision) { _animator.SetBool("Barking", false); }
                break;
            case EnemyState.Patroling:
                _animator.SetBool("isMoving", true);
                if (_dogVision) { _animator.SetBool("Barking", false); }
                break;
            case EnemyState.Barking:
                _animator.SetBool("Barking", true);
                break;
            default:
                break;

        }
    }

    private void UpdateState()
    {
        if (!gameObject.CompareTag("DogEnemy"))
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
        else
        {
            if (_dogVision.isDetectingPlayer)
            {
                currentState = EnemyState.Barking;
            }
            else if (_dogMovement.isMoving())
            {
                currentState = EnemyState.Patroling;
            }
            else
            {
                currentState = EnemyState.Idle;
            }
        }
    }

}

public enum EnemyState
{
    Idle,
    Patroling,
    Barking,
}
