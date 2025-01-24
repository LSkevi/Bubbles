using UnityEngine;

public class VerticalBubble : BubbleBase
{
    public float floatSpeed = 1f; // Velocidade de subida da bolha
    private bool playerLeft;

    void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        var yVelocity = floatSpeed * Time.deltaTime;
        myRb.linearVelocity = new Vector2(myRb.linearVelocity.x, floatSpeed);

        //transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Se player estava na bolha e sair, ela estoura
        if (playerLeft) PopBubble(false);
    }

    protected override void ReleasePlayer()
    {
        playerLeft = true;
        base.ReleasePlayer();
    }
}
