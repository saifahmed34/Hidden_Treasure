using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStart : MonoBehaviour
{
    int Completed_Level = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Level1")
        {
            SceneManager.LoadScene("Level1");

            Completed_Level++;
        }
        else if (collision.gameObject.tag == "Level2" && Completed_Level == 1)
        {
            SceneManager.LoadScene("Level2");
            Completed_Level++;
        }
        else if (collision.gameObject.tag == "Level3" && Completed_Level == 2)
        {
            SceneManager.LoadScene("Level3");
            Completed_Level++;
        }
        else if (collision.gameObject.tag == "lastlevel" && Completed_Level == 3)
        {
            SceneManager.LoadScene("lastlevel");
        }
    }
}