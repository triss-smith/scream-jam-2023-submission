using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    public float accelerationSpeed = 3f;
    public float decelerationSpeed = 3f;
    // public LayerMask groundLayers;
    private bool isSprinting = false;
    public int stamina = 10;
    private bool staminaRegen = false;
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
            FlipSprite();
            if(Input.GetButtonDown("Jump")) {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
                Debug.Log("Jump"); 

                // if (myCapsuleCollider.IsTouchingLayers(groundLayers))
                // {

                // }
            }
    }

    void OnMove(InputValue value)
    { 
        moveInput = value.Get<Vector2>();   
    }

    void Run()
    {
        float moveInputX = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            currentSpeed = currentSpeed = Mathf.MoveTowards(currentSpeed, runSpeed, accelerationSpeed * Time.deltaTime);
        }
        else
        {
            isSprinting = false;
            currentSpeed = currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, accelerationSpeed * Time.deltaTime);
        }

        float moveSpeedX = currentSpeed * moveInputX;

        myRigidbody.velocity = new Vector2(moveSpeedX, myRigidbody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
    void isSprinting1 ()
    {
        if (isSprinting == true)
        {
            StartCoroutine(isSprinting2 ());
        }
    }

    IEnumerator isSprinting2 ()
    {
        yield return new WaitForSeconds(1);
        stamina -= 1;
        isSprinting1();
    }

    void staminaRegen1 ()
    {
        if (stamina == 10)
        {
            staminaRegen = false;
        }
        else if (staminaRegen == true)
        {
            StartCoroutine(staminaRegen2 ());
        }
    }

    IEnumerator staminaRegen2 ()
    {
        yield return new WaitForSeconds(1);
        stamina += 1;
        staminaRegen1();
    }
}
