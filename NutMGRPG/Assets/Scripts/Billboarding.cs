using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    public Camera mainCamera;
    public Transform cameraTransform;
    public SpriteRenderer spriteRenderer;
    public int bottomHalfSortingOrder = 2;
    public int topHalfSortingOrder = -2;

    private void Update()
    {
        // Apply the rotation of the camera to the object's sprite
        transform.rotation = cameraTransform.rotation;

        // Get the position of the sprite relative to the camera viewport
        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the sprite's y position is in the bottom half of the screen
        if (viewportPosition.y <= 0.5f)
        {
            // Set the sorting order to the bottom half value
            spriteRenderer.sortingOrder = bottomHalfSortingOrder;
        }
        else
        {
            // Set the sorting order to the top half value
            spriteRenderer.sortingOrder = topHalfSortingOrder;
        }
    }
}
