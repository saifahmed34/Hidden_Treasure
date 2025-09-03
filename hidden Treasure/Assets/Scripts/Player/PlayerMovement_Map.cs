using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement_Map : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D Rigidbody2D;
    public float jumpForce = 7f;
    public bool isGrounded = false;
    [SerializeField]
    private Animator _Animator;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    public void move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        string currentScene = SceneManager.GetActiveScene().name;

        // If player is inside level scenes, restrict to horizontal movement
        if (currentScene == "Level1" || currentScene == "Level2" || currentScene == "Level3" || currentScene == "LastLevel")
        {
            transform.Translate(new Vector2(hor, 0) * speed * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                isGrounded = false;
            }
            Rigidbody2D.gravityScale = 1;
        }
        else // In map scene, allow free movement
        {
            transform.Translate(new Vector2(hor, ver) * speed * Time.deltaTime);

        }
        if (hor != 0 || ver != 0)
        {
            _Animator.SetBool("IsMoving", true);
        }
        else
        {
            _Animator.SetBool("IsMoving", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            // When player touches ground, allow jumping again
            isGrounded = true;
        }
    }
}

