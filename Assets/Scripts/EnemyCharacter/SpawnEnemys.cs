using System.Collections.Generic;
using UnityEngine;

// spawn points are all child objects
public class SpawnEnemys : MonoBehaviour
{


    public float timeToSpawnEnemy;
    private Transform[] spawnPoints;
    
    //the list used by the player to check all enemies
    [HideInInspector]public List<Transform> enemys;
    public GameObject enemyPrefab;
    private bool isSpawning = true;
    

    public void GameOver()
    {
        isSpawning = false;
    }

    
    void Spawn()
    {
        int rand = Random.Range(0, spawnPoints.Length);
        enemys.Add(Instantiate(enemyPrefab, spawnPoints[rand].position,Quaternion.identity).transform);
    }
    
    void Start()
    {
        
        spawnPoints=transform.GetComponentsInChildren<Transform>();
        Spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsInvoking("Spawn")&&isSpawning)
        {
            Invoke("Spawn", timeToSpawnEnemy); 
        } 
    }
}
