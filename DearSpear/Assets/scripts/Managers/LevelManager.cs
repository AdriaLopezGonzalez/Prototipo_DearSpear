using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject[] enemyArray;

    // Start is called before the first frame update
    void Start()
    {
        enemyArray = new GameObject[GameObject.FindGameObjectsWithTag("Enemy").Length];
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Respawn()
    {

    }
}
