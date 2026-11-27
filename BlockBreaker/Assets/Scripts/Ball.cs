using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float InitialBallVelocity = 8f;
    [SerializeField] private Transform paddle;
    [SerializeField] private AudioClip paddleSound;
    [SerializeField] private AudioClip wallSound;

    [SerializeField] private float minimumVerticalSpeed = 2f;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool hasLaunched = false;
    private Vector2 ballPaddleDelta;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        ballPaddleDelta =
            (Vector2)transform.position - (Vector2)paddle.position;
    }

    void Update()
    {
        if (!hasLaunched)
        {
            PaddleLocked();

            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }
    }

    private void PaddleLocked()
    {
        Vector2 paddlePosition = paddle.position;
        transform.position = paddlePosition + ballPaddleDelta;
    }

    public void LaunchBall()
    {
        rb.linearVelocity = Vector2.up * InitialBallVelocity;
        hasLaunched = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(
            gameObject.name + " collided with " +
            collision.gameObject.name
        );

        if (collision.gameObject.GetComponent<Paddle>() != null)
        {
            if (paddleSound != null)
            {
                audioSource.PlayOneShot(paddleSound);
            }
        }

        else if (
            collision.gameObject.name == "Left Wall" ||
            collision.gameObject.name == "Right Wall" ||
            collision.gameObject.name == "Ceiling"
        )
        {
            if (wallSound != null)
            {
                audioSource.PlayOneShot(wallSound);
            }
        }

        Vector2 velocity = rb.linearVelocity;

        if (Mathf.Abs(velocity.y) < minimumVerticalSpeed)
        {
            velocity.y = velocity.y >= 0
                ? minimumVerticalSpeed
                : -minimumVerticalSpeed;

            rb.linearVelocity =
                velocity.normalized * InitialBallVelocity;
        }
    }
}
