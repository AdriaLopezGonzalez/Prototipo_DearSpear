using UnityEngine;

public class BirdDeath : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spear")
        {
            ParticleSystem BirdFeathers = GameObject.Instantiate(deathParticles).GetComponent<ParticleSystem>();
            BirdFeathers.transform.position = gameObject.transform.position;
            BirdFeathers.Play();

            Destroy(gameObject);
        }
    }
}
