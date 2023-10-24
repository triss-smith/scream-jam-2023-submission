using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyDetection : MonoBehaviour
{

    public GameObject player; // Reference to the player's transform
    private Animator myAnimator; // Reference to the Animator component
    public GameObject enemySpotlight;
    private AIPlayerDetector _aiPlayerDetector;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _aiPlayerDetector = GetComponent<AIPlayerDetector>();
        myAnimator = GetComponent<Animator>(); // Assign the Animator component of the enemy
        enemySpotlight.SetActive(false);
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Check if the player is within the detection radius
        
        Vector2 originalEnemyDetectionCollider = _playerMovement.enemyDetectionCollider.GetComponent<CapsuleCollider2D>().size;
        if (_aiPlayerDetector.playerDetected && !_playerMovement._isSneaking)
        {
            // Handle what happens when the player is detected (e.g., attack, chase, etc.)
            myAnimator.SetBool("isCaught", true);
            // _playerMovement.ChangeDetectionColliderSize(40f, 40f);
            enemySpotlight.SetActive(true);
            Debug.Log("Player detected!");
        }
        else
        {
            // _playerMovement.ChangeDetectionColliderSize(1f, 3f);
            myAnimator.SetBool("isDetected", false);
            enemySpotlight.SetActive(false);
        }
    }
}
