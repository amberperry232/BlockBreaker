using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip blockBreakSound;
    [SerializeField] private int pointValue = 10;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField][Min(1)] private int hitsToDestroy = 1;
    [SerializeField] private bool isUnbreakable = false;

    private int hitsRemaining;
    private bool destroyed = false;

    public bool IsUnbreakable => isUnbreakable;

    private void Start()
    {
        hitsRemaining = hitsToDestroy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyed || collision.gameObject.GetComponent<Ball>() == null)
            return;

        // Play sound whenever the ball hits a block
        if (blockBreakSound != null)
        {
            AudioSource.PlayClipAtPoint(
                blockBreakSound,
                transform.position
            );
        }

        // Unbreakable blocks never lose health or award points
        if (isUnbreakable)
            return;

        hitsRemaining--;

        // Wait until the final hit to destroy the block
        if (hitsRemaining > 0)
            return;

        destroyed = true;

        // Award points only when the block breaks
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointValue);
        }

        // Explosion on final hit
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        // Notify LevelManager
        LevelManager levelManager =
            FindFirstObjectByType<LevelManager>();

        if (levelManager != null)
        {
            levelManager.BlockDestroyed();
        }

        Destroy(gameObject);
    }
}
