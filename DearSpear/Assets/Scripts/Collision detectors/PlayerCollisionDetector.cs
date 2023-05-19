using System;
using UnityEngine;

public class PlayerCollisionDetector : MonoBehaviour
{
    public bool isGrounded;
    //public PlayerAnimator pAnimator;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            //SetAnimator(isGrounded);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            //SetAnimator(isGrounded);
        }
    }

    /*private void SetAnimator(bool _isGrounded)
    {
        //hacer script con FSM para el animator
        if (_isGrounded)
        {
            pAnimator.ChangeState(PlayerState.Idle);
        }
        else
        {
            pAnimator.ChangeState(PlayerState.Jumping);
        }
    }*/
}
