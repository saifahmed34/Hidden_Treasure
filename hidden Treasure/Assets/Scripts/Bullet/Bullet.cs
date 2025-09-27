using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public int damage = 1;
    public bool isFromCannon2 = false; // Set true for cannon2 bullets

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // auto-destroy after time
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir, float spd = 10f, bool fromCannon2 = false)
    {
        direction = dir.normalized;
        speed = spd;
        isFromCannon2 = fromCannon2;
        gameObject.tag = "Bullet";
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("box"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}