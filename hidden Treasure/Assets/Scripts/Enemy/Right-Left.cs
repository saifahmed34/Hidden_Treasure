using UnityEngine;

public class Right_Left : MonoBehaviour
{
    public float speed = 5f;
    public GameObject left;
    public GameObject right;
    public GameObject circle;
    private SpriteRenderer spriteRenderer;
    private bool movingRight = true; // Track movement direction

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (circle == null || left == null || right == null) return;

        float step = speed * Time.deltaTime; // Ensure smooth movement

        // Check distance instead of direct comparison
        if (Vector3.Distance(circle.transform.position, left.transform.position) < 0.1f)
        {
            movingRight = true;
            spriteRenderer.flipX = false;

        }
        else if (Vector3.Distance(circle.transform.position, right.transform.position) < 0.1f)
        {
            movingRight = false;
            spriteRenderer.flipX = true;
        }

        // Move towards the target position
        Vector3 targetPosition = movingRight ? right.transform.position : left.transform.position;
        circle.transform.position = Vector3.MoveTowards(circle.transform.position, targetPosition, step);
    }
}
