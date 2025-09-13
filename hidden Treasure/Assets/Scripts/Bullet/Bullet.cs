using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 1;

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto-destroy after time
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            /*        // Damage enemy
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy != null)
                    {*/
            /*         enemy.TakeDamage(damage);
                 }*/
            Destroy(collision.gameObject);
            Destroy(gameObject); // Destroy on hit
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }

}
