using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawn : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile to spawn
    public float spawnRate = 0.5f; // The rate at which projectiles are spawned (in seconds)

    private float nextSpawnTime; // The time when the next projectile can be spawned

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRate; // Set the initial spawn time
    }

    private void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time >= nextSpawnTime)
        {
            SpawnProjectile();
            nextSpawnTime = Time.time + spawnRate; // Update the next spawn time
        }
    }

    private void SpawnProjectile()
    {
        // Instantiate a new projectile
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
