using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    [Header("Own Variables")]
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
        SetConstraints();

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

    void SetConstraints()
    {
        if (isOnWind)
            myRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        else myRb.constraints |= RigidbodyConstraints2D.FreezePositionY;
    }

    void SetDirection()
    {
        if (PlayerManager.Instance != null
            && PlayerManager.Instance.PlayerMovement != null)
        {
            bool direction = PlayerManager.Instance.PlayerMovement.isFacingRight;
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
