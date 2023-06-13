using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the damage text moves up
    public float destroyDelay = 1f; // Delay before destroying the damage text

    private TextMeshPro textMeshPro;
    private Transform cameraTransform;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        // Set the sorting layer to overlay and the sorting order to a high value
        textMeshPro.sortingLayerID = SortingLayer.NameToID("Overlay");
        textMeshPro.sortingOrder = 999;
    }

    private void Update()
    {
        // Calculate the movement direction towards the top of the screen
        Vector3 moveDirection = cameraTransform.up;

        // Move the damage text upwards
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Calculate the rotation to face the camera
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - cameraTransform.position, cameraTransform.up);

        // Apply the rotation
        transform.rotation = targetRotation;
    }

    public void SetText(string text)
    {
        textMeshPro.text = text;

        // Destroy the damage text after a delay
        Destroy(gameObject, destroyDelay);
    }
}