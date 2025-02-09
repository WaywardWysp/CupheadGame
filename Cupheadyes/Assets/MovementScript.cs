using UnityEngine;

public class CupheadPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 12f;
    public float airControl = 0.6f;

    [Header("Jump Settings")]
    public float jumpForce = 8f;
    public float jumpCutMultiplier = 0.5f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    [Header("Gravity Settings")]
    public float normalGravity = 1.6f;
    public float fallGravityMultiplier = 1f;
    public float maxFallSpeed = -20f;

    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public float groundCheckOffset = 0.1f;
    public float groundCheckThickness = 0.1f;

    [Header("Crouch Settings")]
    public float crouchHeightMultiplier = 0.5f;
    private bool isCrouching = false;
    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip jumpSound;

    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Animator anim;
    private float moveInput;
    private bool isGrounded;
    private bool isJumping;
    private float coyoteCounter;
    private float jumpBufferCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = normalGravity;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        originalColliderSize = collider.size;
        originalColliderOffset = collider.offset;
    }

    private void Update()
    {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            Crouch();
        }
        else
        {
            StandUp();
        }

        if (!isCrouching)
        {
            if (Input.GetKey(KeyCode.A)) moveInput = -1f;
            if (Input.GetKey(KeyCode.D)) moveInput = 1f;
        }

        isGrounded = Physics2D.OverlapBox(
            new Vector2(transform.position.x, transform.position.y - collider.bounds.extents.y - groundCheckOffset),
            new Vector2(collider.bounds.size.x * 0.9f, groundCheckThickness),
            0f,
            groundLayer
        );

        if (isGrounded)
        {
            coyoteCounter = coyoteTime;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isCrouching)
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

        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            }
            isJumping = false;
        }

        anim.SetBool("isRunning", moveInput != 0 && !isCrouching);
        anim.SetBool("isJumping", !isGrounded);
        anim.SetBool("isCrouching", isCrouching);

        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (!isCrouching)
        {
            HandleMovement();
        }
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float accelerationRate = isGrounded ? acceleration : acceleration * airControl;
        float moveForce = Mathf.Abs(speedDifference) * accelerationRate;

        rb.AddForce(Vector2.right * moveForce * Mathf.Sign(speedDifference));

        if (moveInput == 0 && isGrounded)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.fixedDeltaTime), rb.velocity.y);
        }
    }

    private void ApplyGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = normalGravity * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = normalGravity;
        }

        if (rb.velocity.y < maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        coyoteCounter = 0;
        jumpBufferCounter = 0;

        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void Crouch()
    {
        if (!isCrouching)
        {
            isCrouching = true;
            collider.size = new Vector2(originalColliderSize.x, originalColliderSize.y * crouchHeightMultiplier);
            collider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y * crouchHeightMultiplier);
        }
    }

    private void StandUp()
    {
        if (isCrouching)
        {
            isCrouching = false;
            collider.size = originalColliderSize;
            collider.offset = originalColliderOffset;
        }
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

