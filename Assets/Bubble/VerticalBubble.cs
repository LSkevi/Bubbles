using UnityEngine;

public class VerticalBubble : BubbleBase
{
    public float floatSpeed = 2f; // Velocidade de subida da bolha
    public float additionalJumpForce = 5f; // Força do pulo adicional
    private bool playerLeft;

    void Update()
    {
        BubbleLogic();
    }

    protected override void BubbleLogic()
    {
        var yVelocity = floatSpeed * Time.deltaTime;
        myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, floatSpeed);

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
