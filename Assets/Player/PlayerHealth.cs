using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3; // Vida máxima do jogador
    private int currentHealth;

    private ShieldBubble activeShieldBubble; // Referência à bolha escudo ativa

    void Start()
    {
        currentHealth = maxHealth; // Começa com a vida cheia
    }

    public void TakeDamage(int damage)
    {
        if (activeShieldBubble != null)
        {
            // Se há uma bolha escudo, ela absorve o dano e estoura
            Debug.Log("ShieldBubble blocking damage!");
            activeShieldBubble.PopBubble();
            activeShieldBubble = null; // Remove a referência à bolha
            return;
        }

        // Aplica dano ao jogador se não houver bolha
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
        // Reinicie a cena ou implemente lógica adicional de morte aqui
    }
}
