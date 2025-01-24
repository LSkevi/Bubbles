using UnityEngine;

public class HorizontalBouncingBubble : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade horizontal da bolha
    public float bounceForce = 10f; // For�a do pulo adicional
    private Vector2 moveDirection = Vector2.right; // Dire��o inicial do movimento

    private void Update()
    {
        // Movimenta-se horizontalmente
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Faz o jogador pular
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
            }
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Explode a bolha ao tocar em um obst�culo
            ExplodeBubble();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Explode ao tocar um obst�culo s�lido
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ExplodeBubble();
        }
    }

    private void ExplodeBubble()
    {
        // Explos�o da bolha (pode adicionar efeitos aqui)
        Destroy(gameObject);
    }
}
