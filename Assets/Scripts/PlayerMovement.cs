using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    Vector2 moveInput;
    Rigidbody2D myRigigbody;
    Animator myAnimator;

    void Start()
    {
        myRigigbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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
}
