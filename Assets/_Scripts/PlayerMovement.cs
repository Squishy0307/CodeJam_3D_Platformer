using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walksSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float airSpeed;
    [SerializeField] float groundDrag;
    [SerializeField] float inAirDrag;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] int MaxNumberOfJumps = 2;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float airMultiplier;

    private float moveSpeed;
    private float coyoteTimeCounter;
    bool readyToJump = true;
    int doubleJumpCounter = 1;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    //Landing
    private bool landed = true;

    [Header("Events")]
    [SerializeField] UnityEvent onLand;
    [SerializeField] UnityEvent onJump;
    

    private bool onMovingPlatform;
    private bool canApplyDownwardForce;

    [SerializeField] Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDir;
    Rigidbody rb;

    public MovementState movementState;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        //ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f), Color.green);

        MovementInput();
        SpeedControl();
        movementStateHandler();

        //handle drag
        if (isGrounded)
        {
            rb.drag = groundDrag;

            if (!landed)
            {
                landed = true;
                doubleJumpCounter = 1;
                onLand.Invoke();
            }

            if (readyToJump)
                coyoteTimeCounter = coyoteTime;
        }
        else
        {
            landed = false;
            rb.drag = inAirDrag;
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void Landed()
    {
        Debug.Log("Broken Ankle");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void movementStateHandler()
    {
        // Sprinting
        if (isGrounded && Input.GetKey(sprintKey))
        {
            movementState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        //Walking
        else if (isGrounded)
        {
            movementState = MovementState.walking;
            moveSpeed = walksSpeed;
        }

        //in air
        else
        {
            movementState = MovementState.air;
            moveSpeed = airSpeed;
        }
    }

    void MovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && coyoteTimeCounter > 0)
        {
            readyToJump = false;

            if(doubleJumpCounter >= MaxNumberOfJumps)
                coyoteTimeCounter = 0;

            doubleJumpCounter++;
            jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void MovePlayer()
    {
        //calculate movement direction
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        else if (onMovingPlatform)
        {
            //Vector3 targetVel = velocity + (moveDir.normalized * 5 * moveSpeed);
            //rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref refVel, 0.001f);

            rb.AddForce(moveDir.normalized * moveSpeed * 10, ForceMode.Force);

            if (canApplyDownwardForce && rb.velocity.y < 0)
            {
                rb.AddForce(Vector3.down * 0.05f, ForceMode.Force);
            }

        }

        //on ground
        else if (isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10, ForceMode.Force);
        }
        //in air
        else if (!isGrounded)
        {
            rb.AddForce(moveDir.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
        }

        //turn of gravity when on slope so character no longer slides on the slope
        if(!onMovingPlatform)
            rb.useGravity = !OnSlope();

    }

    void SpeedControl()
    {
        //limit speed on slope
        if ((OnSlope() && !exitingSlope))
        {
            if(rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        //limiting speed on ground or in air
        else
        {

            Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            //limit the velocity if it goes higher then moveSpeed variable
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
            }
        }
    }

    void jump()
    {
        exitingSlope = true;
        onMovingPlatform = false;
        onJump.Invoke();

        //reset y velocity so it will always jump the exact same height
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit,playerHeight* 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    public void movingPlatformHandler(bool onPlatform, bool applyDownwardForce)
    {
        onMovingPlatform = onPlatform;
        canApplyDownwardForce = applyDownwardForce;
    }
} 
