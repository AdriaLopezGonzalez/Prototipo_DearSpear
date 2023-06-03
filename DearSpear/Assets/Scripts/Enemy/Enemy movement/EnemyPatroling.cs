using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class EnemyPatroling : MonoBehaviour
{
    EnemyGroundDetector _groundDetector;

    [SerializeField]
    private float baseSpeed = 0.5f;

    private float Speed;
    private bool playerDetected;
    public float pauseAfterFlip = 0;

    private bool canFlip;

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
    }

    void Update()
    {
        CheckGroundDetection();
        if (canFlip)
        {

            Speed = 0;

            if (pauseAfterFlip == 0)
            {
                Flip();
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
        if(!canFlip && _groundDetector.NotGround)
        {
            canFlip = true;
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
        Speed = 0;

        playerDetected = true;
        DeathCamera?.Invoke(gameObject);
        PlayerSurrender?.Invoke();
        //_weapon.Shoot();
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
