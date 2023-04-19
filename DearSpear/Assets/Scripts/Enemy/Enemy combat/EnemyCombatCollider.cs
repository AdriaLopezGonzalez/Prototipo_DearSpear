using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatCollider : MonoBehaviour
{
    public static Action SpearFall;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spear"))
        {
            Destroy(this.gameObject);

            SpearFall?.Invoke();
        }
    }
}
