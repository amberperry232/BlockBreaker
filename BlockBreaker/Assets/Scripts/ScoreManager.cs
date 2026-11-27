using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        CurrentScore += points;
        Debug.Log("Current Score: " + CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
    }
}
