using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    private int currentCameraIndex = 0;

    private void Start()
    {
        // Enable the first camera and disable the second camera initially
        SetCameraActive(camera1, true);
        SetCameraActive(camera2, false);
    }

    private void Update()
    {
        // Check if the "X" key is pressed
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Switch the active camera
            currentCameraIndex = (currentCameraIndex + 1) % 2;
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if (currentCameraIndex == 0)
        {
            // Switch to camera1
            SetCameraActive(camera1, true);
            SetCameraActive(camera2, false);
        }
        else if (currentCameraIndex == 1)
        {
            // Switch to camera2
            SetCameraActive(camera1, false);
            SetCameraActive(camera2, true);
        }
    }

    private void SetCameraActive(Camera camera, bool isActive)
    {
        camera.enabled = isActive;
        AudioListener listener = camera.GetComponent<AudioListener>();
        if (listener != null)
        {
            listener.enabled = isActive;
        }
    }
}