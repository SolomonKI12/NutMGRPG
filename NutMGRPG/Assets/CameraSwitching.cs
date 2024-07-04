using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitching : MonoBehaviour
{
    public Camera mainCamera;
    public Camera childCamera;

    private void Start()
    {
        // Disable the child camera initially
        childCamera.enabled = false;
    }

    private void Update()
    {
        // Check for the "X" key press
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Toggle the cameras
            mainCamera.enabled = !mainCamera.enabled;
            childCamera.enabled = !childCamera.enabled;
        }
    }
}
