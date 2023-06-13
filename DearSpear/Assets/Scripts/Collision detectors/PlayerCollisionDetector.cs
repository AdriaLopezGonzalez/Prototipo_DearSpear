using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public bool isGrounded;
    private GameObject _audioManager;

    private void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bool lastGrounded = isGrounded;
            isGrounded = true;
            gameObject.GetComponentInParent<PlayerMovement>().knockback = false;

            if (lastGrounded != isGrounded)
            {
                _audioManager.GetComponent<AudioManager>().Land();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;

        }
    }

}
