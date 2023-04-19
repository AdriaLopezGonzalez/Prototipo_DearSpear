using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollider : MonoBehaviour
{
    public static Action CanFlip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") /*|| collision.gameObject.CompareTag("Wall")*/)
        {
            CanFlip?.Invoke();
        }
    }
}
