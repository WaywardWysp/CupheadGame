using UnityEngine;


// Code for the trampoline, can be used for other things to if you set the bounds right
public class TrampolineCreature : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float smoothTime = 0.3f; // Adds smooth, adds slide
    public float slideFactor = 1.5f; // How much it'll overshoot

    [Header("Movement Bounds")]
    public float leftBound = -5f; // These are the bounds
    public float rightBound = 5f;

    [Header("Bounce Settings")]
    public float bounceForce = 20f; // Makes the trampoline bouncy

    private Transform player;
    private Vector2 velocity = Vector2.zero;
    private float targetX;
    private float overshootX;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // MAKE SURE THE PLAYER HAS THE PLAYER TAG!!!
    }

    void Update()
    {
        if (player != null)
        {
            targetX = Mathf.Clamp(player.position.x, leftBound, rightBound);

            overshootX = Mathf.Lerp(overshootX, targetX, Time.deltaTime * slideFactor);

            Vector2 targetPosition = new Vector2(Mathf.Clamp(overshootX, leftBound, rightBound), transform.position.y);
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null && playerRb.velocity.y <= 0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
            }
        }
    }
}
