using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float fallSpeed = 2f;    // Velocidade de queda da bolha
    public float jumpForce = 10f;   // Força aplicada ao jogador ao estourar
    public float lifeTime = 5f;     // Tempo de vida da bolha antes de desaparecer
    private bool isActive = false;  // Define se a bolha esta ativa ao redor do jogador
    private bool hasPopped = false; // Define se a bolha ja estourou

    void Start()
    {
        friendlyTag.Add("Ground");
    }

    private void Update()
    {
        if (!isActive && !hasPopped) PopBubble(true, lifeTime);
    }

    private void FixedUpdate() => BubbleLogic();

    protected override void BubbleLogic()
    {
        if (!isActive)
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.SetParent(collision.transform);
            isActive = true;
        }

        if (!hasPopped) hasPopped = true;

        base.OnTriggerEnter2D(collision);
    }

    protected override void PopBubble(bool isTimeSensitive = false, float lifeTime = 0)
    {
        var playerRb = PlayerManager.Instance.PlayerMovement.rb;

        playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        base.PopBubble(isTimeSensitive, lifeTime);
    }
}
