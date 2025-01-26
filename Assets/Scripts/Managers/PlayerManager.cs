using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public Transform playerTransform;
    public PlayerMovement PlayerMovement;
    public JumpFuncion jumpFuncion;
    public PlayerHealth PlayerHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
