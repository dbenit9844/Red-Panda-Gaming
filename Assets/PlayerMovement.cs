using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;

    [Header("Settings")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpingPower = 12f;
    [SerializeField] float airControl = 0.9f;

    [Header("Grounding")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundBoxSize = new Vector2(0.3f, 0.1f);

    private float horizontal;

    // EXPOSED VALUES FOR ANIMATION
    public float Horizontal => horizontal;
    public bool IsGroundedValue => IsGrounded();
    public float VerticalVelocity => rb.linearVelocity.y;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float control = IsGrounded() ? 1f : airControl;
        float targetSpeed = horizontal * speed;
        float newX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, control);
        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);

        // Better fall feel
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.fixedDeltaTime;
        }

        // Flip direction
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.49005f, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.49005f, 1);
        }
    }

    #region INPUT
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (context.canceled && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.4f);
        }
    }
    #endregion

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }
    }
}
