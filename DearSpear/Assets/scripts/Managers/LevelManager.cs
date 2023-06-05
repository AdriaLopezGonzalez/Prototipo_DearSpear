using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> enemyTypeList = new List<GameObject>();
    private List<Vector3> enemyPositionList = new List<Vector3>();
    private List<Quaternion> enemyRotationList = new List<Quaternion>();
    //private List<Transform> checkpointsList = new List<Transform>();

    private Vector3 activeCheckpoint;

    public static Action SpearGrab;

    [SerializeField]
    private GameObject player;

    public GameObject baseEnemy;
    public GameObject baseEnemyStanding;
    public GameObject radarEnemy;
    public GameObject dogEnemy;

    public Camera cam;

    private void OnEnable()
    {
        CameraAnimations.Respawn += Respawn;
        CameraAnimations.PlayerStillAlive += PlayerStillAlive;
        SetCheckpoint.SetActiveCheckpoint += SetActiveCheckpoint;
    }

    private void OnDisable()
    {
        CameraAnimations.Respawn -= Respawn;
        CameraAnimations.PlayerStillAlive += PlayerStillAlive;
        SetCheckpoint.SetActiveCheckpoint -= SetActiveCheckpoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("BaseEnemy"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            enemyRotationList.Add(en.transform.rotation);
            enemyTypeList.Add(baseEnemy);
        }
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("BaseEnemy_Standing"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            enemyRotationList.Add(en.transform.rotation);
            enemyTypeList.Add(baseEnemyStanding);
        }
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("RadarEnemy"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            enemyRotationList.Add(en.transform.rotation);
            enemyTypeList.Add(radarEnemy);
        }
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("EnemyWithDog"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            enemyRotationList.Add(en.transform.rotation);
            enemyTypeList.Add(dogEnemy);
        }

        activeCheckpoint = player.transform.position;
    }

    private void Respawn()
    {
        player.transform.position = activeCheckpoint;

        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerInputs>().enabled = true;

        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
        enemyList.Clear();

        for (int i = 0; i < enemyPositionList.Count; i++)
        {
            enemyList.Add(Instantiate(enemyTypeList[i], enemyPositionList[i], enemyRotationList[i]));
        }

        SpearGrab?.Invoke();

        cam.GetComponent<MainCameraMove>().CameraRespawn();
    }

    private void PlayerStillAlive()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<PlayerInputs>().enabled = true;
    }

    private void SetActiveCheckpoint(Transform checkPoint)
    {
        activeCheckpoint = checkPoint.position;
    }
}
