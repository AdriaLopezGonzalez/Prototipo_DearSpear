using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool IsMoving => _isMoving;


    [SerializeField]
    private float Speed = 8f;

    private bool _isMoving;
    private PlayerVineDetector _vineDetect;
    PlayerInput _input;
    Rigidbody2D _rigidbody;

    private float oldGravityScale;

    public static Func<bool> CheckHook;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _vineDetect = gameObject.GetComponentInChildren<PlayerVineDetector>();

        oldGravityScale = _rigidbody.gravityScale;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (CheckHook())
        {
            _rigidbody.gravityScale = oldGravityScale;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity += new Vector2(_input.MovementHorizontal * Speed / 100, 0);
            _isMoving = direction.magnitude > 0.01f;
        }
        else if (_vineDetect.canClimb)
        {
            _rigidbody.gravityScale = 0;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _input.MovementVertical * Speed);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 0.01f;
        }
        else
        {
            _rigidbody.gravityScale = oldGravityScale;

            Vector2 direction = new Vector2(_input.MovementHorizontal * Speed, _rigidbody.velocity.y);

            _rigidbody.velocity = direction;
            _isMoving = direction.magnitude > 0.01f;
        }

    }
}
