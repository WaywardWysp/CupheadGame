using UnityEngine;

public class WispSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject wispPrefab; // MAKE SURE THE WISP IS A PREFAB
    public Transform spawnPoint; // The point where wisps will spawn
    public float spawnInterval = 3f; // Time interval between spawning wisps
    public int maxWispCount = 5; // Max number of wisps

    private float spawnTimer; // Timer
    private int currentWispCount; // Counter

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        // If the timer reaches zero, spawn a wisp
        if (spawnTimer <= 0f && currentWispCount < maxWispCount)
        {
            SpawnWisp();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnWisp()
    {
        // Instantiate the wisp
        GameObject wisp = Instantiate(wispPrefab, spawnPoint.position, Quaternion.identity);

        // Make the wisp knows which spawner created it
        WispEnemy wispScript = wisp.GetComponent<WispEnemy>();
        if (wispScript != null)
        {
            wispScript.spawner = this;
        }

        currentWispCount++; // Increment the wisp counter
    }

    public void WispDestroyed()
    {
        currentWispCount = Mathf.Max(0, currentWispCount - 1); // Prevent negative values
    }
}
