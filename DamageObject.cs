using UnityEngine;

// Make sure you make the object a trigger, or else it wont deal damage

public class DamageObject : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damageAmount = 1;  // Amount of health to remove

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Checks if the player touched the object
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount); // Deals damage
            }
        }
    }
}
