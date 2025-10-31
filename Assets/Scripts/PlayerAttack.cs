using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public EnemyDetection targetEnemy;
    public Text feedbackText;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (feedbackText != null)
            feedbackText.text = "";
    }

    void Update()
    {
        HandleAttack();
        UpdateFeedbackUI();
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");

            if (targetEnemy != null && targetEnemy.CanBeBackstabbed())
            {
                targetEnemy.Die();
                ShowFeedback("Backstab Successful!", Color.green);
            }
            else
            {
                ShowFeedback("Attack Failed!", Color.red);
            }
        }
    }

    void UpdateFeedbackUI()
    {
        if (targetEnemy == null || feedbackText == null) return;

        // Show prompt when in backstab zone
        if (targetEnemy.CanBeBackstabbed() && feedbackText.text != "Backstab Successful!")
        {
            feedbackText.text = "STAB NOW!";
            feedbackText.color = Color.yellow;
        }
    }

    void ShowFeedback(string message, Color color)
    {
        if (feedbackText == null) return;
        feedbackText.text = message;
        feedbackText.color = color;
    }
}
