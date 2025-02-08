using UnityEngine;
using TMPro; // TextMeshPro, you know how this works

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;  // Maximum health
    private int currentHealth;  // Current health

    [Header("UI Settings")]
    public TextMeshProUGUI healthText; // Reference to the text

    [Header("Damage Settings")]
    public float invincibilityTime = 1f;  // I-Frames, time before the player can take damage again
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;  // Set health to full
        UpdateHealthUI();  // Initialize
    }

    // Call this when the player takes damage
    public void TakeDamage(int damageAmount = 1)
    {
        if (isInvincible) return;  // Prevents multiple hits

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI(); // Call update

        StartCoroutine(InvincibilityPeriod());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Update the UI text with the current health
    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth; // Display health
        }
        if (PlayerPrefs.GetInt("HardMode", 0) == 1)
        {
            maxHealth = 1;
        }
        else if (PlayerPrefs.GetInt("EasyMode", 0) == 1)
        {
            maxHealth = 30;
        }
        else
        {
            maxHealth = 3;
        }

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // This is just a place for future game over screen
    }

    private System.Collections.IEnumerator InvincibilityPeriod()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }
}
