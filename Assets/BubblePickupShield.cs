using UnityEngine;

public class BubblePickupShield : MonoBehaviour
{
    public ShieldBubble shieldPrefab; // Prefab da bolha escudo

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null && shieldPrefab != null)
            {
                // Instancia a bolha escudo e ativa no jogador
                ShieldBubble shieldBubble = Instantiate(shieldPrefab, collision.transform.position, Quaternion.identity);
                playerHealth.SetShieldBubble(shieldBubble);
                Debug.Log("ShieldBubble picked up and activated!");
                Destroy(gameObject); // Remove o pickup
            }
        }
    }
}
