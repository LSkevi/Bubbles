using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Criar bolha
        {
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {

    }
}
