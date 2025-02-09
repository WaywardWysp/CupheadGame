using UnityEngine;

public class WispEnemy : MonoBehaviour
{
    [Header("Wisp Settings")]
    public float moveSpeed = 2f; // Speed of the wisp
    public float bobbingHeight = 0.5f; // Height it will bob
    public float bobbingSpeed = 1f; // Speed of bob
    public int damage = 1; // Damage, obv
    public LayerMask wallLayer; // Make sure to set the wall layer

    private Vector3 startPosition; // Starting position
    private float bobbingTimer = 0f;
    public WispSpawner spawner; // Reference to spawner

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        BobbingMovement();
        MoveLeft(); // Move left
    }

    private void MoveLeft()
    {
        // Move left pt 2
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void BobbingMovement()
    {
        // This is the bob, it just moves up and down
        bobbingTimer += Time.deltaTime * bobbingSpeed;
        float bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, startPosition.y + bobbingOffset, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Did wisp hit player?
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            // Destroys the wisp
            DestroySelf();
        }

        // Did wisp hit wall?
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            // Destroys the wisp
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        if (spawner != null)
        {
            spawner.WispDestroyed();
        }
        // Destroys the wisp
        Destroy(gameObject);
    }
}

