using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 7.97f;
    [SerializeField] float jumpForce = 20f;
    public float maxJumpTime = 0.2f;


    public float accelerationSpeed = 3f;
    public float decelerationSpeed = 3f;
    private bool isFacingRight = true;
    public LayerMask groundLayers;
    private bool isSprinting = false;
    private bool isGrounded = true;
    private float jumpTimeCounter;
    public bool isJumping;
    public float maxSneakDuration = 5.0f; // Maximum duration for sneaking in seconds
    public GameObject sneakObject; //
   
    private float currentSpeed;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
  
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

   
    void Update()
    {
        Run();
        isGrounded = Physics2D.IsTouchingLayers(myCapsuleCollider, groundLayers);

        if (isGrounded) {
            jumpTimeCounter = maxJumpTime;  // Reset jump time counter when grounded.
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Jump();
        }

        if (Input.GetButton("Jump") && isJumping) {
            ContinueJump();
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
    }

    void Jump() 
    {
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        isJumping = true;
        StartCoroutine(EndJump());
    }

    void ContinueJump()
    {
        if (isJumping)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }

    IEnumerator EndJump()
    {
        float jumpTimeCounter = 0;
        while (jumpTimeCounter < maxJumpTime && Input.GetButton("Jump"))
        {
            jumpTimeCounter += Time.deltaTime;
            yield return null;
        }
        isJumping = false;
    }

    void FixedUpdate() 
    {
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, Mathf.Clamp(myRigidbody.velocity.y, -decelerationSpeed, decelerationSpeed * 5));
        TurnCheck();
    }

    void OnMove(InputValue value)
    { 
        moveInput = value.Get<Vector2>();   
    }

    void Run()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveSpeedX = runSpeed * moveInputX;

        myRigidbody.velocity = new Vector2(moveSpeedX, myRigidbody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void TurnCheck()
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // if (playerHasHorizontalSpeed)
        // {
        //     transform.localRotation = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        // }
        if (isFacingRight) {             
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        } else {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }

    }
}
