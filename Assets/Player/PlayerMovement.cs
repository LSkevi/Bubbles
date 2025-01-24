using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Collider2D bodyCollider;
    public Collider2D groundCheck;
    private float moveInput;

    [Header("Ground Movement Settings")]
    public LayerMask groundLayer;
    public float moveSpeed = 5f;
    //public float groundAcceleration = 5f;
    //public float groundDeceleration = 5f;

    [Header("Air Movement Settings")]
    public float jumpForce = 10f;
    //public float airAcceleration = 5f;
    //public float airDeceleration = 5f;

    [Header("Flags")]
    public bool isGrounded;
    public bool isFacingRight = true;

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    private void Move()
    {
        var xVelocity = moveInput * moveSpeed;
        rb.linearVelocity = new Vector2 (xVelocity, rb.linearVelocity.y);

        if ((isFacingRight && moveInput < -0.1f) || (!isFacingRight && moveInput > 0.1f)) Flip();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        if (context.canceled && rb.linearVelocity.y > 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }
}
