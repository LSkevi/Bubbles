using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleHandler : MonoBehaviour
{
    [Header("Bubbles Settings")]
    private Dictionary<string, GameObject> bubblePrefabs;

    public GameObject verticalBubblePrefab;
    public GameObject horizontalBubblePrefab;
    public GameObject shieldBubblePrefab;
    public GameObject explosiveBubblePrefab;

    public Transform spawnPoint;
    public SpriteRenderer spriteRenderer;

    private string currentForm;

    [Header("Cooldown Settings")]
    public float bubbleSpawnCooldown = 1.25f;
    public bool canSpawnBubble = true;

    private PlayerHealth playerHealth;
    public AudioClip swapColorAudio;

    private void Awake()
    {
        InitializeDictionary();
        SetInitialForm("Vertical");
    }

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>(); // Obtém o script PlayerHealth
    }

    private void InitializeDictionary()
    {
        bubblePrefabs = new Dictionary<string, GameObject>
        {
            { "Vertical"    ,   verticalBubblePrefab },
            { "Horizontal"  ,   horizontalBubblePrefab },
            { "Shield"      ,   shieldBubblePrefab },
            { "Explosive"   ,   explosiveBubblePrefab }
        };
    }

    private void SetInitialForm(string form)
    {
        if (bubblePrefabs.ContainsKey(form))
        {
            currentForm = form;
            UpdateSpriteColor();
        }
    }

    public void ChangeForm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            string[] forms = new string[] { "Vertical", "Horizontal", "Shield", "Explosive" };
            int currentIndex = System.Array.IndexOf(forms, currentForm);
            currentForm = forms[(currentIndex + 1) % forms.Length];
            UpdateSpriteColor();
            AudioManager.Instance.PlaySFX(swapColorAudio);
        }
    }

    public void SpawnBubble(InputAction.CallbackContext context)
    {
        if (context.performed && spawnPoint != null && canSpawnBubble &&
            bubblePrefabs.TryGetValue(currentForm, out GameObject bubblePrefab))
        {
            if (playerHealth.HasAmmo())
            {
                StartCoroutine(HandleBubbleSpawn(bubblePrefab));
                playerHealth.UseAmmo(); // Consome munição
            }
            else
            {
                Debug.Log("No ammo left to spawn a bubble!");
            }
        }
    }

    private IEnumerator HandleBubbleSpawn(GameObject bubblePrefab)
    {
        canSpawnBubble = false;

        if (bubblePrefab != null)
        {
            if (bubblePrefab != shieldBubblePrefab)
                Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);
            else
            {
                var shield = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
                shield.transform.SetParent(transform);
            }
        }

        yield return new WaitForSeconds(bubbleSpawnCooldown);

        canSpawnBubble = true;
    }

    private void UpdateSpriteColor()
    {
        if (spriteRenderer == null) return;

        string hexColor = "#FFABAA";

        switch (currentForm)
        {
            case "Vertical":
                spriteRenderer.color = Color.cyan;
                break;
            case "Horizontal":
                spriteRenderer.color = Color.yellow;
                break;
            case "Shield":
                spriteRenderer.color = Color.green;
                break;
            case "Explosive":
                spriteRenderer.color = ColorUtility.TryParseHtmlString(hexColor, out Color color)
                    ? color
                    : spriteRenderer.color;
                break;
        }
    }
}
