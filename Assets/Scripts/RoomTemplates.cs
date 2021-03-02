using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBossPortal;
    public GameObject bossPortal;

    void Update()
    {
        if(waitTime <= 0 && spawnedBossPortal == false)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1) //when we hit the last room
                {
                    rooms[i].GetComponentInChildren<RoomController>().triggerBoxDisabler(); //disable door locks and enemy spawns in the portal room.

                    Instantiate(bossPortal, rooms[i].transform.position, Quaternion.identity); //should spawn a boss portal here and never again
                    spawnedBossPortal = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

}
