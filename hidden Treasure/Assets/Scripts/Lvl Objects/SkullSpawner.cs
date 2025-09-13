using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    public GameObject skullPrefab;   // skull prefab
    public float spawnInterval = 4f; // spawning time interval
    public Transform spawnPoint;     // spawning possision
    public float moveSpeed = 2f;     // skull movement speed
    public float lifeTime = 10f;     // destroy after that time

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnSkull();
            timer = 0f;
        }
    }

    void SpawnSkull()
    {
        // skull initialization
        GameObject skull = Instantiate(skullPrefab, spawnPoint.position, Quaternion.identity);

        // skull movement
        Rigidbody2D rb = skull.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        if (rb != null)
        {
            rb.linearVelocity = transform.right * moveSpeed;
            // -moveSpeed for left
        }

        // destroy object
        Destroy(skull, lifeTime);
    }
}
