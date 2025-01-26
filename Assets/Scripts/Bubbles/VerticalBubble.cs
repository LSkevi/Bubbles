using UnityEngine;

public class VerticalBubble : BubbleBase
{
    [Header("Own Variables")]
    public float floatSpeed = 1f; // Velocidade de subida da bolha
    private bool isPlayerInside;
    private bool isPlayerJumping;

    private PlayerMovement playerMovement; // Referência para o PlayerMovement
    private JumpFuncion jumpFuncion;

    void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        SetConstraints();

        if (!isOnWind)
        {
            var yVelocity = floatSpeed * Time.fixedDeltaTime;
            myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, yVelocity);

            if (isPlayerInside && isPlayerJumping) PopBubble();
        }

        if (isOnWind)
        {
            var yVelocity = floatSpeed * Time.fixedDeltaTime;
            myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, myRb.linearVelocity.y);

            if (isPlayerInside && isPlayerJumping) PopBubble();
        }
    }

    void SetConstraints()
    {
        if (isOnWind)
            myRb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        else myRb.constraints |= RigidbodyConstraints2D.FreezePositionX;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Resetando o pulo caso esteja no chão
                if (playerMovement.isGrounded) isPlayerJumping = false;

                if (!isPlayerInside)
                {
                    isPlayerInside = true;
                    if (playerTransform != null) ParentPlayer();
                }

                // Associa o evento de pulo para ser chamado quando o player pular
                jumpFuncion.OnPlayerJump += HandlePlayerJump;
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

                // Desassociando o evento ao sair da colisão
                if (playerMovement != null) jumpFuncion.OnPlayerJump -= HandlePlayerJump;
            }
        }
    }

    // Função chamada quando o pulo é executado
    private void HandlePlayerJump()
    {
        if (isPlayerInside) isPlayerJumping = true;
    }
}
