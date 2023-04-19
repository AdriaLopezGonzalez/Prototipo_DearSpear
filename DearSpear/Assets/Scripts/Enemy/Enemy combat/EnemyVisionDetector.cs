using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 3;

    [SerializeField]
    private float FieldOfView = 90;

    [SerializeField]
    LayerMask WhatIsVisible;

    Transform _player;

    public bool isDetectingPlayer;

    public static Action OnPlayerDetected;
    public static Action OnPlayerUndetected;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Vector2 start = transform.position;

        var direction = Quaternion.AngleAxis(
            FieldOfView / 2, transform.forward)
          * transform.right;
        Vector2 end = transform.position + direction.normalized * DetectionRange;
        Gizmos.DrawLine(start, end);

        start = transform.position;

        direction = Quaternion.AngleAxis(
            -FieldOfView / 2, transform.forward)
          * transform.right;
        end = transform.position + direction.normalized * DetectionRange;
        Gizmos.DrawLine(start, end);



        Gizmos.color = Color.white;

    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            if (IsInFOV())
            {
                if (IsNotBlocked())
                {
                    Debug.Log("grita muy mal");
                    //OnPlayerDetected?.Invoke();
                }
            }
        }
        if (!IsInRange() || !IsInFOV() || !IsNotBlocked())
        {
            //OnPlayerUndetected?.Invoke();
        }

    }



    private bool IsNotBlocked()
    {
        if (IsBlocked(_player))
        {
            isDetectingPlayer = false;
        }
        else
        {
            isDetectingPlayer = true;
        }

        return isDetectingPlayer;
    }

    private bool IsBlocked(Transform player)
    {
        Vector2 v2 = player.position - transform.position;
        var hit = Physics2D.Raycast(transform.position, v2, DetectionRange, WhatIsVisible);
        return hit.transform != player;
    }


    private bool IsInFOV()
    {
        float angle = GetAngle(_player);

        if (angle > FieldOfView / 2f)
        {
            isDetectingPlayer = false;
        }
        else
        {
            isDetectingPlayer = true;
        }

        return isDetectingPlayer;
    }

    private float GetAngle(Transform player)
    {
        Vector2 v1 = transform.right;
        Vector2 v2 = player.position - transform.position;
        return Vector2.Angle(v1, v2);
    }

    private bool IsInRange()
    {
        
        if (Vector2.Distance(_player.position, transform.position) < DetectionRange)
        {
            isDetectingPlayer = true;
        }
        else
        {
            isDetectingPlayer = false;
        }

        return isDetectingPlayer;
    }
}
