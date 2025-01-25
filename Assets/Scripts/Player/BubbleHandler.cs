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

    private void Awake()
    {
        InitializeDictionary();
        SetInitialForm("Vertical");
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
            UpdateSpriteColor(); // Atualiza a cor com base na forma inicial
        }
    }

    public void ChangeForm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Array com as formas
            string[] forms = new string[] { "Vertical", "Horizontal", "Shield", "Explosive" };

            // Pega o índice atual e calcula o próximo de forma cíclica
            int currentIndex = System.Array.IndexOf(forms, currentForm);
            currentForm = forms[(currentIndex + 1) % forms.Length];

            UpdateSpriteColor();
        }
    }

    public void SpawnBubble(InputAction.CallbackContext context)
    {
        if (context.performed && spawnPoint != null && canSpawnBubble
            && bubblePrefabs.TryGetValue(currentForm, out GameObject bubblePrefab))
        {
            StartCoroutine(HandleBubbleSpawn(bubblePrefab));
        }
    }

    private IEnumerator HandleBubbleSpawn(GameObject bubblePrefab)
    {
        canSpawnBubble = false;

        // Instancia a bolha
        if (bubblePrefab != null)
        {
            // Se nao eh shield
            if (bubblePrefab != shieldBubblePrefab)
                Instantiate(bubblePrefab, spawnPoint.position, Quaternion.identity);

            // Se eh shield
            else
            {
                var shield = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
                shield.transform.SetParent(transform);
            }
        }

        // Aguarda o cooldown
        yield return new WaitForSeconds(bubbleSpawnCooldown);

        // Libera o spawn e reativa o movimento
        canSpawnBubble = true;
    }


    private void UpdateSpriteColor()
    {
        if (spriteRenderer == null) return;

        // Define as cores com base no nome da forma
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
                spriteRenderer.color = Color.red;
                break;
        }
    }
}
