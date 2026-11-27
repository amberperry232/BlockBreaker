using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Floor : MonoBehaviour
{
    [SerializeField] private AudioClip gameOverSound;

    private bool gameEnded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>() != null && !gameEnded)
        {
            gameEnded = true;
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        if (gameOverSound != null)
        {
            AudioSource.PlayClipAtPoint(
                gameOverSound,
                Camera.main.transform.position
            );

            yield return new WaitForSeconds(
                gameOverSound.length
            );
        }

        SceneManager.LoadScene("GameOver");
    }
}
