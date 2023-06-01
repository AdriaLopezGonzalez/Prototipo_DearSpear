using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool IsMoving => _isMoving;

    [SerializeField]
    private float Speed = 8f;

    private bool _isMoving;
    private PlayerVineDetector _vineDetect;
    PlayerInputs _input;
    Rigidbody2D _rigidbody;

    //private PlayerAnimator pAnimator;
    private SpriteRenderer _spriteRenderer;

    private Transform _enemyChecker;

    private float oldGravityScale;

    public static Func<bool> CheckHook;

    void Start()
    {
        _input = GetComponent<PlayerInputs>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _vineDetect = gameObject.GetComponentInChildren<PlayerVineDetector>();
        //pAnimator = GetComponent<PlayerAnimator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "EnemyChecker")
            {
                _enemyChecker = child;
            }
        }

        oldGravityScale = _rigidbody.gravityScale;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        FlipX();

        if (CheckHook())
        {
            _rigidbody.gravityScale = oldGravityScale;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity += new Vector2(_input.MovementHorizontal * Speed / 100, 0);
            _isMoving = direction.magnitude > 1f;
        }
        else if (_vineDetect.canClimb)
        {
            _rigidbody.gravityScale = 0;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _input.MovementVertical * Speed);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 1f;
        }
        else
        {
            _rigidbody.gravityScale = oldGravityScale;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 1f;
        }

    }

    private void FlipX()
    {
        bool oldFlip = _spriteRenderer.flipX;

        if (_input.MovementHorizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_input.MovementHorizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }

        if (oldFlip != _spriteRenderer.flipX)
        {
            _enemyChecker.localPosition = new Vector3(-_enemyChecker.localPosition.x, _enemyChecker.localPosition.y, _enemyChecker.localPosition.z);
        }
    }

    public bool isClimbing()
    {
        return _vineDetect.canClimb;
    }

    public bool isSwinging()
    {
        return CheckHook();
    }
    /*private void SetAnimator()
    {
        //hacer script con FSM para el animator
        if (_isMoving)
        {
            pAnimator.ChangeState(PlayerState.Running);
        }
        else
        {
            pAnimator.ChangeState(PlayerState.Idle);
        }
    }*/
}
