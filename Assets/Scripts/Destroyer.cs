using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "SpawnPoint" || other.gameObject.tag == "ClosedRoom")
            Destroy(other.gameObject);
    }
}
