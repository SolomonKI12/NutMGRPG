using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlueGoblinBehaviour : MonoBehaviour
{
    public int health = 100; // Initial health of the enemy

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
