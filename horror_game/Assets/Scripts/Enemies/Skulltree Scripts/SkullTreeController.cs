using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTreeController : MonoBehaviour
{
    public float moveSpeed = 3.0f;      // Speed at which the enemy moves.
    public float detectionRange = 5.0f; // Range at which the enemy detects the player. 
    
    private Transform playerTransform;           // Reference to the player's Transform.
    public GameObject playerObject;
    private Collider2D playerCollider;

    private AIPlayerDetector playerDetector;

    private Collider2D enemyCollider;

    private Vector3 initialPosition;    // Initial position of the enemy.
    private bool isChasing = false;     // Flag to check if the enemy is chasing.
    private Animator animator;          // Reference to the enemy's animator (if you're using one).
     
    
    // Add any other variables or references as needed.
    
    void Start()
    {
        
        playerTransform = playerObject.transform; 
        initialPosition = transform.position;
        // Initialize the animator if you're using one.
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        playerDetector = GetComponent<AIPlayerDetector>();
    }

    void Update()
    {

        // Check if the player is within the detection range.
        if (playerDetector.playerDetected )
        {
            isChasing = true;

            // Calculate the direction to the player.
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float desiredX = transform.position.x + directionToPlayer.x * moveSpeed * Time.deltaTime;


            // Move the enemy in the direction of the player.
            transform.position =  new Vector3(desiredX, transform.position.y, transform.position.z);

            // Ensure the enemy faces the player.
            if (directionToPlayer.x > 0)
            {
                // If the player is on the right side, flip the enemy to face right.
                Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
            }
            else if (directionToPlayer.x < 0)
            {
                // If the player is on the left side, flip the enemy to face left.
                Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotator);
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

    // Draw the detection range using Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}