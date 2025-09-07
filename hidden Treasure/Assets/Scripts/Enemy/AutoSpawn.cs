using UnityEngine;

public class AutoSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;


        int rand = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[rand];

        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
