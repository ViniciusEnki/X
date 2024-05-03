using UnityEngine;

public class PlayerMovements : MonoBehaviour
{

    CharacterController controller;


    Vector3 velocity;
    Vector3 direction = Vector3.zero;
    Vector3 currentVelocity;

    public float acceleration = 20;
    public float maxSpeed = 13;

    //AAAAAA


    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;


    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 0.8f;
    float timeToMaxHeight = 0.2f;

    void Start()
    {

        controller = GetComponent<CharacterController>();

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 * maxJumpHeight) / timeToMaxHeight;

    }

    void Update()
    {


        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");


        forward = forwardInput * transform.forward;
        strafe = strafeInput * transform.right;

        direction = (forward + strafe).normalized;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            vertical = jumpSpeed * Vector3.up;
        }

        if (vertical.y > 0 && (controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            vertical = Vector3.zero;
        }


        velocity = Vector3.SmoothDamp(velocity, direction * maxSpeed, ref currentVelocity, maxSpeed / acceleration);


        controller.Move((velocity + vertical) * Time.deltaTime);



    }

}