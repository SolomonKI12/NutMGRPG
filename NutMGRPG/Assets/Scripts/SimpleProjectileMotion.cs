using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectileMotion : MonoBehaviour
{
    public float speed = 5f; // The constant speed at which the projectile moves
    public float maxDistance = 10f; // The maximum distance the projectile can travel from its spawn position

    public int damage = 5; //damage dealt to enemies by projectile

    private Vector3 spawnPosition; // The position where the projectile was spawned
    private float distanceTraveled; // The distance the projectile has traveled

    private void Start()
    {
        spawnPosition = transform.position;
        distanceTraveled = 0f;

        // Get the initial position of the player's cursor
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z; // Maintain the same z position as the projectile

        // Calculate the direction based on the difference between the projectile's position and the target position
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Apply the velocity to the projectile with a constant speed
        Rigidbody2D projectileRb = GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * speed;

        // Disable collisions between the projectile and the player
        Collider2D projectileCollider = GetComponent<Collider2D>();
        Collider2D[] playerColliders = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Collider2D>();

        foreach (Collider2D playerCollider in playerColliders)
        {
            Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        }
    }

    private void Update()
    {
        // Calculate the distance traveled by the projectile
        distanceTraveled = Vector3.Distance(transform.position, spawnPosition);

        // Check if the distance traveled exceeds the maximum distance
        if (distanceTraveled >= maxDistance)
        {
            // Destroy the projectile game object
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision does not belong to the player
        if ((collision.gameObject.tag != "Player") && (!collision.gameObject.CompareTag("Projectile")))
        {
            // Destroy the projectile game object
            Destroy(gameObject);
        }
    }
}