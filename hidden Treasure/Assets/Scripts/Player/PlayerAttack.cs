using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // where bullets spawn
    public float shootCooldown = 0.3f;
    /* public PlayerAttack playerAttack;*/

    private float timer;
    public AudioSource audioSource;
    public PlayerMovement_Map playerMovement;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.J) && timer >= shootCooldown) // left mouse click
        {

            Shoot();

            timer = 0f;
        }
    }

    void Shoot()
    {
        audioSource.Play();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);



        Vector2 shootDirection = playerMovement.facingRight ? Vector2.right : Vector2.left;

        // Set bullet direction
        bullet.GetComponent<Bullet>().SetDirection(shootDirection);

        // Optional: rotate bullet so sprite points the same way
        if (shootDirection == Vector2.left)
            bullet.transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
    }
}
