using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float fallSpeed = 2f; // Velocidade de queda da bolha
    public float jumpForce = 10f; // Força aplicada ao jogador ao estourar
    public float lifeTime = 5f; // Tempo de vida da bolha antes de desaparecer

    public Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool isActive = false; // Define se a bolha está ativa ao redor do jogador

    void Start()
    {
        // Destroi a bolha após um tempo se ela não for usada
        PopBubble(true, lifeTime);
    }

    private void FixedUpdate()
    {
        BubbleLogic();
    }

    protected override void BubbleLogic()
    {
        if (!isActive)
        {
            // A bolha cai se ainda não estiver ativa
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }

    public void ActivateBubble() => isActive = true;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        ActivateBubble();
    }

    protected override void PopBubble(bool isTimeSensitive = false, float lifeTime = 0)
    {
        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        base.PopBubble(isTimeSensitive, lifeTime);
    }
}
