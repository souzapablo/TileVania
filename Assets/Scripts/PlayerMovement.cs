using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    Vector2 moveInput;
    Rigidbody2D myRigigbody;
    void Start()
    {
        myRigigbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed, myRigigbody.velocity.y);

        myRigigbody.velocity = moveInput;
    }
}
