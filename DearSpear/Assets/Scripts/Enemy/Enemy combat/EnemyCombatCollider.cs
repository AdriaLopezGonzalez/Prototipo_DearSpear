using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatCollider : MonoBehaviour
{
    public static Action<Vector3> SpearFall;
    public GameObject enemyBlood;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            ParticleSystem thisBlood = GameObject.Instantiate(enemyBlood).GetComponent<ParticleSystem>();
            thisBlood.transform.position = gameObject.transform.position;
            thisBlood.Play();

            Destroy(this.gameObject);


            SpearFall?.Invoke(gameObject.transform.position);
        }
    }
}
