using UnityEngine;

public class ShootCannon : MonoBehaviour
{
    public GameObject buleet;         // Bullet prefab
    public Transform cannon1;         // Assign GameObject 1's transform in Inspector
    public Transform cannon2;         // Assign GameObject 2's transform in Inspector
    public float bulletSpeed = 10f;
    public float shootInterval = 2f;

    private float shootTimer;

    void Update()
    {
        // Cannon1 shoots left at intervals
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootLeftFromCannon1();
            shootTimer = 0f;
        }
    }

    // Shoots a bullet to the left from cannon1
    void ShootLeftFromCannon1()
    {
        GameObject bullet = Instantiate(buleet, cannon1.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.SetDirection(Vector2.left, bulletSpeed, true);
    }

    // Call this from Player when player collides with the box to shoot up from cannon2
    public void ShootUpFromCannon2()
    {
        GameObject bullet = Instantiate(buleet, cannon2.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
            bulletScript.SetDirection(Vector2.up, bulletSpeed, false);
    }
}