using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator spriteAnimator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private bool isFacingRight = true;

    private Vector2 movementInput;

    void Update()
    {
        HandleInput();
        UpdateAnimatorParameters();
    }

    void FixedUpdate()
    {
        Move();
        CheckGrounded();
    }

    private void HandleInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            spriteAnimator.SetTrigger("jump");
        }

        // Atualiza a direção do personagem (sem flip)
        if (movementInput.x > 0)
        {
            isFacingRight = true;
        }
        else if (movementInput.x < 0)
        {
            isFacingRight = false;
        }
    }

    private void Move()
    {
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void UpdateAnimatorParameters()
    {
        spriteAnimator.SetBool("isGrounded", isGrounded);
        spriteAnimator.SetBool("isMoving", movementInput.x != 0);
        spriteAnimator.SetBool("isFacingRight", isFacingRight);
        spriteAnimator.SetFloat("verticalVelocity", rb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void ApplyJumpForce() {

        // Aplica o impulso de pulo
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

    }
}
