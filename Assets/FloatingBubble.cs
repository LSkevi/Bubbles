using UnityEngine;

public class FloatingBubble : MonoBehaviour
{
    public float floatSpeed = 2f; // Velocidade de subida da bolha
    public float additionalJumpForce = 5f; // Força do pulo adicional
    private Transform player; // Referência ao jogador
    private Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool isCarryingPlayer = false;

    void Update()
    {
        // Bolha sobe
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Se estiver carregando o jogador, mova-o junto com a bolha
        if (isCarryingPlayer && player != null)
        {
            player.position = new Vector3(transform.position.x, transform.position.y, player.position.z);

            // Verifica se o jogador tentou pular
            if (Input.GetButtonDown("Jump")) // Substitua "Jump" pelo seu mapeamento de pulo
            {
                PerformAdditionalJump();
                PopBubble();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Inicia o transporte do jogador
            isCarryingPlayer = true;
            player = collision.transform;
            playerRb = collision.GetComponent<Rigidbody2D>();

            // Desabilita a gravidade do jogador enquanto ele está na bolha
            if (playerRb != null)
            {
                playerRb.gravityScale = 0;
                playerRb.linearVelocity = Vector2.zero; // Zera qualquer movimento
            }
        }
        else if (collision.CompareTag("Obstacle"))
        {
            // Estoura a bolha ao tocar em um obstáculo
            PopBubble();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Para de carregar o jogador se ele sair da bolha
            ReleasePlayer();
        }
    }

    void PerformAdditionalJump()
    {
        if (playerRb != null)
        {
            // Aplica um impulso vertical ao jogador
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, additionalJumpForce);
        }
    }

    void PopBubble()
    {
        // Estoura a bolha e libera o jogador na posição atual
        if (isCarryingPlayer && player != null)
        {
            ReleasePlayer();
        }
        Destroy(gameObject);
    }

    void ReleasePlayer()
    {
        // Libera o jogador na posição atual da bolha
        if (playerRb != null)
        {
            playerRb.gravityScale = 1; // Restaura a gravidade
        }

        if (player != null)
        {
            player.position = new Vector3(transform.position.x, transform.position.y, player.position.z);
        }

        isCarryingPlayer = false;
        player = null;
        playerRb = null;
    }
}
