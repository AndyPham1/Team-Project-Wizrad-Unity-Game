using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int SpawnLimit = 5;
    public float SpawnDelay = 1f;
    public bool TriggerSpawning = false;
    private bool FirstSpawn = false;
    private float TimeElasped = 0f;
    private int NumberSpawned = 0;

    public bool TriggerEnemySpawn()
    {
        return (TriggerSpawning = true);
    }

    public bool StopEnemySpawn()
    {
        return (TriggerSpawning = false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerSpawning == true)
        {
            TimeElasped += Time.deltaTime;
            if ((FirstSpawn = false || TimeElasped >= SpawnDelay) && NumberSpawned < SpawnLimit)
            {
                FirstSpawn = true;
                SpawnEnemy();
                TimeElasped = 0;
                NumberSpawned++;
            }

            if (SpawnLimit <= NumberSpawned)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemy.GetComponent<EnemyController>() != null) enemy.GetComponent<EnemyController>().player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(enemy, this.transform.position, this.transform.rotation);
    }
}