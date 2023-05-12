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

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float oldGravityScale;

    public static Func<bool> CheckHook;

    void Start()
    {
        _input = GetComponent<PlayerInputs>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _vineDetect = gameObject.GetComponentInChildren<PlayerVineDetector>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        oldGravityScale = _rigidbody.gravityScale;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        CheckFlip();

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

            SetAnimator();

        }

    }

    private void CheckFlip()
    {
        if (_input.MovementHorizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        if (_input.MovementHorizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void SetAnimator()
    {
        if (_isMoving)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
    }
}
