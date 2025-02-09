using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float destroyTime = 2f; // Auto-destroy after 2 seconds

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0; // Prevent falling
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.velocity = direction.normalized * force; // Move in a straight line
        Destroy(gameObject, destroyTime); // Destroy projectile
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Projectile collision with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy")) // Only destroy if hitting "Enemy"
        {
            Destroy(other.gameObject);
        }

        Destroy(gameObject); // Destroy projectile on any collision also tho
    }
}
