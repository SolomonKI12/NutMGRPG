using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBruteBehaviour : MonoBehaviour
{
    public int health = 100;
    public float baseMovementSpeed = 2f;
    public float detectionDistance = 5f;
    public float stopDistance = 2f;
    public float speedIncreaseAmount = 2f;
    public float speedIncreaseDuration = 2f;
    public float speedIncreaseInterval = 5f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private float currentMovementSpeed;
    private bool isSpeedIncreased;
    private float speedIncreaseTimer;
    private float speedIncreaseIntervalTimer;
    private Vector3 previousDirection;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        currentMovementSpeed = baseMovementSpeed;
        speedIncreaseTimer = 0f;
        speedIncreaseIntervalTimer = speedIncreaseInterval;
        previousDirection = Vector3.zero;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            StopMoving();
        }
        else if (IsPlayerWithinDetectionDistance())
        {
            MoveTowardsPlayer();
        }
        else
        {
            StopMoving();
        }

        UpdateSpeedIncreaseTimers();
    }

    private bool IsPlayerWithinDetectionDistance()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance <= detectionDistance;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance > stopDistance)
        {
            if (!isSpeedIncreased)
            {
                rb.velocity = direction * currentMovementSpeed;
                previousDirection = direction;
            }
            else
            {
                rb.velocity = previousDirection * currentMovementSpeed;
            }
        }
        else
        {
            StopMoving();
        }
    }

    private void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void UpdateSpeedIncreaseTimers()
    {
        if (isSpeedIncreased)
        {
            speedIncreaseTimer += Time.deltaTime;

            if (speedIncreaseTimer >= speedIncreaseDuration)
            {
                ResetMovementSpeed();
            }
        }
        else
        {
            speedIncreaseIntervalTimer += Time.deltaTime;

            if (speedIncreaseIntervalTimer >= speedIncreaseInterval)
            {
                IncreaseMovementSpeed();
            }
        }
    }

    private void IncreaseMovementSpeed()
    {
        currentMovementSpeed = baseMovementSpeed + speedIncreaseAmount;
        isSpeedIncreased = true;
    }

    private void ResetMovementSpeed()
    {
        currentMovementSpeed = baseMovementSpeed;
        speedIncreaseTimer = 0f;
        speedIncreaseIntervalTimer = 0f;
        isSpeedIncreased = false;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out SimpleProjectileMotion damageDealer))
        {
            TakeDamage(damageDealer.damage);
        }
    }
}
