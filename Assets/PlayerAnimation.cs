using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();

        if (animator == null) Debug.LogError("Animator not found on " + gameObject.name);
        if (movement == null) Debug.LogError("PlayerMovement not found on " + gameObject.name);
    }

    private void Update()
    {
        if (animator == null || movement == null) return;

        animator.SetFloat("Speed", Mathf.Abs(movement.Horizontal));
        animator.SetBool("IsGrounded", movement.IsGroundedValue);
        animator.SetFloat("VerticalVelocity", movement.VerticalVelocity);
    }
}
