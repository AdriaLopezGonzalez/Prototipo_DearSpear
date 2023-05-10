using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    private List<GameObject> enemyList = new List<GameObject>();
    private List<GameObject> enemyTypeList = new List<GameObject>();
    private List<Vector3> enemyPositionList = new List<Vector3>();
    private List<Transform> checkpointsList = new List<Transform>();

    private Vector3 activeCheckpoint;

    [SerializeField]
    private GameObject player;

    public GameObject baseEnemy;
    public GameObject radarEnemy;
    public GameObject dogEnemy;

    public Camera cam;

    private void OnEnable()
    {
        CameraAnimations.Respawn += Respawn;
    }

    private void OnDisable()
    {
        CameraAnimations.Respawn -= Respawn;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject en in PrefabUtility.FindAllInstancesOfPrefab(baseEnemy))
        {
            enemyList.Add(en);
            Debug.Log(en);
            enemyPositionList.Add(en.transform.position);
            enemyTypeList.Add(baseEnemy);
        }
        foreach (GameObject en in PrefabUtility.FindAllInstancesOfPrefab(radarEnemy))
        {
            enemyList.Add(en);
            Debug.Log(en);
            enemyPositionList.Add(en.transform.position);
            enemyTypeList.Add(radarEnemy);
        }
        foreach (GameObject en in PrefabUtility.FindAllInstancesOfPrefab(dogEnemy))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            enemyTypeList.Add(radarEnemy);
        }
        /*foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);

            if(PrefabUtility.FindAllInstancesOfPrefab(baseEnemy) == baseEnemy)
            {
                enemyTypeList.Add(baseEnemy);
            }
            else if(en == radarEnemy)
            {
                enemyTypeList.Add(radarEnemy);
            }
            else
            {
                enemyTypeList.Add(dogEnemy);
            }
        }*/

        foreach (GameObject ch in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            checkpointsList.Add(ch.transform);
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
            Debug.Log("spawneo a " + enemyTypeList[i]);
            Instantiate(enemyTypeList[i], enemyPositionList[i], Quaternion.identity);
        }

        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(en);
        }

        cam.GetComponent<MainCameraMove>().CameraRespawn();
    }

    private void SetActiveCheckpoint()
    {

    }
}
