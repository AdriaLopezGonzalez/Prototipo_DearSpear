using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearCollisionDetector : MonoBehaviour
{
    private Spear _spear;

    private Rigidbody2D spearRb;

    public static Action SpearGrabbed;
    private void Start()
    {
        _spear = GetComponent<Spear>();
        spearRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            _spear.spearCollided = true;
            spearRb.velocity = Vector2.zero;
            spearRb.isKinematic = true;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (_spear.spearCollided)
            {
                SpearGrabbed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
