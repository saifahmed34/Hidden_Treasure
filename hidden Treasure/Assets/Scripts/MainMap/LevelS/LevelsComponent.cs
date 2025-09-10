using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsComponent : MonoBehaviour
{
    public PlayerAttack PlayerAttack;
    private Rigidbody2D Rigidbody2D;
    public static Vector3 MainMapSpawnPoint = new Vector3(-7f, -1.12f, 0f);
    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerAttack = GetComponent<PlayerAttack>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (currentScene == "MainMap")
        {
            transform.position = MainMapSpawnPoint;
        }
    }


    private void Update()


    {
        PlayerAttack = GetComponent<PlayerAttack>();
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMap")
        {
            PlayerAttack.enabled = false;
            Rigidbody2D.gravityScale = 0;

        }
        if (currentScene != "MainMap")
        {
            PlayerAttack.enabled = true;
            Rigidbody2D.gravityScale = 1;

        }
    }

}
