using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(2.5f, 2.5f, -10f);
    private float smoothTime = 0.30f;
    private Vector3 velocity = Vector3.zero;

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
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    public void CameraRespawn()
    {
        transform.position = target.position + offset;
    }
}
