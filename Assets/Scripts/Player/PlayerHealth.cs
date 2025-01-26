using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 3; // Vida m�xima do jogador
    public int currentHealth;
    public bool isShieldActive;
    public Vector2 spawnPoint;

    void Start()
    {
        if (spawnPoint == Vector2.zero) spawnPoint = transform.position;
        currentHealth = maxHealth; // Come�a com a vida cheia
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive)
        {
            // Se h� uma bolha escudo, ela absorve o dano e estoura
            Debug.Log("ShieldBubble blocking damage!");
            return;
        }

        // Aplica dano ao jogador se n�o houver bolha
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0) Die();
        else RespawnInCheckPoint();
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Reinicie a cena ou implemente l�gica adicional de morte aqui
    }

    // Interface IDamageable
    public void OnTakeDamage(int damage) => TakeDamage(damage);

    public void RespawnInCheckPoint() {
        if (spawnPoint == null) return;
        else transform.position = spawnPoint;
    }
}
