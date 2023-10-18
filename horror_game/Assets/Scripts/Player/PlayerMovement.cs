using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    public bool Sprinting = false;
    public int Stamina = 10;
    public bool StaminaRegen = false;

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
        Debug.Log("dialog active" + DialogBehavior.textActive);
        if (!DialogBehavior.textActive) {
            Run();
            FlipSprite();
        }
    }

    void OnMove(InputValue value)
    { 
        moveInput = value.Get<Vector2>();   
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) == false)
        {
            myRigidbody.velocity = new Vector2(moveInput.x * walkSpeed, myRigidbody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && StaminaRegen == false)
        {
            myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
            Sprinting = true;
            Sprinting1();
        }

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
    void Sprinting1 ()
    {
        if (Sprinting == true)
        {
            StartCoroutine(Sprinting2 ());
        }
    }

    IEnumerator Sprinting2 ()
    {
        yield return new WaitForSeconds(1);
        Stamina -= 1;
        Sprinting1();
    }

    void StaminaRegen1 ()
    {
        if (Stamina == 10)
        {
            StaminaRegen = false;
        }
        else if (StaminaRegen == true)
        {
            StartCoroutine(StaminaRegen2 ());
        }
    }

    IEnumerator StaminaRegen2 ()
    {
        yield return new WaitForSeconds(1);
        Stamina += 1;
        StaminaRegen1();
    }
}
