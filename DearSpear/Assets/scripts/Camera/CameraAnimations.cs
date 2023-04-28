using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    public bool animationOngoing;
    private bool longShotActive;
    private bool closeKillActive;

    private float timer;
    private float timeCameraApproach = 0.5f;
    private float timeToKeepCamera = 1f;

    private Vector3 oldCameraPosition;
    private float oldCameraSize;
    private Camera cam;

    //private float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private float plainVelocity = 0;

    private Vector3 closeKillOffset = new Vector3(1f, 0f, -10f);
    private float closeKillSizeObjective = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        animationOngoing = false;
        longShotActive = false;
        closeKillActive = false;
    }

    private void OnEnable()
    {
        PlayerCloseKill.ActivateCloseKill += ActivateCloseKill;
    }

    private void OnDisable()
    {
        PlayerCloseKill.ActivateCloseKill -= ActivateCloseKill;
    }

    private void LongShot()
    {

    }

    private void ActivateCloseKill()
    {
        animationOngoing = true;
        closeKillActive = true;
        oldCameraPosition = transform.position;
        oldCameraSize = cam.orthographicSize;
    }

    private void CloseKill(Transform target)
    {
        timer += Time.deltaTime;

        if (timer < timeCameraApproach)
        {
            Vector3 targetPosition = target.position + closeKillOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeCameraApproach);
            cam.orthographicSize = Mathf    .SmoothDamp(cam.orthographicSize, closeKillSizeObjective, ref plainVelocity, timeCameraApproach);
        }
        else if (timer < timeCameraApproach + timeToKeepCamera)
        {

        }
        else if (timer < timeCameraApproach + timeToKeepCamera + timeCameraApproach)
        {
            transform.position = Vector3.SmoothDamp(transform.position, oldCameraPosition, ref velocity, timeCameraApproach);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, oldCameraSize, ref plainVelocity, timeCameraApproach);
        }
        else
        {
            animationOngoing = false;
            closeKillActive = false;
            timer = 0;
        }
    }

    public void ManageAnimation(Transform target)
    {
        if (longShotActive)
        {
            LongShot();
        }
        else if (closeKillActive)
        {
            CloseKill(target);
        }
        else
        {
            animationOngoing = false;
        }
    }
}
