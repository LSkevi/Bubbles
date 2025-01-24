using UnityEngine;

public class HorizontalBouncingBubble : BubbleBase
{
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // Forca do pulo adicional
    private Vector2 moveDirection = Vector2.right; // Dire��o inicial do movimento

    private void Update()
    {
        Use();
    }

    protected override void Use()
    {
        // Movimenta-se horizontalmente
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("BouncingSurface"))
        {
            // Faz o jogador pular
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
            }
        }
        else if (!collision.CompareTag("Player"))
        {
            // Explode a bolha ao tocar em um obst�culo
            ExplodeBubble();
        }
    }

    private void ExplodeBubble()
    {
        // Explos�o da bolha (pode adicionar efeitos aqui)
        Destroy(gameObject);
    }
}
