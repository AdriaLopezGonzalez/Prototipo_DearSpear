using UnityEngine;

public class BirdPatrol : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Rigidbody2D _rb;

    float maxTime = 10;
    float timer;
    Vector2 speed = new Vector2(0.1f, 0);
    float maxVelocity = 5;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity += speed;
        timer += Time.deltaTime;

        if (_rb.velocity.x > maxVelocity)
        {
            _rb.velocity = new Vector2(5, 0);
        }

        if (_rb.velocity.x < -maxVelocity)
        {
            _rb.velocity = new Vector2(-5, 0);
        }

        if (_rb.velocity.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }

        if (timer > maxTime)
        {
            timer = 0;
            speed = new Vector2(-speed.x, 0);
        }
    }
}
