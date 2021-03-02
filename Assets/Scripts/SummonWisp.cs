using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWisp : MonoBehaviour {

    public GameObject wisp;
    public GameObject player;

    private GameObject wispClone;
    private bool isSummoned = false;
    private Vector3 pos = new Vector3(0f, 0.5f, 0f);

	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump") && isSummoned == false) //Space
        {
            isSummoned = true;
            //summon wisp
            wispClone = Instantiate(wisp, player.transform.position + pos, player.transform.rotation);
            wispClone.transform.parent = player.transform;
        }
        else if (Input.GetButtonDown("Jump") && isSummoned == true)
        {
            //unsummon wisp
            isSummoned = false;
            Destroy(wispClone);

        }


    }
}
