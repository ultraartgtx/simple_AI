using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeToSpawnEnemy;
    private Transform[] spawnPoints;
    public List<Transform> enemys;
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
    void Update()
    {
        if(!IsInvoking("Spawn")&&isSpawning)
        {
            Invoke("Spawn", timeToSpawnEnemy); //выполняем sec каждые 5 секунд
        } 
    }
}
