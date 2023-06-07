using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBruteBehaviour : MonoBehaviour
{
    public int health = 100; // Initial health of the enemy
    public float movementSpeed = 2f; // Movement speed of the goblin
    public float detectionDistance = 5f; // Distance threshold for detecting the player
    public float stopDistance = 2f; // Distance threshold for stopping movement towards the player
    public float chargeChance = 0.1f; // Chance of charging towards the player (10%)

    private Transform playerTransform; // Reference to the player's transform
    private Rigidbody2D rb; // Reference to the goblin's Rigidbody2D component

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void Update()
    {
        // Check if the player is within the detection distance
        if (IsPlayerWithinDetectionDistance())
        {
            if (Random.value < chargeChance)
            {
                ChargeTowardsPlayer();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            StopMoving();
        }
    }

    private bool IsPlayerWithinDetectionDistance()
    {
        // Calculate the distance between the goblin and the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Return true if the player is within the detection distance
        return distance <= detectionDistance;
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Calculate the distance to the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Move towards the player using Rigidbody2D if the distance is greater than the stop distance
        if (distance > stopDistance)
        {
            rb.velocity = direction * movementSpeed;
        }
        else
        {
            StopMoving();
        }
    }

    private void ChargeTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Charge towards the player by applying a higher velocity using Rigidbody2D
        rb.velocity = direction * (movementSpeed * 2f); // Increase the movement speed for charging
    }

    private void StopMoving()
    {
        // Stop the goblin's movement by setting the velocity to zero
        rb.velocity = Vector2.zero;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            // Enemy is defeated, you can add your own logic here
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision has a damage variable
        if (collision.gameObject.TryGetComponent(out SimpleProjectileMotion damageDealer))
        {
            // Reduce the enemy's health by the damage amount
            TakeDamage(damageDealer.damage);
        }
    }
}
