using System;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!collision.gameObject.GetComponentInChildren<PlayerCollisionDetector>().isGrounded)
            {
                float forceX;
                float forceY;
                if ((collision.transform.position.x - transform.position.x) > 0)
                {
                    forceX = 7f;
                }
                else
                {
                    forceX = -7f;
                }

                if ((collision.transform.position.y - transform.position.y) > 0)
                {
                    forceY = 5f;
                }
                else
                {
                    forceY = -5f;
                }

                Vector2 force = new Vector2(forceX, forceY);

                collision.gameObject.GetComponent<PlayerMovement>().knockback = true;
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += force;
            }

        }
    }
}
