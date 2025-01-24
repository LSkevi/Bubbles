using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Vida m�xima do jogador
    private int currentHealth;

    private ShieldBubble activeShieldBubble; // Refer�ncia � bolha escudo ativa

    void Start()
    {
        currentHealth = maxHealth; // Come�a com a vida cheia
    }

    public void TakeDamage(int damage)
    {
        if (activeShieldBubble != null)
        {
            // Se h� uma bolha escudo, ela absorve o dano e estoura
            Debug.Log("ShieldBubble blocking damage!");
            activeShieldBubble.PopBubble();
            activeShieldBubble = null; // Remove a refer�ncia � bolha
            return;
        }

        // Aplica dano ao jogador se n�o houver bolha
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void SetShieldBubble(ShieldBubble shieldBubble)
    {
        activeShieldBubble = shieldBubble;
        shieldBubble.ActivateBubble(transform);
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Reinicie a cena ou implemente l�gica adicional de morte aqui
    }
}
