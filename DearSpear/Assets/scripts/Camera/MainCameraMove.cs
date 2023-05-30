using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(2.2f, 1f, -10f);
    private float smoothTimeX = 0.15f;
    private float smoothTimeY = 0.7f;
    private float velocityX = 0;
    private float velocityY = 0;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private CameraAnimations anim;

    private void FixedUpdate()
    {
        if (anim.animationOngoing)
        {
            anim.ManageAnimation(target);
        }
        else
        {
            Vector3 targetPosition = target.position + offset;
            float xPosition = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocityX, smoothTimeX);
            float yPosition = Mathf.SmoothDamp(transform.position.y, targetPosition.y, ref velocityY, smoothTimeY);
            transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        }
    }

    public void CameraRespawn()
    {
        transform.position = target.position + offset;
    }
}
