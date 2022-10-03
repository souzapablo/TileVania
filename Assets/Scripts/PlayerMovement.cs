using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float jumpSpeed = 10f;    
    
    [SerializeField]
    float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigigbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    float gravityScaleAtStart;

    void Start()
    {
        myRigigbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigigbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        if (value.isPressed )
        {
            myRigigbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, myRigigbody.velocity.y);
        myRigigbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigigbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigigbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(myRigigbody.velocity.x), 1f);
    }
    
    void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigigbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigigbody.velocity.x, moveInput.y * climbSpeed);
        myRigigbody.velocity = climbVelocity;
        myRigigbody.gravityScale = 0f;
        
        bool playerVericalSpeed = Mathf.Abs(myRigigbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerVericalSpeed);
    }
}
