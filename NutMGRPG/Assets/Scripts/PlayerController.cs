using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public float rotationSpeed = 5f;

    public string layerA = "Player"; // Name of the first layer
    public string layerB = "Enemy"; // Name of the second layer

    private Rigidbody2D rb;
    private Quaternion initialRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;

        // Ignore collisions between the player and enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        }

        // Adjustments to collision matrix

        // Get the layer indices
        int layerIndexA = LayerMask.NameToLayer(layerA);
        int layerIndexB = LayerMask.NameToLayer(layerB);

        // Disable collision between the two layers
        Physics2D.IgnoreLayerCollision(layerIndexA, layerIndexB, true);
    }

    private void FixedUpdate()
    {
        // Rotate the camera
        float rotateInput = Input.GetAxisRaw("Rotation");
        cameraTransform.Rotate(Vector3.forward, rotateInput * rotationSpeed * Time.fixedDeltaTime);

        // Calculate movement direction relative to the camera
        Vector3 cameraForward = cameraTransform.up;
        Vector3 cameraRight = cameraTransform.right;

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = cameraForward * moveVertical + cameraRight * moveHorizontal;
        movement.Normalize();

        rb.velocity = movement * speed;

        // Rotate the player sprite to match the camera's rotation
        transform.rotation = cameraTransform.rotation * Quaternion.Inverse(initialRotation);
    }

    private void LateUpdate()
    {
        // Make the camera follow the player
        Vector3 targetPosition = transform.position;
        targetPosition.z = cameraTransform.position.z;
        cameraTransform.position = targetPosition;
    }
}
