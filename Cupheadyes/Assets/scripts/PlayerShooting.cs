using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float spawnOffsetY = 1f; // Make it to where it shoots from the right height, and not at Cup's feet

    void Update()
    {
        Vector2 shootDirection = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            shootDirection = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            shootDirection = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            shootDirection = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            shootDirection = Vector2.right;

        if (shootDirection != Vector2.zero)
            Shoot(shootDirection);
    }

    void Shoot(Vector2 direction)
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, spawnOffsetY, 0); // Spawn abt right height
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.Launch(direction, projectileSpeed);

        // Ignore collision between the projectile and the player
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D projectileCollider = projectile.GetComponent<Collider2D>();

        if (playerCollider != null && projectileCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, projectileCollider);
        }
    }

}
