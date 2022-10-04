using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Cam;
    public MarioController Player;
    public float CameraHeight = 0.5f;

    public float MoveCameraMargin = 0.90f;

    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Player = FindObjectOfType<MarioController>();
    }
    public void FocusCameraOnPlayer()
    {
        Cam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + CameraHeight, gameObject.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player != null)
        {
            Vector3 ScreenPosition = Cam.WorldToViewportPoint(Player.transform.position);
            if (ScreenPosition.x > MoveCameraMargin)
            {
                float PositionToBeMoved = Mathf.Abs(ScreenPosition.x - MoveCameraMargin);
                Cam.transform.position = new Vector3(Cam.transform.position.x + PositionToBeMoved, transform.position.y, transform.position.z);
            }
            else if (ScreenPosition.x < 1 - MoveCameraMargin)
            {
                float PositionToBeMoved = Mathf.Abs(ScreenPosition.x - (1 - MoveCameraMargin));
                Cam.transform.position = new Vector3(Cam.transform.position.x - PositionToBeMoved, transform.position.y, transform.position.z);
            }
        }
    }
}
