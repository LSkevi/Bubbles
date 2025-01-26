using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1; // Valor da moeda (1 ponto ou 10 pontos)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Acessa o script do jogador para adicionar os pontos
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.AddScore(value);
            }

            // Destroi a moeda após ser coletada
            Destroy(gameObject);
        }
    }
}
