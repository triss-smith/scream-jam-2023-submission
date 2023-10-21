using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedEyeUp : MonoBehaviour
{
    public float detectionRadius = 5f; // Detection radius to be set in the Inspector
    public LayerMask playerLayer; // The layer that the player is on

    private Transform player; // Reference to the player's transform
    private Animator myAnimator; // Reference to the Animator component

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assuming the player is tagged as "Player"
        myAnimator = GetComponent<Animator>(); // Assign the Animator component of the enemy
    }

    private void Update()
    {
        // Check if the player is within the detection radius
        bool playerDetected = DetectPlayer();

        if (playerDetected)
        {
            // Handle what happens when the player is detected (e.g., attack, chase, etc.)
            myAnimator.SetBool("isCaught", true);
            Debug.Log("Player detected!");
        }
        else
        {
            myAnimator.SetBool("isCaught", false);
        }
    }

    private bool DetectPlayer()
    {
        // Check if the player is within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform == player)
            {
                // The player is within the detection radius
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a gizmo to visualize the detection radius in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
