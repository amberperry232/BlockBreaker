using UnityEngine;

public class Paddle : MonoBehaviour
{
    void Update()
    {
        MoveWithMouse();
    }

    void MoveWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float paddleX = Mathf.Clamp(mousePosition.x, 1f, 15f);

        transform.position = new Vector3(
            paddleX,
            transform.position.y,
            transform.position.z
        );
    }
}
