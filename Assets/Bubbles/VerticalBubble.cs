using UnityEngine;

public class VerticalBubble : BubbleBase
{
    public float floatSpeed = 1f; // Velocidade de subida da bolha
    private bool isPlayerInside;
    private bool isPlayerJumping;

    private PlayerMovement playerMovement; // Refer�ncia para o PlayerMovement

    void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, floatSpeed);

        // Verifica se o player est� dentro da bolha e se ele executou o pulo
        if (isPlayerInside && isPlayerJumping) PopBubble();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Resetando o pulo caso esteja no ch�o
                if (playerMovement.isGrounded) isPlayerJumping = false;

                if (!isPlayerInside)
                {
                    isPlayerInside = true;
                    if (playerTransform != null) ParentPlayer();
                }

                // Associa o evento de pulo para ser chamado quando o player pular
                playerMovement.OnPlayerJump += HandlePlayerJump;
            }
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isPlayerInside)
            {
                isPlayerInside = false;

                if (isPlayerJumping) isPlayerJumping = false;

                // Desassociando o evento ao sair da colis�o
                if (playerMovement != null) playerMovement.OnPlayerJump -= HandlePlayerJump;
            }
        }
    }

    // Fun��o chamada quando o pulo � executado
    private void HandlePlayerJump()
    {
        if (isPlayerInside) isPlayerJumping = true;
    }
}
