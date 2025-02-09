using UnityEngine;

public class CrowEnemy : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed
    public float patrolRange = 10f; // The move range
    public float minX; // The left boundary
    public float maxX; // The right boundary
    private bool movingRight = true; // Whether the crow is moving right or left

    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // The projectile
    public float projectileCooldown = 2f; // Time between projectiles
    private float nextProjectileTime = 0f; // Timer to track next projectile

    private SpriteRenderer spriteRenderer; // Reference to SpriteRenderer

    private void Start()
    {
        minX = transform.position.x - patrolRange / 2f;
        maxX = transform.position.x + patrolRange / 2f;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    private void Update()
    {
        MoveBetweenBounds();
        DropProjectile();
    }

    private void MoveBetweenBounds()
    {
        // Move the crow
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            // If the crow reaches the right bound, start moving left
            if (transform.position.x >= maxX)
            {
                movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // If the crow reaches the left bound, start moving right
            if (transform.position.x <= minX)
            {
                movingRight = true;
                FlipSprite();
            }
        }
    }

    private void FlipSprite()
    {
        // Flip the sprite by changing the X scale
        transform.localScale = new Vector3(movingRight ? 1 : -1, 1, 1);
    }

    private void DropProjectile()
    {
        // Only drop a projectile if the cooldown is ready
        if (Time.time >= nextProjectileTime)
        {
            // Drop the projectile
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Reset the cooldown
            nextProjectileTime = Time.time + projectileCooldown;
        }
    }
}

