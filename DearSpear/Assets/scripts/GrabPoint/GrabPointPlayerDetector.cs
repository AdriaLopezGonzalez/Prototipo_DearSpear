using UnityEngine;

public class GrabPointPlayerDetector : MonoBehaviour
{
    [SerializeField]
    float DetectionRange = 8;

    Transform _player;

    public bool isDetecting;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
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
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

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
