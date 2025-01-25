using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    private float moveDirection;
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // Forca do pulo adicional

    private void Awake()
    {
        SetDirection();
    }

    private void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        if (!isOnWind)
        {
            var xVelocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
            myRb.linearVelocity = new Vector2(xVelocity, myRb.linearVelocity.y);
        }

        if (isOnWind)
        {
            var yVelocity = moveSpeed * Time.fixedDeltaTime;
            myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, myRb.linearVelocity.y);
        }
    }

    void SetDirection()
    {
        if (PlayerManager.Instance != null
            && PlayerManager.Instance.PlayerMovement != null)
        {
            // Define a dire��o com base no valor de isFacingRight
            bool direction = PlayerManager.Instance.PlayerMovement.isFacingRight;

            // Agora, definimos a dire��o com base no isFacingRight
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
