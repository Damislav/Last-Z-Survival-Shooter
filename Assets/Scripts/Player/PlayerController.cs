using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public static Action<float, float> onChangePlayerSpeed;

    public static Action<float, float, float> onBoostActivated;
    [SerializeField] private float normalMovementSpeed = 15f;
    [SerializeField] private float movementSpeed;


    private float rightEnd = 6.30f;
    private float leftEnd = -6.30f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        movementSpeed = normalMovementSpeed;
        onChangePlayerSpeed += OnChangePlayerSpeed;
    }


    void Update()
    {
        HandleTouchInput();
        HandleKeyboardInput();
        ClampPosition();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 move = new Vector3(touch.deltaPosition.x * 0.01f, 0, 0); // Normalize movement
                transform.position += move * movementSpeed * Time.deltaTime;
            }
        }
    }

    private void HandleKeyboardInput()
    {
        float moveInput = Input.GetAxis("Horizontal"); // -1 (left) to 1 (right)
        transform.position += new Vector3(moveInput, 0, 0) * movementSpeed * Time.deltaTime;
    }

    private void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, leftEnd, rightEnd);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnChangePlayerSpeed(float speedMultiplier, float duration)
    {
        movementSpeed = movementSpeed * speedMultiplier;
        Invoke(nameof(ResetSpeed), duration);
    }




    // Reset to Normal Speed
    private void ResetSpeed()
    {
        movementSpeed = normalMovementSpeed;
    }

    void OnDestroy()
    {
        onChangePlayerSpeed -= OnChangePlayerSpeed;
    }
}
