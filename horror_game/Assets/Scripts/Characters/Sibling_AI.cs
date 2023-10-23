using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sibling_AI : MonoBehaviour
{
    public float walkingDistance = 5.0f; // The distance the NPC will walk.
    public float walkingSpeed = 2.0f;    // The speed at which the NPC will walk.
    public float detectionWidth = 5.0f;  // The width of the rectangular detection zone.
    public float detectionHeight = 2.0f; // The height of the rectangular detection zone.

    private Vector3 initialPosition;
    private Vector3 leftTargetPosition;
    private Vector3 rightTargetPosition;
    private bool isWalking = false;
    Animator myAnimator;

    private void Start()
    {
        initialPosition = transform.position;
        leftTargetPosition = initialPosition - new Vector3(walkingDistance, 0, 0);
        rightTargetPosition = initialPosition + new Vector3(walkingDistance, 0, 0);
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 targetPosition;

        if (isWalking)
        {
            targetPosition = leftTargetPosition;
            transform.localScale = new Vector3(-1, 1, 1); // Flip the NPC to face right
        }
        else
        {
            targetPosition = initialPosition;
            transform.localScale = new Vector3(1, 1, 1); // Flip the NPC to face left
        }

        // Move the NPC towards the target position.
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkingSpeed * Time.deltaTime);
        myAnimator.SetBool("isRunning", true);
    }

    private void FixedUpdate()
    {
        // Check if the player is within the rectangular detection zone.
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(detectionWidth, detectionHeight), 0);
        bool playerInRange = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                playerInRange = true;
                break;
            }
        }

        isWalking = playerInRange;
    }

    // Visualize the rectangular detection zone in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(detectionWidth, detectionHeight, 1));
    }
}
