using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseKill : MonoBehaviour
{
    private PlayerEnemyDetector enemyDetector;

    public static Action ActivateCloseKill;
    public static Action ActivateCloseKillAnim;

    public GameObject enemyBlood;
    private Vector3 bloodOffset = new Vector3(1f, -0.5f, 0);
    private Vector3 bloodOffsetFlipped = new Vector3(-1f, -0.5f, 0);

    private GameObject _audioManager;

    private void Start()
    {
        enemyDetector = GetComponent<PlayerEnemyDetector>();
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }
    private void OnEnable()
    {
        PlayerInputs.KillEnemy += KillEnemy;
        CameraAnimations.DropBlood += DropBlood;
    }

    private void OnDisable()
    {
        PlayerInputs.KillEnemy -= KillEnemy;
        CameraAnimations.DropBlood -= DropBlood;
    }

    private void KillEnemy()
    {
        //HACER ANIMACIÓN
        //ESCONDER PLAYER
        //ZOOM DE CAMARA
        ActivateCloseKillAnim?.Invoke();
        ActivateCloseKill?.Invoke();

        _audioManager.GetComponent<AudioManager>().EnemyCloseKill();

        Destroy(enemyDetector._enemyTransform.gameObject);
    }

    private void DropBlood()
    {
        ParticleSystem thisBlood = GameObject.Instantiate(enemyBlood).GetComponent<ParticleSystem>();
        if (!gameObject.transform.parent.GetComponent<SpriteRenderer>().flipX)
        {
            thisBlood.transform.position = gameObject.transform.parent.position + bloodOffset;
        }
        else
        {
            thisBlood.transform.position = gameObject.transform.parent.position + bloodOffsetFlipped;
        }
        thisBlood.Play();
    }
    
}
