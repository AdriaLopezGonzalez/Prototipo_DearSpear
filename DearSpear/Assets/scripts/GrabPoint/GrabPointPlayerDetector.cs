using UnityEngine;

public class GrabPointPlayerDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 8;

    Transform _player;

    public bool isDetecting;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.white;
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (IsInRange())
        {
            isDetecting = true;
            ChangeColor(isDetecting);
        }
        if (!IsInRange() && isDetecting)
        {
            isDetecting = false;
            ChangeColor(isDetecting);
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
