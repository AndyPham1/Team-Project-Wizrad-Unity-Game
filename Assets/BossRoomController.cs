using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomController : MonoBehaviour
{
    private GameObject door;
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        door = this.gameObject.transform.GetChild(0).gameObject;
        boss = this.gameObject.transform.GetChild(1).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            door.SetActive(true);
            boss.SetActive(true); //Maybe we can make this more ceremonious in the future
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
