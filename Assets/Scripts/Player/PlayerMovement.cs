using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    [Header("Components")]
    public Rigidbody2D rb;
    public GameObject groundCheck;
    public Animator anim;
    public float moveInput;

    [Header("Ground Movement Settings")]
    public LayerMask groundLayer;
    public float moveSpeed = 5f;
    public float groundCheckRadius = 0.1f;
    //public float groundAcceleration = 5f;
    //public float groundDeceleration = 5f;

    [Header("Air Movement Settings")]
    public float jumpForce = 10f;
    //public float airAcceleration = 5f;
    //public float airDeceleration = 5f;

    [Header("Flags")]
    public bool isGrounded;
    public bool isFacingRight = true;

    [Header("Audio")]
    public AudioClip walkAudio;
    public AudioClip jumpAudio;

    // Eventos
    public event Action OnPlayerJump;

    private void Update()
    {
        IsGrounded();
        anim.SetBool("IsGrounded", IsGrounded());
        anim.SetFloat("Direction", moveInput);
        anim.SetFloat("VerticalVelocity", rb.linearVelocityY);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private bool IsGrounded()
    {
        isGrounded = 
            Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
        return isGrounded;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        //Vector3 scale = transform.localScale;
        //scale.x *= -1;
        //transform.localScale = scale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    private void Move()
    {
        var xVelocity = moveInput * moveSpeed;
        rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);

        if ((isFacingRight && moveInput < -0.1f) || (!isFacingRight && moveInput > 0.1f)) Flip();

        if (isGrounded && Mathf.Abs(moveInput) > 0.1f)
        {
            AudioManager.Instance.PlayAudio(walkAudio, true);
        }
        else AudioManager.Instance.StopAudio();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            anim.SetTrigger("Jump");
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            OnPlayerJump?.Invoke();

            //AudioManager.Instance.PlaySFX(jumpAudio);
        }
        if (context.canceled && rb.linearVelocity.y > 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
}