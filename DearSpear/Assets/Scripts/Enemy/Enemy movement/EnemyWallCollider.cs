using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallCollider : MonoBehaviour
{
    //public static Action CanFlip;
    private EnemyPatroling _patroling;
    private EnemyDogPatroling _dogPatroling;

    private void Awake()
    {
        _patroling = GetComponentInParent<EnemyPatroling>();

        if (gameObject.CompareTag("DogEnemy"))
        {
            _dogPatroling = GetComponentInParent<EnemyDogPatroling>();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !transform.CompareTag("EnemyDog"))
        {
            _patroling.Speed = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("BaseEnemy")/*|| collision.gameObject.CompareTag("Wall")*/)
        {
            //CanFlip?.Invoke();
            if (gameObject.CompareTag("DogEnemy"))
            {
                Debug.Log("queHaceAqui");
                _dogPatroling.CanFlip();
            }
            else
            {
                _patroling.CanFlip();
            }
        }
    }
}
