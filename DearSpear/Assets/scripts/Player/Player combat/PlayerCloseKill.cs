using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseKill : MonoBehaviour
{
    private PlayerEnemyDetector enemyDetector;

    public static Action ActivateCloseKill;

    private void Start()
    {
        enemyDetector = GetComponent<PlayerEnemyDetector>();
    }
    private void OnEnable()
    {
        PlayerInput.KillEnemy += KillEnemy;
    }

    private void OnDisable()
    {
        PlayerInput.KillEnemy -= KillEnemy;
    }

    private void KillEnemy()
    {
        //HACER ANIMACIÓN
        //ESCONDER PLAYER
        //ZOOM DE CAMARA
        ActivateCloseKill?.Invoke();
        Destroy(enemyDetector._enemyTransform.gameObject);

    }
    
}
