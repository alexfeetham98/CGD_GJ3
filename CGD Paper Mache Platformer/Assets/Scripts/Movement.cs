using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    //object
    public Transform cam;
    CharacterController cc;
    public Transform player;
    CapsuleCollider cap;

    //input
    public Vector2 input;

    //Physics
    Vector3 intent;
    public Vector3 velocityXZ;
    public Vector3 velocity;
    public float playerSpeed = 8;
    public float playerAcceleration = 11;
    float turnSpeed = 5;
    float minTurnSpeed = 15;
    float maxTurnSpeed = 40;
    float jumpTimer = 0.0f;


    public bool headCollision = false;

    //gravity
    public float gravityMagnitude = -9.81f;
    public bool grounded = false;
    float terminalVelocity = -30f;

    //jump
    public float jumpForce = 10f;
    public float jumpForceTwo = 10f;
    public float airTimer = 0.0f;
    public bool jumped = false;
     public int numJumps = 1;
    int jumpCounter = 0;
    float DoubleJumpTimer = 0f;
    bool doubleJumped = false;
    public bool killEnemy = false;
    //powerups
   // public bool doubleJump = false;


    //camera
    Vector3 camF;
    Vector3 camR;


    float ts;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        cap = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        getInput();
        calculateCameraPosition();
        calculateGround();
        movement();
        addGravity();

        jump();
        jumpCollision();

        cc.Move(velocity * Time.deltaTime);
    }

    void getInput()
    {

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        minTurnSpeed = 15;
        maxTurnSpeed = 40;
        
        input = Vector2.ClampMagnitude(input, 1);
    }

    void calculateCameraPosition()
    {

        camF = cam.forward;
        camR = cam.right;

        //ignore angle of camera for movement
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;


    }

    void calculateGround()
    {


        if (cc.isGrounded && !killEnemy)
        {

            grounded = true;
            jumped = false;

            if (airTimer > 0.3)
            {
                airTimer = 0;
            }
        }
        else
        {
            grounded = false;
            airTimer += Time.deltaTime;
        }
    }

    void movement()
    {


        //moving relative to camera direction

        intent = camF * input.y + camR * input.x;

        ts = velocity.magnitude / playerSpeed;
        turnSpeed = Mathf.Lerp(maxTurnSpeed, minTurnSpeed, ts);

        if (input.magnitude > 0)
        {
            Quaternion rot = Quaternion.LookRotation(intent);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }


        velocityXZ = velocity;
        velocityXZ.y = 0;
        velocityXZ = Vector3.Lerp(velocityXZ, transform.forward * input.magnitude * playerSpeed, playerAcceleration * Time.deltaTime);
        velocity = new Vector3(velocityXZ.x, velocity.y, velocityXZ.z);

    }

    void addGravity()
    {
        if (grounded)
        {
            velocity.y = -5f;

        }
        else
        {
            velocity.y += gravityMagnitude * Time.deltaTime;
            velocity.y = Mathf.Clamp(velocity.y, terminalVelocity, 20);
        }

    }

    void jump()
    {
        if (grounded)
        {
            jumpCounter = 0;
        }

        if (doubleJumped)
        {
            DoubleJumpTimer += Time.deltaTime;
        }

        if (DoubleJumpTimer > 0.75)
        {
            DoubleJumpTimer = 0f;
            doubleJumped = false;
        }


        if (jumpCounter < numJumps)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jumped = true;

                if (jumpCounter == 0)
                {
                    velocity.y = jumpForce;
                }
                else
                {
                    velocity.y = jumpForceTwo;
                    doubleJumped = true;
                }

                jumpCounter++;
            }


        }

        if (cc.collisionFlags == CollisionFlags.Above)
        {
            jumpTimer = 0f;
            jumped = false;
        }

        if (jumped)
        {
            jumpTimer += Time.deltaTime;
        }

    }

    void jumpCollision()
    {
        if (cc.collisionFlags == CollisionFlags.Above)
        {
            print("Touching Ceiling!");
            if (!grounded && !Input.GetButtonDown("Jump") && velocity.y > 0)

            {

                velocity.y = -velocity.y * 0.2f;

            }
        }
    }

}
