using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Movement
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject groundCheck;
    public Transform bubbleParent;
    [HideInInspector]
    public float moveInput;

    [Header("Ground Movement Settings")]
    public LayerMask groundLayer;
    public float moveSpeed = 5f;
    public float groundCheckRadius = 0.1f;

    [Header("Air Movement Settings")]
    public float jumpForce = 10f;
    public float fallMultiplier = 1.5f;

    [Header("Flags")]
    public bool isGrounded;
    public bool isFacingRight = true;

    [Header("Audio")]
    public AudioClip[] walkAudio;
    public AudioClip jumpAudio;

    //[Header("States")]
    //public MoveStates currentState;

    // Eventos
    public event Action OnPlayerJump;

    private void Update()
    {
        IsGrounded();
        anim.SetBool("IsGrounded", IsGrounded());
        anim.SetBool("Moving", rb.linearVelocityX != 0);
        anim.SetFloat("HorizontalDirection", moveInput);
        anim.SetFloat("VerticalVelocity", rb.linearVelocityY);
        //UpdateState(currentState);
        //HandleStateTransitions();
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
        Vector3 scale = bubbleParent.localScale;
        scale.x *= -1;
        bubbleParent.localScale = scale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>().x;
    }

    private void Move()
    {
        var xVelocity = moveInput * moveSpeed;
        rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);

        if ((isFacingRight && moveInput < -0.1f) 
            || (!isFacingRight && moveInput > 0.1f)) Flip();
    }

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    if (context.performed && isGrounded)
    //    {
    //        anim.SetTrigger("Jump");
    //        OnPlayerJump?.Invoke();
    //    }
    //    if(context.canceled && isGrounded)
    //    {
    //        anim.SetTrigger("Jump");
    //        OnPlayerJump?.Invoke();
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
    }
    #endregion
}