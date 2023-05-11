using System;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour
{
    public static Action<Transform> SetActiveCheckpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetActiveCheckpoint?.Invoke(gameObject.transform);
        }
    }
}
