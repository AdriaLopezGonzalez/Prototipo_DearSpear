using UnityEngine;

public class BirdPatrol : MonoBehaviour
{
    Vector3 startPosition;
    SpriteRenderer _spriteRenderer;

    float xMaxMove = 30;
    float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        if (transform.position.x > startPosition.x + xMaxMove || transform.position.x < startPosition.x - xMaxMove)
        {
            speed = -speed;
            if (_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = false;
            }
            else
            {
                _spriteRenderer.flipX = true;
            }
        }
    }
}
