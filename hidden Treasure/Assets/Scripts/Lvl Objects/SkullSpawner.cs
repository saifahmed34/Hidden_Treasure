using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    public GameObject skullPrefab;
    public float spawnInterval = 4f;
    public Transform spawnPoint;
    public float moveSpeed = 2f;
    public float lifeTime = 10f;

    public enum Direction { Right, Left, Up, Down }
    public Direction moveDirection = Direction.Right;

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
        GameObject skull = Instantiate(skullPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = skull.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        Vector2 dir = Vector2.zero;
        switch (moveDirection)
        {
            case Direction.Right: dir = transform.right; break;
            case Direction.Left: dir = -transform.right; break;
            case Direction.Up: dir = transform.up; break;
            case Direction.Down: dir = -transform.up; break;
        }

        rb.linearVelocity = dir * moveSpeed;

        Destroy(skull, lifeTime);
    }
}
