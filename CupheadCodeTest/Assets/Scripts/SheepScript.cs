using UnityEngine;

public class SheepScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f; // Speed
    public float patrolRange = 8f; // Total distance the sheep will move
    private float minX; // Left boundary
    private float maxX; // Right boundary
    private bool movingRight = true; // Movement direction

    private SpriteRenderer spriteRenderer; // To flip the sprite

    private void Start()
    {
        // Calculate patrol boundaries
        minX = transform.position.x - patrolRange / 2f;
        maxX = transform.position.x + patrolRange / 2f;

        // Get the sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        MoveBetweenBounds();
    }

    private void MoveBetweenBounds()
    {
        // Move the sheep
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            // If the sheep reaches the right boundary, change direction
            if (transform.position.x >= maxX)
            {
                movingRight = false;
                FlipSprite();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // If the sheep reaches the left boundary, change direction
            if (transform.position.x <= minX)
            {
                movingRight = true;
                FlipSprite();
            }
        }
    }

    private void FlipSprite()
    {
        // Flip the sprite when changing direction
        spriteRenderer.flipX = !movingRight;
    }
}
