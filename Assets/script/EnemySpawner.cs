using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject enemyPrefab;

    [Tooltip("Time in seconds between each spawn")]
    public float spawnInterval = 5f;

    [Tooltip("Radius around this object where enemies can spawn")]
    public float spawnRadius = 10f;

    [Tooltip("Maximum number of enemies allowed at the same time")]
    public int maxEnemies = 10;

    private int currentEnemyCount = 0;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Spawner: No prefab assigned!");
            return; // Stop here if no prefab
        }

        // Validate NavMeshAgent presence on prefab to warn user early
        if (enemyPrefab.GetComponent<NavMeshAgent>() == null)
        {
            Debug.LogWarning("Enemy Spawner: The enemy prefab doesn't have a NavMeshAgent. It might not move.");
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            // Wait before trying to spawn
            yield return new WaitForSeconds(spawnInterval);

            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        // 1. Get a random point inside a sphere
        Vector3 randomPoint = Random.insideUnitSphere * spawnRadius;

        // 2. Add it to the spawner's position to make it relative to the world
        Vector3 spawnTarget = transform.position + randomPoint;

        // 3. Find the nearest valid point on the NavMesh
        // 2.0f is the max distance to search from the random point
        if (NavMesh.SamplePosition(spawnTarget, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            // 4. Instantiate the enemy at the valid NavMesh position
            GameObject newEnemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);

            // Track the enemy count (optional: simple tracking)
            currentEnemyCount++;

            // Optional: Detect when enemy dies to decrease count
            // This requires the enemy to have a HealthSystem!
            HealthSystem health = newEnemy.GetComponent<HealthSystem>();
            if (health != null)
            {
                // Unsubscribe first to be safe, then subscribe
                health.OnDeath.AddListener(OnEnemyDeath);
            }
        }
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0) currentEnemyCount = 0;
    }

    // Draw the spawn area in the Editor for easy debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
