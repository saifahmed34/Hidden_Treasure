using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement_Map : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 7f;
    public float flySpeed = 5f;

    [Header("Shooting")]
    public Transform firePoint; // assign your empty FirePoint in Inspector

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool isGrounded = false;
    private int groundContacts = 0;
    private bool isFlying = false;

    // Track facing direction
    public bool facingRight = true;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        string currentScene = SceneManager.GetActiveScene().name;

        // -------- LEVEL SCENES (Platformer movement) --------
        if (currentScene == "tutorial" || currentScene == "Lvl uno" ||
            currentScene == "Level3" || currentScene == "LastLevel")
        {
            if (!isFlying)
            {
                _rigidbody2D.linearVelocity = new Vector2(hor * speed, _rigidbody2D.linearVelocity.y);

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                _rigidbody2D.linearVelocity = new Vector2(hor * speed, flySpeed);
            }

            if (Input.GetKeyDown(KeyCode.G) && isGrounded)
            {
                isFlying = !isFlying;

                if (isFlying)
                {
                    _rigidbody2D.gravityScale = 0f;
                    _spriteRenderer.flipY = true;
                }
                else
                {
                    _rigidbody2D.gravityScale = 1f;
                    _spriteRenderer.flipY = false;
                }
            }
        }
        else
        {
            _rigidbody2D.linearVelocity = new Vector2(hor * speed, ver * speed);
        }

        _animator.SetBool("IsMoving", hor != 0 || ver != 0);

        // -------- SPRITE + FIREPOINT FLIP (left/right) --------
        if (hor > 0 && !facingRight)
        {
            facingRight = true;
            _spriteRenderer.flipX = false;
            firePoint.localPosition = new Vector3(0.5f, firePoint.localPosition.y, firePoint.localPosition.z); // right
        }
        else if (hor < 0 && facingRight)
        {
            facingRight = false;
            _spriteRenderer.flipX = true;
            firePoint.localPosition = new Vector3(-0.5f, firePoint.localPosition.y, firePoint.localPosition.z); // left
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContacts++;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContacts--;

            if (groundContacts <= 0)
            {
                groundContacts = 0;
                isGrounded = false;
            }
        }
    }
}