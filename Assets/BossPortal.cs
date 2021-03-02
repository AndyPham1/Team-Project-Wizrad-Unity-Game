using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    private GameObject portalExit;
    public bool isInLecternRoom = false;

    // Start is called before the first frame update
    void Start()
    {
        portalExit = GameObject.Find("BossPortalExit");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            //move player to adjoining portal
            other.gameObject.transform.position = portalExit.transform.position;

            // Update the boolean value to be true so the player can change their spell setup
            isInLecternRoom = true;

            //When the player exits out the other side, close the portal
            portalExit.GetComponent<Animator>().Play("PortalClose");
            //Destroy(portalExit.gameObject);
        }
    }

}
