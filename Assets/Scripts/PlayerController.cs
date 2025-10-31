using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Text distanceText;

    private CharacterController controller;
    private PlayerInput input;
    private Camera cam;
    private Vector3 velocity;
    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main;
        startPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        UpdateDistanceUI();
    }

    void HandleMovement()
    {
        Vector3 direction = input.moveDirection;

        if (direction.magnitude >= 0.1f)
        {
            // Convert input to world direction relative to camera
            Vector3 forward = cam.transform.forward;
            Vector3 right = cam.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 horizontalMove = forward * direction.z + right * direction.x;
            horizontalMove.Normalize();
            horizontalMove *= moveSpeed;

            // Rotate player towards movement direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(horizontalMove),
                rotationSpeed * Time.deltaTime);

            // Apply horizontal movement
            Vector3 move = horizontalMove;
            move.y = velocity.y;
            controller.Move(move * Time.deltaTime);
        }

        // Jump
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (controller.isGrounded && input.jumpPressed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            input.jumpPressed = false; // reset to prevent multiple jumps
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

    }

    void UpdateDistanceUI()
    {
        if (distanceText != null)
        {
            Vector3 displacement = transform.position - startPosition;
            float distance = Vector3.Distance(startPosition, transform.position);

            distanceText.text =
                "Total Distance: " + distance.ToString("F2") + " units\n" +
                "X: " + displacement.x.ToString("F2") + "\n" +
                "Y: " + displacement.y.ToString("F2") + "\n" +
                "Z: " + displacement.z.ToString("F2");
        }
    }
}
