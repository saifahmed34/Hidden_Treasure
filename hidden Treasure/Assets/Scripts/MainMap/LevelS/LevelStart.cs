using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelStart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int Completed_Level = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            LevelNumber levelNumber = new LevelNumber();
            if (levelNumber != null && levelNumber.LevelIndex == Completed_Level + 1)
            {
                SceneManager.LoadScene(levelNumber.LevelName);
                Completed_Level++;
            }
        }


    }
}
