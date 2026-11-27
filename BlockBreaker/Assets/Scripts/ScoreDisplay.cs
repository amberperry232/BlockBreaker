using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = "Score: " +
                ScoreManager.Instance.CurrentScore;
        }
    }
}
