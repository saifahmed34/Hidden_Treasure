using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestOpen : MonoBehaviour
{
     public Sprite newSprite;         // new image
    public float waitTime = 5f;      // time to end level

    public GameObject objectToShow; // the object to show

    private SpriteRenderer spriteRenderer;
    private bool triggered = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (objectToShow != null)
        {
            objectToShow.SetActive(false); // disable object
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.CompareTag("Player"))
        {
            triggered = true;

            // change image
            spriteRenderer.sprite = newSprite;

            // show object
            if (objectToShow != null)
            {
                objectToShow.SetActive(true);
            }

            // movement script
            PlayerMovement_Map move = collision.gameObject.GetComponent<PlayerMovement_Map>();
            if (move != null)
            {
                move.enabled = false;
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = Vector2.zero; // freeze player
            }

            // invoke waitTime
            Invoke(nameof(GoToNextScene), waitTime);
        }
    }

    void GoToNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1); // next scene
    }
}
