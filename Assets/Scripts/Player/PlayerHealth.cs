using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 3; // Vida máxima do jogador
    public int currentHealth;
    public bool isShieldActive;

    public int score = 0; // Pontuação do jogador
    public int pointsForExtraLife = 100; // Pontos necessários para ganhar uma vida

    void Start()
    {
        currentHealth = maxHealth; // Começa com a vida cheia
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive)
        {
            // Se há uma bolha escudo, ela absorve o dano e estoura
            Debug.Log("ShieldBubble blocking damage!");
            return;
        }

        // Aplica dano ao jogador se não houver bolha
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0) Die();
    }

    public void AddScore(int points)
    {
        score += points; // Adiciona pontos
        Debug.Log($"Score: {score}");

        // Verifica se o jogador ganhou uma vida
        while (score >= pointsForExtraLife)
        {
            GainExtraLife();
            score -= pointsForExtraLife; // Subtrai os pontos usados para ganhar a vida
        }
    }

    private void GainExtraLife()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            Debug.Log("Extra life gained! Current health: " + currentHealth);
        }
        else
        {
            Debug.Log("Extra life gained, but health is already full!");
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Reinicie a cena ou implemente lógica adicional de morte aqui
    }

    // Interface IDamageable
    public void OnTakeDamage(int damage) => TakeDamage(damage);
}
