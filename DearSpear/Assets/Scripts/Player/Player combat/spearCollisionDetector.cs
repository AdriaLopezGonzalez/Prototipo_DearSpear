using System;
using UnityEngine;

public class spearCollisionDetector : MonoBehaviour
{
    private Spear _spear;

    private Rigidbody2D spearRb;
    public GameObject dirtHit;

    public static Action SpearGrabbed;

    private float timeHanging;

    private bool isFalling;
    private bool particlesThrown = false;
    private void Start()
    {
        _spear = GetComponent<Spear>();
        spearRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            gameObject.tag = "SpearInGround";
            if (!particlesThrown)
            {
                ParticleSystem thisDirt = GameObject.Instantiate(dirtHit).GetComponent<ParticleSystem>();
                thisDirt.transform.localRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - 180);
                thisDirt.transform.position = transform.position;
                thisDirt.Play();
                particlesThrown = true;
            }
        }

        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("GroundChecker") && !collision.gameObject.CompareTag("VineChecker"))
        {
            if (!(collision.gameObject.CompareTag("Roof") && isFalling))
            {
                _spear.spearCollided = true;
                spearRb.velocity = Vector2.zero;
                spearRb.isKinematic = true;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_spear.spearCollided)
            {
                gameObject.tag = "Spear";
                SpearGrabbed?.Invoke();
            }
        }

        if (collision.gameObject.CompareTag("Roof"))
        {
            if (_spear.spearCollided)
            {
                timeHanging += 5 * Time.deltaTime;

                if (timeHanging >= 2)
                {
                    _spear.spearCollided = false;
                    spearRb.velocity = new Vector2(0, -7);

                    timeHanging = 0;
                    isFalling = true;
                }
            }
        }
    }
}
