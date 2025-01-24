using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // Forca do pulo adicional

    public enum Direction
    {
        Left = -1,
        Right = 1
    }
    public Direction moveDirection = Direction.Right;

    private void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        // Bolha move na horizontal
        var xVelocity = (float)moveDirection * moveSpeed * Time.deltaTime;
        myRb.linearVelocity = new Vector2(xVelocity, myRb.linearVelocity.y);
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
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
