using UnityEngine;

public class GrabPointPlayerDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 8;

    Transform _player;

    public bool isDetecting;
    private ParticleSystem glowParticles;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.white;
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        glowParticles = transform.parent.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (IsInRange())
        {
            isDetecting = true;
            //ChangeColor(isDetecting);
            //ACTIVAR PARTICULAS
            if (!glowParticles.isPlaying)
            {
                glowParticles.Play();
            }

        }
        if (!IsInRange() && isDetecting)
        {
            isDetecting = false;
            //ChangeColor(isDetecting);
            //DESACTIVAR PARTICULAS
            glowParticles.Stop();
        }
    }

    private void ChangeColor(bool detectorCheck)
    {
        if (detectorCheck)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private bool IsInRange()
    {
        return Vector2.Distance(_player.position, transform.position) < DetectionRange;
    }
}
