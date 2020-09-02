using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float climbSpeed;
    public float restoreTime;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    private bool jumpPressed;
    private bool isOnwayPlatform;

    private bool isLadder;
    private bool isClimbing;

    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;

    private float playerGravity;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
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
            CheckLadder();
            CheckAirStatus();
            OnWayPlatformCheck();
            SwitchAnimation();
        }
    }

    private void FixedUpdate()
    {
        if (GameController.isGameAlive)
        {
            Run();
            jump();
            Climb();
        }
    }

    void CheckGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                          myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                          myFeet.IsTouchingLayers(LayerMask.GetMask("OnewayPlatform"));
        isOnwayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OnewayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
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

    void Climb()
    {
        if (isLadder)
        {
            float moveY = Input.GetAxis("Vertical");
            if (moveY > 0.5f || moveY < -0.5)
            {
                myAnim.SetBool("Climbing", true);
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
            }
            else if (myRigidbody.velocity.x != 0)
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    myAnim.SetBool("Climbing",false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
                else
                {
                    myAnim.SetBool("Climbing", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climbing", false);
            myRigidbody.gravityScale = playerGravity;
        }


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

    void OnWayPlatformCheck()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        //if (Input.GetButtonDown("Vertical") && Input.GetButtonDown("jump"))
        //{
        //    gameObject.layer = LayerMask.NameToLayer("OnewayPlatform");
        //    Invoke("RestorePlayerLayer", restoreTime);
        //}

        float moveY = Input.GetAxisRaw("Vertical");
        if (isOnwayPlatform && moveY < -0.1f /*&& Input.GetButtonDown("jump")*/)
        {
            gameObject.layer = LayerMask.NameToLayer("OnewayPlatform");
            Invoke("RestorePlayerLayer", restoreTime);
        }
    }

    void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirStatus()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climbing");
    }
}
