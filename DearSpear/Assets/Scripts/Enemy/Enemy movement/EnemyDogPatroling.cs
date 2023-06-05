using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EnemyDogPatroling : MonoBehaviour
{
    EnemyGroundDetector _groundDetector;

    [SerializeField]
    private float baseSpeed = 0.5f;

    public float Speed;
    private bool playerDetected;
    public float pauseAfterFlip = 0;

    private bool isInEdge = false;

    private bool canFlip;

    private EnemyPatroling enemyWithDog;

    //public static Action Shoot;

    private EnemyWeapon _weapon;

    public static Action<GameObject> DeathCamera;
    public static Action PlayerSurrender;

    //private void OnEnable()
    //{
    //    EnemyWallCollider.CanFlip += CanFlip;
    //
    //    EnemyVisionDetector.DetectedThePlayer += DetectedThePlayer;
    //    EnemyVisionDetector.ContinuePatrolling += ContinuePatrolling;
    //}
    //
    //private void OnDisable()
    //{
    //    EnemyWallCollider.CanFlip -= CanFlip;
    //
    //    EnemyVisionDetector.DetectedThePlayer -= DetectedThePlayer;
    //    EnemyVisionDetector.ContinuePatrolling -= ContinuePatrolling;
    //}

    private void Awake()
    {
        _groundDetector = GetComponentInChildren<EnemyGroundDetector>();

        _weapon = GetComponentInChildren<EnemyWeapon>();

        Speed = baseSpeed;

        enemyWithDog = transform.parent.GetComponentInChildren<EnemyPatroling>();
    }

    void Update()
    {
        CheckGroundDetection();
        if (canFlip)
        {

            Speed = 0;

            if (isInEdge)
            {
                if (pauseAfterFlip >= 2.5f)
                {
                    Flip();

                    isInEdge = false;
                }
            }
            else
            {
                if (pauseAfterFlip == 0)
                {
                    Flip();
                }
            }

            pauseAfterFlip += 1 * Time.deltaTime;

            if (pauseAfterFlip >= 3)
            {
                canFlip = false;
                Speed = baseSpeed;
                pauseAfterFlip = 0;
            }
        }

        Move();
    }

    private void CheckGroundDetection()
    {
        if (!canFlip && _groundDetector.NotGround)
        {
            canFlip = true;

            isInEdge = true;
        }
    }

    public void CanFlip()
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

    public void DetectedThePlayer(Transform player)
    {
        Speed = 0.0f;

        playerDetected = true;

        enemyWithDog.GoWithDog(player);
        //DeathCamera?.Invoke(gameObject);
        //PlayerSurrender?.Invoke();
    }

    public void ContinuePatrolling()
    {
        if (!playerDetected)
        {
            Speed = baseSpeed;
        }
    }

    public bool isMoving()
    {
        return Speed > 0.3;
    }


}
