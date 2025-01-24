using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    public float fallSpeed = 2f; // Velocidade de queda da bolha
    public float popForce = 10f; // Força aplicada ao jogador ao estourar
    public float lifeTime = 5f; // Tempo de vida da bolha antes de desaparecer

    private Transform player; // Referência ao jogador
    private Rigidbody2D playerRb; // Referência ao Rigidbody2D do jogador
    private bool isActive = false; // Define se a bolha está ativa ao redor do jogador

    void Start()
    {
        // Destroi a bolha após um tempo se ela não for usada
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (!isActive)
        {
            // A bolha cai se ainda não estiver ativa
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
        else if (player != null)
        {
            // A bolha segue o jogador quando ativa
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActive)
        {
            // Ativa a bolha ao redor do jogador
            ActivateBubble(collision.transform);
        }
    }

    public void ActivateBubble(Transform playerTransform)
    {
        isActive = true;
        player = playerTransform;
        playerRb = player.GetComponent<Rigidbody2D>();
        Debug.Log("ShieldBubble activated around player!");
    }

    public void PopBubble()
    {
        if (playerRb != null)
        {
            // Dá um impulso vertical ao jogador
            Debug.Log("ShieldBubble popped! Giving player an upward boost.");
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, popForce);
        }

        // Destroi a bolha
        Destroy(gameObject);
    }
}
