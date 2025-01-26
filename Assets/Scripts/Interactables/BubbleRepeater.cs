using UnityEngine;

public class BubbleRepeater : MonoBehaviour
{
    public GameObject[] bubblePrefabs; // Lista de prefabs das bolhas disponíveis
    public Transform spawnPoint;       // Ponto onde as bolhas serão criadas
    public float spawnInterval = 10f;  // Intervalo entre as bolhas criadas

    private GameObject currentBubblePrefab; // O prefab da bolha atual
    private float spawnTimer;               // Temporizador interno

    void Update()
    {
        // Se há uma bolha configurada, iniciar o temporizador
        if (currentBubblePrefab != null)
        {
            spawnTimer += Time.deltaTime;

            // Verifica se é hora de criar uma nova bolha
            if (spawnTimer >= spawnInterval)
            {
                SpawnBubble();
                spawnTimer = 0f; // Reseta o temporizador
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "bubble"
        if (collision.CompareTag("bubble"))
        {
            // Obtém o script BubbleBase para acessar o tipo da bolha
            BubbleBase bubble = collision.GetComponent<BubbleBase>();
            if (bubble != null)
            {
                // Procura o prefab correspondente pelo tipo da bolha
                currentBubblePrefab = GetPrefabByType(bubble.bubbleType);

                if (currentBubblePrefab != null)
                {
                    Debug.Log($"Bubble type updated to: {bubble.bubbleType}");
                }
                else
                {
                    Debug.LogWarning($"No prefab found for bubble type: {bubble.bubbleType}");
                }
            }
        }
    }

    private GameObject GetPrefabByType(int bubbleType)
    {
        // Procura pelo prefab correspondente na lista de prefabs
        foreach (GameObject prefab in bubblePrefabs)
        {
            BubbleBase prefabBubble = prefab.GetComponent<BubbleBase>();
            if (prefabBubble != null && prefabBubble.bubbleType == bubbleType)
            {
                return prefab;
            }
        }
        return null; // Retorna null se nenhum prefab correspondente for encontrado
    }

    private void SpawnBubble()
    {
        if (currentBubblePrefab != null)
        {
            // Cria uma nova bolha ao lado do repetidor
            Instantiate(currentBubblePrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Bubble created!");
        }
    }
}
