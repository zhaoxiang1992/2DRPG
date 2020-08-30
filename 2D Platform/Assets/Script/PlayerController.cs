using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    private bool jumpPressed;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumpPressed = true;
            }
            //Flip();
            //Run();
            //Attack();
            CheckGround();
            SwitchAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (GameController.isGameAlive)
        {
            Run();
            jump();
        }
    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                          myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"));
    }

    //void Flip() 
    //{
    //    bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    //    if (playerHasXAxisSpeed) 
    //    {
    //        if (myRigidbody.velocity.x > 0.1f)
    //        {
    //            transform.localRotation = Quaternion.Euler(0,0,0);
    //            //Debug.Log(playerHasXAxisSpeed);
    //        }
    //        if (myRigidbody.velocity.x < -0.1f)
    //        {
    //            transform.localRotation = Quaternion.Euler(0, 180, 0);
    //            //Debug.Log(playerHasXAxisSpeed);
    //        }
    //    }
    //}

    void Run()
    {
        float moveDir = Input.GetAxisRaw("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        if (moveDir != 0)
        {
            transform.localScale = new Vector3(moveDir, 1, 1);
        }
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    void jump()
    {
        if (isGround)
        {
            //myAnim.SetBool("Jump", true);
            //Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            //myRigidbody.velocity = Vector2.up * jumpVel;
            canDoubleJump = true;
        }
        if (jumpPressed && isGround)
        {
            canDoubleJump = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            jumpPressed = false;
            myAnim.SetBool("Jump", true);
        }
        else if (jumpPressed && canDoubleJump && !isGround)
        {
            myAnim.SetBool("DoubleJump", true);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, doubleJumpSpeed);
            canDoubleJump = false;
            jumpPressed = false;
        }
    }

    //void Attack()
    //{
    //    if (Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}
    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }
}
