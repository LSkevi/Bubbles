using UnityEngine;

public class VerticalBubble : BubbleBase
{
    public float floatSpeed = 2f; // Velocidade de subida da bolha
    public float additionalJumpForce = 5f; // Força do pulo adicional
    private Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool playerLeft;

    void Update()
    {
        Use();
    }

    protected override void Use()
    {
        // Bolha sobe
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

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
