using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    //public const int maxHealth = 20;
    //public int currentHealth = maxHealth;
    public float SpawnDelay = 1f;
    public bool TriggerSpawning = false;
    private bool FirstSpawn = false;
    private float TimeElasped = 0f;
    private Vector3 SpawnOffset = new Vector3 (1/2, 1, 0);

    // Update is called once per frame
    void Update()
    {
        if (TriggerSpawning == true)
        {
            TimeElasped += Time.deltaTime;
            if ((FirstSpawn = false || TimeElasped >= SpawnDelay))
            {
                FirstSpawn = true;
                SpawnEnemy();
                TimeElasped = 0;
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemy.GetComponent<EnemyController>() != null)  enemy.GetComponent<EnemyController>().player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(enemy, this.transform.position + SpawnOffset, this.transform.rotation);
        Instantiate(enemy, this.transform.position - SpawnOffset, this.transform.rotation);
    }
}