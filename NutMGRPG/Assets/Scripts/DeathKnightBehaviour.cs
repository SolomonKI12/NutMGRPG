using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathKnightBehaviour : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the player
    public int currentHealth; // Current health of the player

    private void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            int damageAmount = collision.gameObject.GetComponent<EnemyProjectileMotion>().damage;

            TakeDamage(damageAmount); // Deduct health based on the projectile's damage
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die(); // Player has run out of health
        }
    }

    private void Die()
    {
        // Implement your own logic for player death
        // For example, you might display a game over screen, restart the level, etc.
        Debug.Log("Player died");
    }
}
