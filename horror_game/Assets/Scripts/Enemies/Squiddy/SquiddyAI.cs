using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquiddyAI : MonoBehaviour
{
    public float horizontalPatrolDistance = 5f;
    public float horizontalPatrolSpeed = 2f;
    public float verticalPatrolMinDistance = 1f; // Min vertical distance
    public float verticalPatrolMaxDistance = 3f; // Max vertical distance
    public float verticalPatrolSpeed = 1f;
    public float detectionRange = 5f;
    public float followSpeed = 5f;

    private Vector2 initialPosition;
    private Vector2 horizontalTargetPosition;
    private Vector2 verticalTargetPosition;
    private bool movingRight = true;
    private float verticalPatrolTimer = 0f; // Timer for vertical movement
    private float verticalPatrolDuration; // Random duration for vertical movement
    private Transform player;
    private bool isFollowingPlayer = false;

    private void Start()
    {
        initialPosition = transform.position;
        horizontalTargetPosition = initialPosition + Vector2.right * horizontalPatrolDistance;
        RandomizeVerticalPatrol();

        // Find the player GameObject or tag and assume it's named "Player"
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
            DetectPlayer();
            MoveVertically();
        }
    }

    private void Patrol()
    {
        // Horizontal patrol
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, horizontalTargetPosition, horizontalPatrolSpeed * Time.deltaTime);
            FlipSprite(true); // Face right when moving right
            if (Vector2.Distance((Vector2)transform.position, horizontalTargetPosition) < 0.1f)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, horizontalPatrolSpeed * Time.deltaTime);
            FlipSprite(false); // Face left when moving left
            if (Vector2.Distance((Vector2)transform.position, initialPosition) < 0.1f)
            {
                movingRight = true;
            }
        }
    }

    private void DetectPlayer()
    {
        // Check if the player is within the detection range
        if (Vector2.Distance(player.position, transform.position) < detectionRange)
        {
            isFollowingPlayer = true;
        }
    }

    private void FollowPlayer()
    {
        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        // Ensure the sprite is facing the correct direction
        if (player.position.x < transform.position.x)
        {
            FlipSprite(false); // Face left when following the player to the left
        }
        else
        {
            FlipSprite(true); // Face right when following the player to the right
        }
    }

    private void MoveVertically()
    {
        verticalPatrolTimer += Time.deltaTime;

        if (verticalPatrolTimer >= verticalPatrolDuration)
        {
            // Randomize vertical movement duration and direction
            RandomizeVerticalPatrol();
            verticalPatrolTimer = 0;
        }

        // Vertical patrol
        transform.position = Vector2.MoveTowards(transform.position, verticalTargetPosition, verticalPatrolSpeed * Time.deltaTime);
    }

    private void RandomizeVerticalPatrol()
    {
        // Randomize vertical movement duration between min and max values
        verticalPatrolDuration = Random.Range(verticalPatrolMinDistance, verticalPatrolMaxDistance);

        // Randomize the vertical target position
        verticalTargetPosition = new Vector2(transform.position.x, initialPosition.y + Random.Range(-verticalPatrolDuration, verticalPatrolDuration));
    }

    private void FlipSprite(bool faceRight)
    {
        if (faceRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Draw Gizmos for patrol paths and detection range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(initialPosition, horizontalTargetPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(initialPosition, verticalTargetPosition);
    }
}
