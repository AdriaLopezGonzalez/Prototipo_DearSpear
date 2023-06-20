using UnityEngine;

public class MainCameraMove : MonoBehaviour
{
    private Vector3 offset = new Vector3(2.2f, 0f, -10f);
    private float smoothTimeX = 0.15f;
    private float smoothTimeY = 0.9f;
    private float velocityX = 0;
    private float velocityY = 0;

    private float xLimit = 560f;

    private float constantCameraSize = 6;
    private float timeCameraApproach = 0.5f;
    private float plainVelocity = 0;

    private Camera cam;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private CameraAnimations anim;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

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
            if (xPosition > xLimit)
            {
                xPosition = xLimit;
            }
            transform.position = new Vector3(xPosition, yPosition, transform.position.z);

            if (cam.orthographicSize != constantCameraSize)
            {
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, constantCameraSize, ref plainVelocity, timeCameraApproach / 2);

                //if (cam.orthographicSize < constantCameraSize + 0.05 || cam.orthographicSize > constantCameraSize - 0.05)
                //{
                //    cam.orthographicSize = constantCameraSize;
                //}
            }
        }
    }

    public void CameraRespawn()
    {
        transform.position = target.position + offset;
    }
}
