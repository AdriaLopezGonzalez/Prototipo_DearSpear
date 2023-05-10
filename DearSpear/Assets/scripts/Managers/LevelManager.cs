using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(en);
            enemyPositionList.Add(en.transform.position);
            if(en == baseEnemy)
            {
                enemyTypeList.Add(baseEnemy);
            }
            else if(en == radarEnemy)
            {
                enemyTypeList.Add(radarEnemy);
            }
            else
            {
                // AQUI DA ERROR!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                enemyTypeList.Add(dogEnemy);
            }
        }

        foreach (GameObject ch in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            checkpointsList.Add(ch.transform);
        }


        //TEMPORAL
        activeCheckpoint = player.transform.position;
    }

    private void Respawn()
    {
        player.transform.position = activeCheckpoint;

        player.GetComponent<PlayerInputs>().enabled = true;

        /*foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
            enemyList.Remove(enemy);
        }*/
        enemyList.Clear();

        Debug.Log("hay "+enemyPositionList.Count+" enemigos");
        for (int i = 0; i < enemyPositionList.Count; i++)
        {
            Debug.Log("spawneo a " + enemyTypeList[i]);
            Instantiate(enemyTypeList[i], enemyPositionList[i], Quaternion.identity);
        }

        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(en);
        }

        //CAMERA RESET
    }

    private void SetActiveCheckpoint()
    {

    }
}
