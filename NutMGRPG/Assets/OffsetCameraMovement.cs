using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetCameraMovement : MonoBehaviour
{
    public Transform parentTransform; // Reference to the parent transform (game object A)
    public float yOffset; // Y-coordinate offset applied to the parent's position

    private Camera mainCamera;
    public Camera alternateCamera; // Reference to the alternate camera

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleCamera();
        }

        if (parentTransform != null)
        {
            // Match the position and rotation of the parent transform with the y-coordinate offset
            Vector3 newPosition = parentTransform.position;
            newPosition.y += yOffset;
            transform.position = newPosition;
            transform.rotation = parentTransform.rotation;
        }
        else
        {
            Debug.LogWarning("Parent transform is not assigned!");
        }
    }

    private void ToggleCamera()
    {
        if (mainCamera != null && alternateCamera != null)
        {
            mainCamera.enabled = !mainCamera.enabled;
            alternateCamera.enabled = !alternateCamera.enabled;
        }
    }
}

