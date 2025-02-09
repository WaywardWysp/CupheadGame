using UnityEngine;

// Please make sure that this has 2 box colliders, one needs to be a trigger
// Also make sure the collider is thick enough
public class MovingBoat : MonoBehaviour
{
    public float targetXPosition; // The position the boat moves to
    public float speed = 2f; // Movement speed
    private Vector2 startPosition;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            Vector2 targetPosition = new Vector2(targetXPosition, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
}
