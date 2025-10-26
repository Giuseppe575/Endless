using UnityEngine;
using System.Collections;

/// <summary>
/// Spawna ostacoli proceduralmente nell'endless runner
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefab")]
    [SerializeField] private GameObject obstaclePrefab;
    
    [Header("Spawn Settings")]
    [SerializeField] private float spawnDistance = 50f;
    [SerializeField] private float minSpawnInterval = 1.5f;
    [SerializeField] private float maxSpawnInterval = 3f;
    
    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 3f;
    [SerializeField] private int numberOfLanes = 3;
    
    [Header("Difficulty Settings")]
    [SerializeField] private float difficultyIncreaseRate = 0.1f;
    [SerializeField] private float minInterval = 0.8f;
    
    private float currentSpawnInterval;
    private bool isSpawning = true;
    private float gameTime = 0f;

    void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("ObstacleSpawner: Nessun prefab ostacolo assegnato!");
            return;
        }
        
        StartCoroutine(SpawnObstacles());
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    IEnumerator SpawnObstacles()
    {
        while (isSpawning)
        {
            currentSpawnInterval = Mathf.Lerp(
                maxSpawnInterval, 
                minSpawnInterval, 
                gameTime * difficultyIncreaseRate
            );
            
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minInterval);
            
            yield return new WaitForSeconds(currentSpawnInterval);
            
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        int randomLane = Random.Range(0, numberOfLanes);
        float xPosition = (randomLane - 1) * laneDistance;
        
        Vector3 spawnPosition = new Vector3(xPosition, 0.5f, spawnDistance);
        
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        obstacle.transform.parent = transform;
        
        if (!obstacle.CompareTag("Obstacle"))
        {
            obstacle.tag = "Obstacle";
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    public void ResumeSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnObstacles());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (int i = 0; i < numberOfLanes; i++)
        {
            float xPos = (i - 1) * laneDistance;
            Vector3 start = new Vector3(xPos, 0, 0);
            Vector3 end = new Vector3(xPos, 0, spawnDistance);
            Gizmos.DrawLine(start, end);
        }
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(-laneDistance, 0, spawnDistance),
            new Vector3(laneDistance, 0, spawnDistance)
        );
    }
}