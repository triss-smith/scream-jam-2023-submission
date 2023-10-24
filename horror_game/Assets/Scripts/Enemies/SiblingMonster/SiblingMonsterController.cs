using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiblingMonsterController : MonoBehaviour
{
    public float detectionRange = 5f;
    public float followSpeed = 5f;

    private Vector2 initialPosition;
    private Vector2 horizontalTargetPosition;
    private bool movingRight = true;
    private Transform player;
    private bool isFollowingPlayer = false;
    private PlayerMovement _playerMovement;

    private Animator myAnimator; 
    
    AIPlayerDetector _aiPlayerDetector;
    AIPlayerGameOver _aiPlayerGameOver;

    private void Start()
    {
        _aiPlayerDetector = GetComponent<AIPlayerDetector>();
        initialPosition = transform.position;

        myAnimator = GetComponent<Animator>();

        // Find the player GameObject or tag and assume it's named "Player"
        player = GameObject.FindWithTag("Player").transform;
        _playerMovement = player.GetComponent<PlayerMovement>();
        _aiPlayerGameOver = GetComponent<AIPlayerGameOver>();
    }

    private void Update()
    {
        if (isFollowingPlayer && _aiPlayerDetector.playerDetected && !_playerMovement._isSneaking)
        {
            FollowPlayer();
        }
        
        if (_aiPlayerDetector.playerDetected && _playerMovement._isSneaking)
        {
            myAnimator.SetBool("isHesitating", true);
        }
        
        else
        {
            myAnimator.SetBool("isHesitating", false);
            myAnimator.SetBool("isRunning", false);
            DetectPlayer();
        }
        
       
        
        
        if (_aiPlayerGameOver.playerHit && !_playerMovement._isSneaking)
        {
            myAnimator.SetBool("isAttacking", true);
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void DetectPlayer()
    {
        // Check if the player is within the detection range
        myAnimator.SetBool("isRunning", false);
        if (Vector2.Distance(player.position, transform.position) < detectionRange)
        {
            isFollowingPlayer = true;
        }
    }

    private void FollowPlayer()
    {
        myAnimator.SetBool("isRunning", true);        // Move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);

        // Ensure the sprite is facing the correct direction
        if (player.position.x < transform.position.x)
        {
            FlipSprite(true); // Face left when following the player to the left
        }
        else
        {
            FlipSprite(false); // Face right when following the player to the right
        }
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

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        
    }
}
