using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool runPressed; // new

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(h, 0f, v).normalized;

        jumpPressed = Input.GetKeyDown(KeyCode.Space);
        runPressed = Input.GetKey(KeyCode.LeftShift); // holding shift to run
    }
}
