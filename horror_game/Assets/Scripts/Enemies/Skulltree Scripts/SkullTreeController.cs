using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTreeController : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // Speed at which the enemy moves.
    public float detectionRange = 5.0f; // Range at which the enemy detects the player.
    
    private Transform player;           // Reference to the player's Transform.
    private Vector3 initialPosition;    // Initial position of the enemy.
    private bool isChasing = false;     // Flag to check if the enemy is chasing.
    private Animator animator;          // Reference to the enemy's animator (if you're using one).
    
    // Add any other variables or references as needed.
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Make sure to tag your player GameObject as "Player."
        initialPosition = transform.position;
        
        // Initialize the animator if you're using one.
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range.
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;

            // Calculate the direction to the player.
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Move the enemy in the direction of the player.
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

            // Ensure the enemy faces the player.
            if (directionToPlayer.x > 0)
            {
                // If the player is on the right side, flip the enemy to face right.
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (directionToPlayer.x < 0)
            {
                // If the player is on the left side, flip the enemy to face left.
                transform.localScale = new Vector3(1, 1, 1);
            }
            
            // Optional: Update the animator parameters to control animations (if using an animator).
            // animator.SetFloat("Speed", moveSpeed);
        }
        else
        {
            isChasing = false;

            // If the enemy is not chasing, it can return to its initial position.
            ReturnToInitialPosition();
            
            // Optional: Update the animator parameters to control animations (if using an animator).
            // animator.SetFloat("Speed", 0);
        }
    }

    void ReturnToInitialPosition()
    {
        // Calculate the direction to the initial position.
        Vector3 directionToInitialPosition = (initialPosition - transform.position).normalized;

        // Move the enemy back to its initial position.
        transform.position += directionToInitialPosition * moveSpeed * Time.deltaTime;
    }
}