using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform _bulletSpawnPosition;

    private float timeBeforeShoot;
    private bool canShoot;

    private void OnEnable()
    {
        EnemyPatroling.Shoot += Shoot;
    }

    private void OnDisable()
    {
        EnemyPatroling.Shoot -= Shoot;
    }

    public void Shoot()
    {

        ShootTimer();

        if(canShoot)
        {
            Instantiate(_bullet, _bulletSpawnPosition.position, Quaternion.identity);

            canShoot = false;
            timeBeforeShoot = 0;
        }
    }

    private void ShootTimer()
    {
        timeBeforeShoot += Time.deltaTime;

        if (timeBeforeShoot >= 0.25f)
        {
            canShoot = true;
        }
    }
}
