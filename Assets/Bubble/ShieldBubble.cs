using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float fallSpeed = 2f; // Velocidade de queda da bolha
    public float popForce = 10f; // Força aplicada ao jogador ao estourar
    public float lifeTime = 5f; // Tempo de vida da bolha antes de desaparecer

    private Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool isActive = false; // Define se a bolha está ativa ao redor do jogador

    void Start()
    {
        // Destroi a bolha após um tempo se ela não for usada
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        Use();
    }

    protected override void Use()
    {
        if (!isActive)
        {
            // A bolha cai se ainda não estiver ativa
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else if (player != null)
        {
            // A bolha segue o jogador quando ativa
            transform.position = new Vector2(player.position.x, player.position.y);
        }
    }

    public void ActivateBubble(Transform playerTransform)
    {
        isActive = true;
        player = playerTransform;
        playerRb = player.GetComponent<Rigidbody2D>();
        Debug.Log("ShieldBubble activated around player!");
    }

    protected override void PopBubble()
    {
        if (playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, popForce);
        }

        // Destroi a bolha
        Destroy(gameObject);
    }
}
