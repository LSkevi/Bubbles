using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // Forca do pulo adicional
    private Vector2 moveDirection = Vector2.right; // Dire��o inicial do movimento

    private void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        // Bolha move na horizontal
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // Faz o player quicar
        if (collision.CompareTag("Player"))
        {
            //playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
        }
    }
}
