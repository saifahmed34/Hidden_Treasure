using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement_Map : MonoBehaviour
{
    private float Startx, Starty;
    private float miny1, miny2;
    private string curState;
    private bool lvl2 = false;
    private bool lvl4 = false;
    
    private bool lvltl = false;

    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;   // how long dash lasts
    public float dashCooldown = 1f;     // time between dashes

    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f; // timer for cooldown

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
        Startx = transform.position.x;
        Starty = transform.position.y;
    }

    void Update()
    {
        // Scene-specific setup
        if (SceneManager.GetActiveScene().name != curState)
        {
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                miny1 = -11;
                miny2 = 24;
                transform.localScale = new Vector3(4, 4, 1);
                curState = SceneManager.GetActiveScene().name;
            }
            else if (SceneManager.GetActiveScene().name == "MainMap")
            {
                transform.position = new Vector3(-7.14f, -1.87f, 0);
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
                curState = SceneManager.GetActiveScene().name;
                isFlying = false;
                _rigidbody2D.gravityScale = 1f;
                _spriteRenderer.flipY = false;
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                if (!lvl2)
                {
                    transform.position = new Vector2(-4.6f, -1.82f);
                    lvl2 = true;
                    isFlying = false;
                    _rigidbody2D.gravityScale = 1f;
                    _spriteRenderer.flipY = false;
                }
                if (transform.position.y > 25 || transform.position.y < -15)
                {
                    isFlying = false;
                    _rigidbody2D.gravityScale = 1f;
                    _spriteRenderer.flipY = false;
                    transform.position = new Vector3(-4.6f,-1.82f,0);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Level4")
            {
                if (!lvl4)
                {
                    transform.position = new Vector2(43f, 8f);
                    lvl4 = true;
                    isFlying = false;
                    _rigidbody2D.gravityScale = 1f;
                    _spriteRenderer.flipY = false;
                    transform.localScale = new Vector3(2.5f, 2.5f, 1);
                }
            }
            else if (SceneManager.GetActiveScene().name == "TreasureLevel")
            {
                if (!lvltl)
                {
                    transform.position = new Vector2(-4.6f, -1.82f);
                    lvltl = true;
                    isFlying = false;
                    _rigidbody2D.gravityScale = 1f;
                    _spriteRenderer.flipY = false;
                    transform.localScale = new Vector3(4, 4, 1);
                }
            }
        }

        // Dash cooldown
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        if (!isDashing)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
            {
                StartDash();
            }
        }
        else
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                EndDash();
            }
        }

        // Death check for Level1
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (transform.position.y < miny1 || transform.position.y > miny2)
            {
                RestartLevel();
            }
        }
    }

    private void RestartLevel()
    {
        Vector3 newPos = transform.position;

        newPos.x = Startx;
        newPos.y = Starty;

        isFlying = false;
        _rigidbody2D.gravityScale = 1f;
        _spriteRenderer.flipY = false;
        transform.position = newPos;
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown; // reset cooldown

        float dashDirection = facingRight ? 1f : -1f;

        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
    }

    private void EndDash()
    {
        isDashing = false;
        _rigidbody2D.gravityScale = 1f;
    }

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "tutorial" || currentScene == "Level1" ||
            currentScene == "Level2" || currentScene == "LastLevel" || currentScene == "TreasureLevel" || currentScene == "Level3" || currentScene == "Level4")
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

        if (hor > 0 && !facingRight)
        {
            facingRight = true;
            _spriteRenderer.flipX = false;
            firePoint.localPosition = new Vector3(0.5f, firePoint.localPosition.y, firePoint.localPosition.z);
        }
        else if (hor < 0 && facingRight)
        {
            facingRight = false;
            _spriteRenderer.flipX = true;
            firePoint.localPosition = new Vector3(-0.5f, firePoint.localPosition.y, firePoint.localPosition.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            groundContacts++;
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Bullet") && SceneManager.GetActiveScene().name == "Level4")
        {
            RestartLevel();
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
