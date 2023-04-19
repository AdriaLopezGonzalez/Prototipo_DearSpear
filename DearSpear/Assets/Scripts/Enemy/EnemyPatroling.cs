using UnityEngine;

public class EnemyPatroling : MonoBehaviour
{
    EnemyGroundDetector _groundDetector;

    [SerializeField]
    private float baseSpeed = 0.5f;

    private float Speed;

    private bool flipped;
    private float pauseAfterFlip;

    private void OnEnable()
    {
        EnemyCollider.Flip += Flip;
    }

    private void OnDisable()
    {
        EnemyCollider.Flip += Flip;
    }

    private void Awake()
    {
        _groundDetector = GetComponentInChildren<EnemyGroundDetector>();

        Speed = baseSpeed;
    }

    void Update()
    {
        if (GroundNotDetected())
        {
            Flip();
        }

        if (flipped)
        {
            Speed = 0;

            pauseAfterFlip += 1 * Time.deltaTime;

            if (pauseAfterFlip >= 3)
            {
                flipped = false;
                Speed = baseSpeed;
                pauseAfterFlip = 0;
            }
        }

        Move();
    }

    private bool GroundNotDetected()
    {
        return _groundDetector.NotGround;
    }

    public void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));

        flipped = true;
    }

    private void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }
}
