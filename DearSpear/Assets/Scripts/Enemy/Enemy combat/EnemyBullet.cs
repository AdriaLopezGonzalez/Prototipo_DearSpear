using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D bulletRb;

    private Transform _player;
    private Vector2 _playerDirection;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        GetPlayerDirection();
    }

    private void Update()
    {
        MoveBullet();

        RotateBullet();
    }

    private void MoveBullet()
    {
        bulletRb.velocity = new Vector2(_playerDirection.x, _playerDirection.y).normalized * speed;
    }

    private void RotateBullet()
    {
        float angle = Mathf.Atan2(-_playerDirection.x, -_playerDirection.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private void GetPlayerDirection()
    {
        _playerDirection = _player.transform.position - transform.position;
    }
}
