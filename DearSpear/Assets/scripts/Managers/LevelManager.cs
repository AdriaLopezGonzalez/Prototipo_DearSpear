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
        PlayerCombatCollider.Respawn += Respawn;
    }

    private void OnDisable()
    {
        PlayerCombatCollider.Respawn -= Respawn;
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

        foreach (GameObject enemy in enemyList)
        {
            Destroy(enemy);
            enemyList.Remove(enemy);
        }
        enemyList.Clear();

        for (int i = 0; i < enemyPositionList.Count; i++)
        {
            Instantiate(enemyTypeList[i], enemyPositionList[i], Quaternion.identity);
        }

        foreach (GameObject en in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(en);
        }
    }

    private void SetActiveCheckpoint()
    {

    }
}
