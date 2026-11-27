using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AudioClip levelCompleteSound;
    [SerializeField][Range(0.5f, 2f)] private float gameSpeed = 1f;

    private int remainingBlocks;
    private bool levelComplete = false;

    void Start()
    {
        Time.timeScale = gameSpeed;

        // Count only blocks that can be destroyed
        remainingBlocks = 0;

        Block[] blocks = FindObjectsByType<Block>(
            FindObjectsSortMode.None
        );

        foreach (Block block in blocks)
        {
            if (!block.IsUnbreakable)
            {
                remainingBlocks++;
            }
        }
    }

    public void BlockDestroyed()
    {
        if (levelComplete)
            return;

        remainingBlocks--;

        if (remainingBlocks <= 0)
        {
            levelComplete = true;
            StartCoroutine(CompleteLevel());
        }
    }

    private IEnumerator CompleteLevel()
    {
        if (levelCompleteSound != null)
        {
            AudioSource.PlayClipAtPoint(
                levelCompleteSound,
                Camera.main.transform.position
            );

            yield return new WaitForSeconds(levelCompleteSound.length);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level01")
        {
            SceneManager.LoadScene("Level02");
        }
        else if (currentScene == "Level02")
        {
            SceneManager.LoadScene("Win");
        }
    }
}
