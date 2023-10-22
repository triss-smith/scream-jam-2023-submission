using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f; // Detection radius to be set in the Inspector
    public LayerMask playerLayer; // The layer that the player is on

    private GameObject player; // Reference to the player's transform
    private Animator myAnimator; // Reference to the Animator component
    private Light2D _enemySpotlight;
    private AIPlayerDetector _aiPlayerDetector;

    private void Start()
    {
        _aiPlayerDetector = GetComponent<AIPlayerDetector>();
        myAnimator = GetComponent<Animator>(); // Assign the Animator component of the enemy
        _enemySpotlight = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        // Check if the player is within the detection radius

        if (_aiPlayerDetector.playerDetected)
        {
            // Handle what happens when the player is detected (e.g., attack, chase, etc.)
            myAnimator.SetBool("isDetected", true);
            _enemySpotlight.enabled = true;
            Debug.Log("Player detected!");
        }
        else
        {
            myAnimator.SetBool("isDetected", false);
            _enemySpotlight.enabled = false;
        }
    }

    private bool DetectPlayer()
    {
        // Check if the player is within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.transform == player.transform)
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
