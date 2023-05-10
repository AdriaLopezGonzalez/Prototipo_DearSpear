using System;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    public bool animationOngoing;
    private bool longShotActive;
    private bool closeKillActive;
    private bool playerDeathActive;

    private float timer;
    private float timeCameraApproach = 0.5f;
    private float timeToKeepCamera = 1f;

    [SerializeField]
    private float constantCameraSize = 5;
    private Vector3 oldCameraPosition;
    private Camera cam;

    //private float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private float plainVelocity = 0;

    private Vector3 zoomOffset = new Vector3(1f, 0f, -10f);
    private float zoomSizeObjective = 4f;

    public static Action Respawn;

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
        EnemyPatroling.DeathCamera += ActivatePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerCloseKill.ActivateCloseKill -= ActivateCloseKill;
        EnemyPatroling.DeathCamera -= ActivatePlayerDeath;
    }

    private void LongShot()
    {

    }

    private void ActivatePlayerDeath()
    {
        animationOngoing = true;
        playerDeathActive = true;
        oldCameraPosition = transform.position;
    }

    private void ActivateCloseKill()
    {
        animationOngoing = true;
        closeKillActive = true;
        oldCameraPosition = transform.position;
    }

    private void PlayerZoom(Transform target)
    {
        timer += Time.deltaTime;

        if (timer < timeCameraApproach)
        {
            Vector3 targetPosition = target.position + zoomOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, timeCameraApproach / 2);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoomSizeObjective, ref plainVelocity, timeCameraApproach / 2);
        }
        else if (timer < timeCameraApproach + timeToKeepCamera)
        {

        }
        else if (timer < timeCameraApproach + timeToKeepCamera + timeCameraApproach)
        {
            if (playerDeathActive)
            {
                playerDeathActive = false;
                Respawn?.Invoke();
                cam.orthographicSize = constantCameraSize;
                animationOngoing = false;
                timer = 0;
            }
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, oldCameraPosition, ref velocity, timeCameraApproach / 2);
                cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, constantCameraSize, ref plainVelocity, timeCameraApproach / 2);
            }
        }
        else
        {
            cam.orthographicSize = constantCameraSize;
            animationOngoing = false;
            timer = 0;
            if (closeKillActive)
            {
                closeKillActive = false;
            }

        }
    }

    public void ManageAnimation(Transform target)
    {
        if (playerDeathActive)
        {
            PlayerZoom(target);
        }
        else if (longShotActive)
        {
            LongShot();
        }
        else if (closeKillActive)
        {
            PlayerZoom(target);
        }
        else
        {
            animationOngoing = false;
        }
    }

}
