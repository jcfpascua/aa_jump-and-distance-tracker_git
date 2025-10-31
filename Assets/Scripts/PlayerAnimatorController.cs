using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public float animationSmoothTime = 0.1f;
    private Animator animator;
    private PlayerInput input;
    private float currentSpeed;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (animator == null) return;

        float targetSpeed = input.moveDirection.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, animationSmoothTime);
        animator.SetFloat("Speed", currentSpeed);
    }
}
