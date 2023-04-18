using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVineDetector : MonoBehaviour
{
    bool canClimb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canClimb = false;
        }
    }
}
