using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRoomTrigger : MonoBehaviour {

    public Transform target;

    void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(Camera.main.gameObject.transform);

        target.transform.position = new Vector3(
            target.transform.position.x + 10,
            target.transform.position.y,
            target.transform.position.z
            );

        Camera.main.gameObject.transform.position = new Vector3(
            Camera.main.gameObject.transform.position.x + 35,
            Camera.main.gameObject.transform.position.y,
            Camera.main.gameObject.transform.position.z);
    }
}
