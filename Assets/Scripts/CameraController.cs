using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public Vector3 moveCamRight;
    public Vector3 moveCamLeft;
    public Vector3 moveCamUp;
    public Vector3 moveCamDown;
    public bool isReadyForBossRoom;
    

    private Vector3 playerPosition;
    private new Camera camera;

    void Start()
    {
        camera = this.GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = camera.WorldToViewportPoint(player.transform.position);

        //player is moving near the edge of the screen on...
        if (playerPosition.x > 0.99f) //the right side, so we want to move our camera right 12 units on X
        {
            camera.transform.position = camera.transform.position + moveCamRight;
        }
        else if(playerPosition.x < 0.001f) //the left side, so shift the camera 12 left units on X
        {
            camera.transform.position = camera.transform.position + moveCamLeft;
        }
        else if(playerPosition.y < 0.001f) //the bottom side, so the camera should move 10 units down on Y
        {
            camera.transform.position = camera.transform.position + moveCamDown;
        }
        else if (playerPosition.y > 0.99f) //the top side, so the camera should move 10 units up on Y
        {
            if (isReadyForBossRoom)
                camera.orthographicSize = 8.00f;

            camera.transform.position = camera.transform.position + moveCamUp;
        }

    }
}
