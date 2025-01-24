using UnityEngine;

public class VerticalBubble : BubbleBase
{
    public float floatSpeed = 2f; // Velocidade de subida da bolha
    public float additionalJumpForce = 5f; // Força do pulo adicional
    private Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool playerLeft;

    void Update()
    {
        BubbleLogic();
    }

    protected override void BubbleLogic()
    {
        // Bolha sobe
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Se player estava na bolha e sair, ela estoura
        if (playerLeft)
        {
            playerLeft = false;
            PopBubble();
        }
    }

    protected override void ReleasePlayer()
    {
        playerLeft = true;
        base.ReleasePlayer();
    }
}
