using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerMovement : MonoBehaviour
{
    PlayerComponentVfx playervfx;



    float playerHeight = 2f;
    WallRun wallrunScript;

    public Vector3 rbVel;
    [SerializeField] float rbMag;

    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;
    float maxSpeed = 25;
    public Vector3 currentVelocitySave;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    float doubleJumpMult = 2f;
    public int doubleJumps = 1;
    [SerializeField] public bool canDoubleJump = false;
    /*public bool isOnWallRight = false;
    public bool isOnWallLeft = false;*/
    public int giveJumpAfterWall = 0;
    public bool wasOnGroundBefore = false;

    Vector3 playerSpeed;


    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 3f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;

    Collider PlayerCollider;

    public bool isGrounded { get; set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        PlayerCollider = GetComponent<Collider>();
        playervfx = GetComponent<PlayerComponentVfx>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        wallrunScript = GetComponent<WallRun>();
    }

    private void Update()
    {
        maxSpeed = maxSpeed += rb.velocity.magnitude / 1.5f;

        IncreasePlayerMass();

        rbVel = rb.velocity;
        rbMag = rb.velocity.magnitude / 5f;
        currentVelocitySave = rbVel;

        print(isGrounded);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        wasOnGroundBefore = !isGrounded;
        /*isOnWallRight = Physics.CheckSphere(orientation.right, groundDistance + 1, groundMask);
        isOnWallRight = Physics.CheckSphere(-orientation.right, groundDistance + 1, groundMask);*/
        if (wallrunScript.wallLeft == true || wallrunScript.wallRight == true)
        {
            giveJumpAfterWall = 1;
        }

        if (giveJumpAfterWall == 1)
        {
            canDoubleJump = true;
            doubleJumps = 1;
            doubleJump();
        }


        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
            doubleJumps = 1;
        }
        if (isGrounded == false && doubleJumps == 1)
        {
            canDoubleJump = true;
            doubleJump();
        }
        else
        {
            canDoubleJump = false;
            playervfx.showParticle = false;

        }
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }
    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);

        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }


    }

    void doubleJump()
    {
        if (Input.GetKeyDown(jumpKey) && canDoubleJump == true && !isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce + Camera.main.transform.forward * 2, ForceMode.VelocityChange);
            doubleJumps = 0;
            giveJumpAfterWall = 0;
            playervfx.showParticle = true;
        }
    }



    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    public void IncreasePlayerMass()
    {
        if (!isGrounded)
        {
            float timer = 5;
            timer -= 1 * Time.deltaTime;

            if (!isGrounded && timer <= 0)
            {
                Physics.gravity = new Vector3(0, -35, 0);
            }
        }

        else
        {
            Physics.gravity = new Vector3(0, -15, 0);
        }
    }


}