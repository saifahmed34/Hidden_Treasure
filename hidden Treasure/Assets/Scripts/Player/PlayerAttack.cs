using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // where bullets spawn
    public float shootCooldown = 0.3f;
    /* public PlayerAttack playerAttack;*/

    private float timer;


    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer >= shootCooldown) // left mouse click
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // important in 2D

        Vector2 dir = (mousePos - firePoint.position).normalized;

        bullet.GetComponent<Bullet>().SetDirection(dir);

        // rotate to face the direction
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
