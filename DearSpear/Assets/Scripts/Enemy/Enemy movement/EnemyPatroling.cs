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

    public float pauseBeforeFlip = 0;

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
        if (GroundNotDetected() || canFlip)
        {
            Speed = 0;

            pauseBeforeFlip += 1 * Time.deltaTime;

            if (pauseBeforeFlip >= 3)
            {
                Flip();

                canFlip = false;
                Speed = baseSpeed;
                pauseBeforeFlip = 0;
            }
        }

        Move();
    }

    private bool GroundNotDetected()
    {
        return _groundDetector.NotGround;
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

        DeathCamera?.Invoke(gameObject);
        PlayerSurrender?.Invoke();
        Debug.Log("ho he fet");
        
        //_weapon.Shoot();
    }


    public void ContinuePatrolling()
    {
        Speed = baseSpeed;
    }

    public bool isMoving()
    {
        return Speed > 0.3;
    }


}
