using UnityEngine;

public class BubbleRepeater : MonoBehaviour
{
    public GameObject currentBubblePrefab; // Prefab da bolha atual
    public Transform spawnPoint; // Ponto onde as bolhas serão criadas
    public float spawnInterval = 10f; // Intervalo entre as bolhas criadas
    private float spawnTimer; // Temporizador interno

    private void Update()
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
        if (collision.CompareTag("Bubble"))
        {
            // Atualiza o prefab da bolha com o que encostou
            currentBubblePrefab = collision.gameObject;

            // Opcional: Reinicie o temporizador ao mudar de bolha
            spawnTimer = 0f;

            Debug.Log($"Bubble type updated to: {currentBubblePrefab.name}");
        }
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
