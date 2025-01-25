using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    private float moveDirection;
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // Forca do pulo adicional
    public PlayerMovement playerMovement;

    private void Awake()
    {
        SetDirection();
    }

    private void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        // Bolha move na horizontal
        var xVelocity = moveDirection * moveSpeed * Time.deltaTime;
        myRb.linearVelocity = new Vector2(xVelocity, myRb.linearVelocity.y);
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void SetDirection()
    {
        if (PlayerManager.Instance != null
            && PlayerManager.Instance.PlayerMovement != null)
        {
            // Define a direção com base no valor de isFacingRight
            bool direction = PlayerManager.Instance.PlayerMovement.isFacingRight;

            // Agora, definimos a direção com base no isFacingRight
            moveDirection = direction ? 1f : -1f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Faz o player quicar
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }
    }
}
