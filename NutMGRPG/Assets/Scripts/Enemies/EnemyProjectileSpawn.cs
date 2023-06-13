using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawn : MonoBehaviour
{
    public GameObject projectilePrefab; // The prefab of the projectile to spawn
    public float spawnRate = 0.5f; // The rate at which projectiles are spawned (in seconds)
    public float spawnDistanceThreshold = 10f; // The distance threshold for spawning projectiles

    private float nextSpawnTime; // The time when the next projectile can be spawned
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRate; // Set the initial spawn time
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform
    }

    private void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time >= nextSpawnTime && IsWithinSpawnDistance())
        {
            SpawnProjectile();
            nextSpawnTime = Time.time + spawnRate; // Update the next spawn time
        }
    }

    private bool IsWithinSpawnDistance()
    {
        // Calculate the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Return true if the distance is within the specified threshold
        return distance <= spawnDistanceThreshold;
    }

    private void SpawnProjectile()
    {
        // Instantiate a new projectile
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
