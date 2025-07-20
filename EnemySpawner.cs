// Manages enemy spawns
using UnityEngine;


// this is currently going to manage all enemy spawns, however it could make sense 
public class EnemySpawner: MonoBehaviour
{


    public int enemyCount; // current enemies alive
    public int maxEnemyCount; // max number of enemies
    public GameObject enemyPrefab; // for now just one that we'll spawn in
    public float spawnDistance; // how far away from the player we spawn, could use min and max spawn distances instead
    public static EnemySpawner Instance; // for enemies to call into when they die, however could instead have enemies remember which spawner spawned them should we do multiple
    public float spawnRadius = 60f; // how many +/- degrees from directly in front of the user enemies can spawn

    // strategies for enemy spawns

    // 1: create an equation to establish percentages by time/wave and then spawn based on those

    // 2: max point value with maybe a modifier for enemy type based on scenario, then spawn randomly up to set max point value
    // e.g slime is 1 point, evil wizard is 5. stat maxIndividual at 1 and maxTotal at 10 so you get 10 slimes 

    void Awake() {
        Instance = this;

        // generate a spawn distance that's off screen
        Camera mainCamera = Camera.main;
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
        spawnDistance = Mathf.Max(screenWidth, screenHeight) * 0.5f;
    }


    void Update() {
        if (enemyCount < maxEnemyCount) {
            // logic for picking which enemy to spawn
            Debug.Log("Spawning in Slime");

            Vector3 spawnPos = GetRandomSpawnPosition();

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemyCount++;

            // set enemy health, damage, maybe other modifiers. That logic could go in the enemy itself

        }
    }

    private Vector3 GetRandomSpawnPosition() {
        Vector3 playerPos = Player.Instance.location.position;

        // Calculate a random angle within spawnRadius degrees from North (0 degrees)
        // North is 0 degrees, so we want angles between (0 - spawnRadius) and (0 + spawnRadius)
        float randomAngle = Random.Range(-spawnRadius, spawnRadius);
        
        // Convert angle to radians
        float angleInRadians = randomAngle * Mathf.Deg2Rad;

                // Calculate spawn position using trigonometry
        // North is (0, 1) in 2D, so we rotate from that direction
        float x = Mathf.Sin(angleInRadians) * spawnDistance;
        float y = Mathf.Cos(angleInRadians) * spawnDistance;
        
        Vector3 spawnPosition = playerPos + new Vector3(x, y, 0);
        
        return spawnPosition;
        
    }


}