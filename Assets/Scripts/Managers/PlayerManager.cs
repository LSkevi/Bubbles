using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Transform playerTransform;
    public PlayerMovement PlayerMovement;
    public PlayerHealth PlayerHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: mantém o manager entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
