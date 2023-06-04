using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component

    void Update()
    {
        bool isMoving = Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;
        bool isMouseInBottomHalf = Input.mousePosition.y < Screen.height / 2;

        // Set the animator parameter based on the movement input and mouse cursor position
        animator.SetBool("MovingDown", isMoving && isMouseInBottomHalf);
    }
}
