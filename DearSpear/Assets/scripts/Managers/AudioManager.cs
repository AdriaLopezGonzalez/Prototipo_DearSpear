using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource enemyHit;
    [SerializeField] private AudioSource enemyHurt1;
    [SerializeField] private AudioSource enemyHurt2;
    [SerializeField] private AudioSource enemyHurt3;
    [SerializeField] private AudioSource enemyCloseKill;

    private AudioSource[] enemyHurts = new AudioSource[3];

    [SerializeField] private AudioSource throwSpear;
    [SerializeField] private AudioSource land;

    // Start is called before the first frame update
    void Start()
    {
        enemyHurts[0] = enemyHurt1;
        enemyHurts[1] = enemyHurt2;
        enemyHurts[2] = enemyHurt3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHurt()
    {
        enemyHit.Play();
        enemyHurts[Random.Range(0,3)].Play();
    }

    public void EnemyCloseKill()
    {
        enemyCloseKill.pitch = Random.Range(0.85f, 1.15f);
        enemyCloseKill.Play();
        enemyHit.Play();
    }

    public void ThrowSpear()
    {
        throwSpear.Play();
    }

    public void Land()
    {
        land.Play();
    }
}
