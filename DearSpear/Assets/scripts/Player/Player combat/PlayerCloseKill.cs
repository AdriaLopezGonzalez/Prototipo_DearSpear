using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseKill : MonoBehaviour
{
    private PlayerEnemyDetector enemyDetector;

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
        Destroy(enemyDetector._enemyTransform.gameObject);

    }
    
}
