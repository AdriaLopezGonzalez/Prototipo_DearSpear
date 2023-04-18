using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVineDetector : MonoBehaviour
{
    public bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
        canClimb = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vine"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vine"))
        {
            canClimb = false;
        }
    }
}
