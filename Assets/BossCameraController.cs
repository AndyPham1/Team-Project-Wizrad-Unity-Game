using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    private Camera mainCamera;
    private CameraController mainCameraController;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // On start initialize reference to main camera
        mainCameraController = mainCamera.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            mainCameraController.isReadyForBossRoom = true;
            mainCameraController.moveCamUp = mainCameraController.moveCamUp + new Vector3(0, 3, 0);
            this.gameObject.SetActive(false);
        }
    }
}
