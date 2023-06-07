using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component
    public DeathKnightBehaviour deathKnightBehaviour; // Reference to the DeathKnightBehaviour script

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0f || verticalInput != 0f;
        bool isMouseInBottomHalf = Input.mousePosition.y < Screen.height / 2;
        bool isMovingDown = verticalInput < 0f;
        bool isMovingUp = verticalInput > 0f;
        bool isFiring = Input.GetMouseButton(0) || Input.GetMouseButton(1);

        bool movingDown = (isMovingDown && !isFiring) || (isMoving && isFiring && isMouseInBottomHalf);
        bool movingUp = (isMovingUp && !isFiring) || (isMoving && isFiring && !isMouseInBottomHalf);

        bool isAlive = deathKnightBehaviour.currentHealth > 0; // Check if player is alive based on current health

        // Set the animator parameters based on the input
        animator.SetBool("MovingDown", movingDown);
        animator.SetBool("MovingUp", movingUp);
        animator.SetBool("IsAlive", isAlive);
    }
}
