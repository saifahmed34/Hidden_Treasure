using UnityEngine;

public class PlayerMovement_Map : MonoBehaviour
{
    public float speed = 5f;
    void Start()
    {

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
        transform.Translate(new Vector2(hor, ver) * speed * Time.deltaTime);
    }

}
