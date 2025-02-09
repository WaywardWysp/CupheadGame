using UnityEngine;

public class CrowProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damageAmount = 1; // Damage
    public float projectileSpeed = 5f; // Speed

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * projectileSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Deal damage
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
        // Check if the projectile hits the ground layer
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destroy the projectile
            Destroy(gameObject);
        }
    }
}


