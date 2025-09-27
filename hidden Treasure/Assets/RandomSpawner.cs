using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject prefab;    // the spawn obj
    public float spawnInterval = 5f; // time interval
    public float minX = 43f;     // low x
    public float maxX = 50f;      // big x
    public float fixedY = 9.6f;    // fixed y
    public float fixedZ = 0f;    // z is always fixed

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandom();
            timer = 0f;
        }
    }

    void SpawnRandom()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, fixedY, fixedZ);

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        obj.transform.rotation = Quaternion.Euler(0, 0, 90);
        Destroy(obj, 2f);
    }
}
