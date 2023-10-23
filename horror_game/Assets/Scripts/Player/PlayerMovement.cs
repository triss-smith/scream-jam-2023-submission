using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 7.97f;
    private bool _isFacingRight = true;
    private float _currentSpeed;

    
    [Header("Jumping")]
    public LayerMask groundLayers;
    public float jumpForce = 20f;
    public float decelerationSpeed = 3f;
    private bool _isGrounded = true;
    private float _jumpTimeCounter;
    public bool isJumping;
    public float maxJumpTime = 0.2f;
    
    [Header("Sneaking")]
    public GameObject relic;
    public bool _isSneaking = false;
    public bool _sneakDepleted = false;
    public int maxSneakDuration = 5;
    public float sneakSpeed = 2.0f;
    public float sneakRechargeRate = 1.0f;
    public bool _sneakIsRecharging = false;
    public int sneakTimeCounter;
    public GameObject enemyDetectionCollider;

    public GameObject GameOverObject;
    public CapsuleCollider2D playerCollider;
    
    
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    CircleCollider2D myCircleCollider;
    
  
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myCircleCollider = GetComponent<CircleCollider2D>();
        sneakTimeCounter = maxSneakDuration;
        playerCollider = GameOverObject.GetComponent<CapsuleCollider2D>();
    }
   
    void Update()
    {
        // Run();
        relic.SetActive(_isSneaking);
        // enemyDetectionCollider.SetActive(true);

        if (sneakTimeCounter <= 0)
        {
            sneakTimeCounter = 0;
            _sneakDepleted = true;
        }

        if (sneakTimeCounter == maxSneakDuration)
        {
            _sneakDepleted = false;
        }
        
        _isGrounded = Physics2D.IsTouchingLayers(myCapsuleCollider, groundLayers);

        if (_isGrounded) {
            _jumpTimeCounter = maxJumpTime;  // Reset jump time counter when grounded.
            isJumping = false;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded) {
            Jump();
        }

        if (Input.GetButton("Jump") && isJumping) {
            ContinueJump();
        }

        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
        }
        
        if (Input.GetButton("Sneak") && !_sneakDepleted)
        {
            StopCoroutine("RechargeSneak");
            _isSneaking = true;
            StartCoroutine(StartSneak());
        }
        if (Input.GetButtonUp("Sneak")) {
            StopCoroutine("StartSneak");
            _isSneaking = false;
            _sneakIsRecharging = true;
            StartCoroutine(RechargeSneak());
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
            myAnimator.SetBool("isJumping", true);
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
        myAnimator.SetBool("isJumping", false);
    }
    
    IEnumerator StartSneak()
    {
        if (_isSneaking && Input.GetButtonDown("Sneak") && sneakTimeCounter > 0)
        {
            while (sneakTimeCounter > 0 && _isSneaking)
            {
                sneakTimeCounter--;
                yield return new WaitForSeconds(1);
            }
        }
    }
    
    IEnumerator RechargeSneak()
    {
        
        while(sneakTimeCounter < maxSneakDuration && _sneakIsRecharging)
        {
            sneakTimeCounter++;
            yield return new WaitForSeconds(sneakRechargeRate);
        }

        _sneakIsRecharging = false;
        
    }

    public void ChangeDetectionColliderSize(float x, float y)
    {
        enemyDetectionCollider.GetComponent<CapsuleCollider2D>().size = new Vector3(x, y, 1);
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
        float moveSpeedX = (_isSneaking ? sneakSpeed : runSpeed) * moveInputX;

        myRigidbody.velocity = new Vector2(moveSpeedX, myRigidbody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void TurnCheck()
    {
        if (moveInput.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && _isFacingRight)
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
        if (_isFacingRight) {             
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        } else {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }

    }
}
