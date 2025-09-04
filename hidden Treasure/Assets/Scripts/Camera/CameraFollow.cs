using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    public float SmothSpeed = 0.125f;
    public Vector3 offset;


    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
        {
            GameObject Player = GameObject.FindGameObjectWithTag("Player");
            if (Player != null)
            {
                target = Player.transform;
            }
        }
        /*    if (target != null) { }*/
        transform.position = target.position + offset;
    }
}
