using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour {

    private EnemySpawn[] enemySpawnPointArray;
    private BoxCollider2D[] triggerBoxArray;
    private bool allSpawnsDeleted = false;

    void Start()
    {
        triggerBoxArray = this.gameObject.GetComponents<BoxCollider2D>(); //array of box colliders for this given room
        enemySpawnPointArray = this.GetComponentsInChildren<EnemySpawn>(); //array of enemy spawn points
    }

    void Update()
    {
        var arrayOfEnemies = GameObject.FindGameObjectsWithTag("Enemy"); //get all enemies in the room with you
        if (enemySpawnPointArray[0] == null) //then we know that the spawners are all disabled and we *may* be ready to reopen the doors
        {
            if(arrayOfEnemies.Length == 0)  //then we know all of the enemies are dead, so we should open the door
            {
                foreach (Transform t in transform)
                {
                    //if t.tag is "door" then enable that game object
                    if (t.gameObject.tag == "Door")
                    {
                        t.gameObject.SetActive(false); //maybe play some audio here so the door opening isnt so weird
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //When the player enters a room (hits the trigger box) we begin spawning enemies and shut all doors to the room
    {
        if (other.tag == "Player")
        {
            triggerBoxDisabler();         //disable triggers

            foreach (Transform t in transform)
            {
                //if t.tag is "door" then enable that game object
                if(t.gameObject.tag == "Door")
                {
                    t.gameObject.SetActive(true); //maybe play some audio here as well so the door slamming shut isnt so weird
                }
            }

            //enable all of our spawn points, so that enemies begin to flood in as the doors close behind our player
            for (var i = 0; i < enemySpawnPointArray.Length; i++)
            {
                enemySpawnPointArray[i].TriggerEnemySpawn(); //turn each spawn point on so it starts spitting out enemies; will delete self after spawning x enemies
            }
        }
    }

    public void triggerBoxDisabler ()
    {
        for (var i = 0; i < triggerBoxArray.Length; i++)
        {
            triggerBoxArray[i].enabled = false;
        }
    }
}
