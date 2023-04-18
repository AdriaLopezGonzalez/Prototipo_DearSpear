using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Rigidbody2D spearRb;

    public bool spearCollided;

    public static Action SpearGrabbed;
    void Start()
    {
        spearRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rotateSpear();

        if(transform.position.y < -10)
        {
            SpearGrabbed?.Invoke();
            Destroy(gameObject);
        }
    }

    private void rotateSpear()
    {
        if (!spearCollided)
        {
            float angle = Mathf.Atan2(spearRb.velocity.y, spearRb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
