using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed = 0f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;

    [Header("Ground Check")]
    public float playerHeight = 2;
    private bool grounded;
    public float groundDrag = 4f;

    private float horizontalInput;
    private float verticalInput;

    [Header("Slope")]
    public float maxSlopeAngle = 40f;
    private RaycastHit slopeHit;
    private bool exitingSlope;


    [Header("Crouching")]
    public float crouchSpeed = 2.5f;
    public float crouchScale = 0.5f;
    private bool tryingToUncrouch = false;
    private float startScale;

    [Header("Jumping")]
    public float jumpForce = 6f;
    public float airMultiplier = 0.3f;
    private bool readyToJump = true;
    public float jumpCooldown = 0.5f;

    [Header("Controls")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public movementState state;
    public enum movementState
    {
        walking,
        sprinting,
        air,
        crouching
    }

    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startScale = transform.localScale.y;

    }

    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        stateHandler();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        RaycastHit x;
        grounded = Physics.SphereCast(transform.position, 0.5f, Vector3.down, out x, playerHeight * 0.5f - 0.3f);
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed*5, ForceMode.Force);
            rb.drag = groundDrag;
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Force);
            rb.drag = 0f;
        }

       
        
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flatVel.magnitude > 7)
        {
            Vector3 limitedVel = flatVel.normalized * 7;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Update()
    {
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke("resetJump", jumpCooldown);
        }
        if (Input.GetKeyDown(crouchKey))
        {
            tryingToUncrouch = false;
            transform.localScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 3f, ForceMode.Impulse);
            playerHeight = 1f;
        }
        RaycastHit hit;
        if (Input.GetKeyUp(crouchKey) && !Physics.SphereCast(transform.position, 0.5f, Vector3.up, out hit, 2f))
        {
            tryingToUncrouch = false;
            transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);
            rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            playerHeight = 2f;
        }
        else if (Physics.SphereCast(transform.position, 0.5f, Vector3.up, out hit, 2f))
        {
            tryingToUncrouch = true;
        }
        if (tryingToUncrouch)
        {
            if (!Physics.SphereCast(transform.position, 0.5f, Vector3.up, out hit, 2f) && transform.localScale.y < 1)
            {
                tryingToUncrouch = false;
                transform.localScale = new Vector3(transform.localScale.x, startScale, transform.localScale.z);
                rb.AddForce(Vector3.up * 3f, ForceMode.Impulse);
                playerHeight = 2f;
            }
        }
    }

    public bool OnSlope()
    {
        maxSlopeAngle = 40f;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle <= maxSlopeAngle && angle != 0;
        }
        return false;
    }
    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void resetJump()
    {
        exitingSlope = false;
        readyToJump = true;
    }

    private void stateHandler()
    {
        RaycastHit hit;
        if (Input.GetKey(crouchKey))
        {
            state = movementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if(Physics.SphereCast(transform.position, 1f, Vector3.up, out hit, 2f) && transform.localScale.y < 1)
        {
            state = movementState.crouching;
            moveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = movementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded)
        {
            state = movementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = movementState.air;
        }
    }
}
