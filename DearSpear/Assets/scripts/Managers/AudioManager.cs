using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource enemyHit;
    [SerializeField] private AudioSource enemyHurt;
    [SerializeField] private AudioSource enemyCloseKill;

    [SerializeField] private AudioSource throwSpear;
    [SerializeField] private AudioSource land;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyHurt()
    {
        enemyHit.Play();
        enemyHurt.Play();
    }

    public void EnemyCloseKill()
    {
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
