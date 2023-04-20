using System;
using UnityEngine;

public class EnemyPatroling : MonoBehaviour
{
    EnemyGroundDetector _groundDetector;

    [SerializeField]
    private float baseSpeed = 0.5f;

    private float Speed;

    private float pauseAfterFlip;

    private bool canFlip;

    public static Action Shoot;

    private void OnEnable()
    {
        EnemyWallCollider.CanFlip += CanFlip;

        EnemyVisionDetector.DetectedThePlayer += DetectedThePlayer;
        EnemyVisionDetector.ContinuePatrolling += ContinuePatrolling;
    }

    private void OnDisable()
    {
        EnemyWallCollider.CanFlip -= CanFlip;

        EnemyVisionDetector.DetectedThePlayer -= DetectedThePlayer;
        EnemyVisionDetector.ContinuePatrolling -= ContinuePatrolling;
    }

    private void Awake()
    {
        _groundDetector = GetComponentInChildren<EnemyGroundDetector>();

        Speed = baseSpeed;
    }

    void Update()
    {
        if (GroundNotDetected() || canFlip)
        {
            Speed = 0;

            pauseAfterFlip += 1 * Time.deltaTime;

            if (pauseAfterFlip >= 3)
            {
                Flip();

                canFlip = false;
                Speed = baseSpeed;
                pauseAfterFlip = 0;
            }
        }

        Move();
    }

    private bool GroundNotDetected()
    {
        return _groundDetector.NotGround;
    }

    private void CanFlip()
    {
        canFlip = true;
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    public void DetectedThePlayer()
    {
        Speed = 0;

        Shoot?.Invoke();
    }

    public void ContinuePatrolling()
    {
        Speed = baseSpeed;
    }
}
