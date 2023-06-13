using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMotion : MonoBehaviour
{
    public float speed = 5f; // The constant speed at which the projectile moves
    public float maxDistance = 10f; // The maximum distance the projectile can travel from its spawn position

    public int damage = 5; // Damage dealt to enemies by projectile

    private Vector3 spawnPosition; // The position where the projectile was spawned
    private float distanceTraveled; // The distance the projectile has traveled

    private void Start()
    {
        spawnPosition = transform.position;
        distanceTraveled = 0f;

        // Get the initial position of the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Calculate the direction based on the difference between the projectile's position and the player's position
        Vector3 direction = (player.transform.position - transform.position).normalized;

        // Rotate the projectile to point towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply the velocity to the projectile with a constant speed
        Rigidbody2D projectileRb = GetComponent<Rigidbody2D>();
        projectileRb.velocity = direction * speed;

        // Disable collisions between the projectile and objects tagged as "Enemy"
        Collider2D projectileCollider = GetComponent<Collider2D>();
        Collider2D[] enemyColliders = GameObject.FindObjectsOfType<Collider2D>();

        foreach (Collider2D enemyCollider in enemyColliders)
        {
            if (enemyCollider.gameObject.CompareTag("Enemy"))
            {
                Physics2D.IgnoreCollision(projectileCollider, enemyCollider);
            }
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
        // Check if the collision does not belong to objects tagged as "Enemy"
        if ((!collision.gameObject.CompareTag("Enemy")) && (!collision.gameObject.CompareTag("Projectile")))
        {
            // Destroy the projectile game object
            Destroy(gameObject);
        }
    }
}