using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballExplosion : MonoBehaviour
{
    public GameObject explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "SpawnPoint" && collision.gameObject.tag != "PlayerProjectile" && collision.gameObject.tag != "DoorTrigger")
        {
            // instantiate boom
            Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.takeDamage(2);
            Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
            // instantiate boom
            Destroy(gameObject);
        }
    }
}
