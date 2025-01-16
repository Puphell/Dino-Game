using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacleList; // Obstacle prefablardan olu�an liste
    public Transform spawnPoint;
    public float initialSpawnInterval = 2f;
    private float currentSpawnInterval;
    private float elapsedTime = 0f;
    private const float MinSpawnInterval = 1f; // Minimum spawn s�resi

    private void Start()
    {
        if (obstacleList == null || obstacleList.Length == 0)
        {
            Debug.LogError("Obstacle list is empty! Please assign obstacles in the inspector.");
            return;
        }

        currentSpawnInterval = initialSpawnInterval;
        InvokeRepeating(nameof(SpawnObstacle), 0f, currentSpawnInterval);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 30f)
        {
            elapsedTime = 0f;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval / 2f, MinSpawnInterval); // Minimum spawn s�resini kontrol et
            CancelInvoke(nameof(SpawnObstacle));
            InvokeRepeating(nameof(SpawnObstacle), 0f, currentSpawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, obstacleList.Length);
        GameObject selectedObstacle = obstacleList[randomIndex];

        Instantiate(selectedObstacle, spawnPoint.position, Quaternion.identity);
    }
}
