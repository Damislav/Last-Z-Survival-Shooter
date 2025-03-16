using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 15f;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    private float rightEnd = 6.30f;
    private float leftEnd = -6.30f;

    void Update()
    {
        //mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 move = new Vector3(touch.deltaPosition.x, 0, 0);
                transform.position += move * speed * Time.deltaTime;
            }
        }

        // Keyboard Input (for testing)
        float moveInput = Input.GetAxis("Horizontal"); // -1 (left) to 1 (right)

        transform.position += new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;

        // Clamp the position
        float clampedX = Mathf.Clamp(transform.position.x, leftEnd, rightEnd);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z
        );
    }


}
