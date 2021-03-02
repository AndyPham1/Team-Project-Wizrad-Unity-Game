using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public bool isBomb;
    public Transform bombEffect;
    private Transform newBombEffect;
    public Transform bombEffect2;
    private Transform newBombEffect2;
    public float radiusEffect;
    private Vector3 targetLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if(enemyHealth != null) enemyHealth.takeDamage(2);

            if(isBomb && this.GetComponent<SpriteRenderer>().enabled)
            {
                targetLocation = this.gameObject.transform.position;
                newBombEffect = Instantiate(bombEffect, this.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(BombDelayedEffect(1f, targetLocation));
                this.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(newBombEffect.gameObject, 1f);
                
            }
            else
            {
                Destroy(gameObject);
            }


        }

    }

    public IEnumerator BombDelayedEffect(float seconds, Vector3 targetLocation)
    {
        yield return new WaitForSeconds(seconds);

        foreach (int i in new int[4])
        {
            Destroy(Instantiate(bombEffect2, targetLocation + new Vector3(Random.insideUnitCircle.normalized.x, Random.insideUnitCircle.normalized.y) * radiusEffect, Quaternion.identity).gameObject, 5f);
        }
        Destroy(this.gameObject,5f);
    }

    private void Awake()
    {
        
        //Destroy(gameObject, 5);
    }
    void DestroyObjectDelayed()
    {
        // Kills the game object in 5 seconds after loading the object
        //Destroy(gameObject, 5);
    }


}
