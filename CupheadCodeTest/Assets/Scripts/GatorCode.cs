using UnityEngine;

public class GatorTrap : MonoBehaviour
{
    [Header("Gator Settings")]
    public float riseHeight = 2f; // How high the gator rises
    public float riseSpeed = 3f; // Speed of rising gator
    public float lowerSpeed = 2f; // Speed of lowering gator
    public float stayUpTime = 2f; // Time the gator stays up
    public float attackInterval = 5f; // Time between attacks
    public int damage = 1; // Damage dealt to the player

    private Vector3 originalPosition;
    private Vector3 raisedPosition;
    private bool isRising = false;
    private bool isLowering = false;
    private bool isActive = false;
    private float timer;

    private void Start()
    {
        originalPosition = transform.position;
        raisedPosition = originalPosition + Vector3.up * riseHeight;
        timer = attackInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !isActive)
        {
            StartCoroutine(ActivateGator());
            timer = attackInterval; // Reset timer for next attack
        }

        if (isRising)
        {
            transform.position = Vector3.MoveTowards(transform.position, raisedPosition, riseSpeed * Time.deltaTime);
            if (transform.position == raisedPosition)
            {
                isRising = false;
                isActive = true;
                Invoke(nameof(StartLowering), stayUpTime);
            }
        }

        if (isLowering)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, lowerSpeed * Time.deltaTime);
            if (transform.position == originalPosition)
            {
                isLowering = false;
                isActive = false;
            }
        }
    }

    private System.Collections.IEnumerator ActivateGator()
    {
        isRising = true;
        yield return null;
    }

    private void StartLowering()
    {
        isLowering = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
