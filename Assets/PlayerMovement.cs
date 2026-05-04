using System.Collections;
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

    [Header("Facing Direction")]
    [SerializeField] private int facingDirection = 1;
    private bool facingDirectionSet = false;

    private float horizontal;
    private Animator animator;
    private bool isAttacking = false;
    private PlayerCombat combat;

    public float Horizontal => horizontal;
    public bool IsGroundedValue => IsGrounded();
    public float VerticalVelocity => rb.linearVelocity.y;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        StartCoroutine(SetFacingDirection());
    }

    private IEnumerator SetFacingDirection()
    {
        yield return null;

        PlayerInput input = GetComponent<PlayerInput>();
        if (input != null)
        {
            facingDirection = input.playerIndex == 0 ? 1 : -1;
            transform.localScale = new Vector3(0.5f * facingDirection, 0.49005f, 1);
            Debug.Log($"Player index: {input.playerIndex}, Facing: {facingDirection}");
        }
        facingDirectionSet = true;
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            float control = IsGrounded() ? 1f : airControl;
            float targetSpeed = horizontal * speed;
            float newX = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, control);
            rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * 2.5f * Time.fixedDeltaTime;
        }

        if (facingDirectionSet && horizontal != 0)
        {
            float direction = Mathf.Sign(horizontal);
            transform.localScale = new Vector3(0.5f * direction, 0.49005f, 1);
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

    public void LightAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
            if (IsGrounded())
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetTrigger("LightAttack");
            combat.LightAttack();
            StartCoroutine(ResetAttack());
        }
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;
            if (IsGrounded())
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            animator.SetTrigger("HeavyAttack");
            combat.HeavyAttack();
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
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