using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAnimations : MonoBehaviour
{
    public bool animationOngoing;
    private bool closeKillActive;
    private bool playerDeathActive;

    private float timer;
    private float timeCameraApproach = 0.3f;
    private float timeToKeepCamera = 0.7f;

    [SerializeField]
    private float constantCameraSize = 6;
    private Vector3 oldCameraPosition;
    private Camera cam;

    GameObject playerKiller;
    Transform _player;

    //private float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private float plainVelocity = 0;

    private Vector3 zoomOffset = new Vector3(1f, 0f, -10f);
    private float zoomSizeObjective = 4f;

    public static Action Respawn;
    public static Action PlayerStillAlive;
    public static Action DropBlood;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        animationOngoing = false;
        closeKillActive = false;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void ActivatePlayerDeath(GameObject enemy)
    {
        FreezePlayer();

        animationOngoing = true;
        playerDeathActive = true;
        oldCameraPosition = transform.position;

        playerKiller = enemy;
    }

    private void ActivateCloseKill()
    {
        FreezePlayer();

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
            /*if (playerDeathActive && playerKiller == null)
            {
                playerDeathActive = false;
                cam.orthographicSize = constantCameraSize;
                animationOngoing = false;
                timer = 0;
                PlayerStillAlive?.Invoke();
            }*/
        }
        else if (timer < timeCameraApproach + timeToKeepCamera)
        {

        }
        else if (timer < timeCameraApproach + timeToKeepCamera + timeCameraApproach)
        {
            if (playerDeathActive)
            {
                playerDeathActive = false;
                cam.orthographicSize = constantCameraSize;
                animationOngoing = false;
                timer = 0;
                if (playerKiller != null)
                {
                    Respawn?.Invoke();
                }
                else
                {
                    PlayerStillAlive?.Invoke();
                }

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
                UnfreezePlayer();
                DropBlood?.Invoke();
            }

        }
    }

    public void ManageAnimation(Transform target)
    {
        if (playerDeathActive)
        {
            PlayerZoom(target);
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

    private void FreezePlayer()
    {
        _player.GetComponent<PlayerInput>().enabled = false;
        _player.GetComponent<PlayerInputs>().FreezePlayer();
        _player.GetComponent<PlayerInputs>().enabled = false;
    }

    private void UnfreezePlayer()
    {
        _player.GetComponent<PlayerInput>().enabled = true;
        _player.GetComponent<PlayerInputs>().enabled = true;
    }

}
