using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1f;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        SceneManager.LoadScene("Level01");
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        SceneManager.LoadScene("Level01");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
