using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CupheadPlayer : MonoBehaviour
{
    // Please do not mess with the coyote mechanics

    [Header("Movement Settings")]
    public float moveSpeed = 8f; // Most of these are named pretty obviously
    public float acceleration = 10f;
    public float deceleration = 12f;
    public float airControl = 0.6f;

    [Header("Jump Settings")]
    public float jumpForce = 8f; // The jump force
    public float jumpCutMultiplier = 0.5f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Gravity Settings")]
    public float normalGravity = 1.6f; // Default gravity
    public float fallGravityMultiplier = 1f; // Increases gravity when falling
    public float maxFallSpeed = -20f; // Limits fall speed

    [Header("Ground Detection")]
    public LayerMask groundLayer; // You need to create a layer for the ground and assign it here
    public float groundCheckOffset = 0.1f;
    public float groundCheckThickness = 0.1f;

    private Rigidbody2D rb;
    private Collider2D collider;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    private float coyoteCounter;
    private float jumpBufferCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rb.gravityScale = normalGravity; // Set default gravity
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y - collider.bounds.extents.y - groundCheckOffset),
            new Vector2(collider.bounds.size.x * 0.9f, groundCheckThickness),
            0f,
            groundLayer
        );

        if (isGrounded)
        {
            // Coyote is a sort of jump forgiveness window
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteCounter > 0)
        {
            Jump();
            jumpBufferCounter = 0;
        }

        if (Input.GetButtonUp("Jump") && isJumping)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDifference = targetSpeed - rb.linearVelocity.x;
        float accelerationRate = isGrounded ? acceleration : acceleration * airControl;
        float moveForce = Mathf.Abs(speedDifference) * accelerationRate;

        rb.AddForce(Vector2.right * moveForce * Mathf.Sign(speedDifference));

        if (moveInput == 0 && isGrounded)
        {
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, deceleration * Time.fixedDeltaTime), rb.linearVelocity.y);
        }
    }

    private void ApplyGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = normalGravity * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }

        if (rb.linearVelocity.y < maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
        }
      
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isJumping = true;
        coyoteCounter = 0;
    }

    private void OnDrawGizmosSelected()
    {
        if (collider != null)
        {
            Gizmos.color = Color.red;
            Vector2 groundCheckCenter = new Vector2(transform.position.x, transform.position.y - collider.bounds.extents.y - groundCheckOffset);
            Vector2 groundCheckSize = new Vector2(collider.bounds.size.x * 0.9f, groundCheckThickness);
            Gizmos.DrawWireCube(groundCheckCenter, groundCheckSize);
        }
    }
}