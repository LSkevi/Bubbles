using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public static PlayerHealth Instance;
    public int maxHealth = 3; // Vida máxima do jogador
    public int currentHealth;
    public bool isShieldActive;
    private Vector2 firstSpawnPoint;
    public Vector2 spawnPoint;

    [Header("Score Settings")]
    public int score = 0;
    public int pointsForExtraLife = 100;

    [Header("Ammo Settings")]
    public int maxAmmo = 7; // Máximo de munição
    public int currentAmmo = 7; // Munição inicial

    public event Action OnDamageTaken;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        firstSpawnPoint = transform.position;
        if (spawnPoint == Vector2.zero) spawnPoint = transform.position;
        currentHealth = maxHealth; // Começa com a vida cheia
        currentAmmo = maxAmmo; // Começa com munição cheia
    }

    public void TakeDamage(int damage)
    {
        PlayerManager.Instance.CameraSwitch.Shake();

        if (isShieldActive)
        {
            OnDamageTaken?.Invoke();
            Debug.Log("ShieldBubble blocking damage!");
            return;
        }

        GetComponent<Collider2D>().enabled = false;
        currentHealth -= damage;
        RespawnInCheckPoint();

        Debug.Log($"Player took {damage} damage. Current health: {currentHealth}");
        Invoke("ReanableCollider",0.2f);

        if (currentHealth <= 0)
            Die();
    }

    void ReanableCollider() {
        GetComponent<Collider2D>().enabled = true;
    }

    private void Die()
    {
        currentHealth = maxHealth;
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
            currentAmmo--; // Consome uma unidade de munição
            Debug.Log($"Ammo used. Remaining ammo: {currentAmmo}");
        }
    }

    public bool HasAmmo()
    {
        return currentAmmo > 0; // Retorna se há munição
    }

    public void RespawnInCheckPoint()
    {
        if (spawnPoint == null) return;

        if(currentHealth <= 0) transform.position = firstSpawnPoint;
        else transform.position = spawnPoint;
    }

    public void OnTakeDamage(int damage) => TakeDamage(damage);

    public void ReloadAmmo()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Ammo reloaded!");
    }
}
