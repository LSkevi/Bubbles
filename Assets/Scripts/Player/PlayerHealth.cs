using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
<<<<<<< Updated upstream
    public static PlayerHealth Instance;
    public int maxHealth = 3; // Vida m�xima do jogador
=======
    [Header("Health Settings")]
    public int maxHealth = 3;
>>>>>>> Stashed changes
    public int currentHealth;
    public bool isShieldActive;
    public Vector2 spawnPoint;

    [Header("Score Settings")]
    public int score = 0;
    public int pointsForExtraLife = 100;

    [Header("Ammo Settings")]
    public int maxAmmo = 7; // M�ximo de muni��o
    public int currentAmmo = 7; // Muni��o inicial

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        if (spawnPoint == Vector2.zero) spawnPoint = transform.position;
        currentHealth = maxHealth; // Come�a com a vida cheia
        currentAmmo = maxAmmo; // Come�a com muni��o cheia
    }

    public void TakeDamage(int damage)
    {
        if (isShieldActive)
        {
            Debug.Log("ShieldBubble blocking damage!");
            return;
        }

        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0) Die();
        else RespawnInCheckPoint();
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score: {score}");

        while (score >= pointsForExtraLife)
        {
            GainExtraLife();
            score -= pointsForExtraLife;
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

    public void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--; // Consome uma unidade de muni��o
            Debug.Log($"Ammo used. Remaining ammo: {currentAmmo}");
        }
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0; // Retorna se h� muni��o
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }

    public void RespawnInCheckPoint()
    {
        if (spawnPoint == null) return;
        else transform.position = spawnPoint;
    }

    public void OnTakeDamage(int damage) => TakeDamage(damage);

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Ammo reloaded!");
    }
}
