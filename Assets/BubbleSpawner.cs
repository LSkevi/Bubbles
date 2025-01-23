using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    private PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Criar bolha
        {
            SpawnBubble();
        }
    }

    void SpawnBubble()
    {
        var currentItem = inventory.CurrentItem;

        if (currentItem != null && currentItem.maxUses > 0)
        {
            // Instancia a bolha do tipo atual
            Instantiate(currentItem.bubblePrefab, spawnPoint.position, Quaternion.identity);
            currentItem.maxUses--;
            Debug.Log($"{currentItem.itemName} uses left: {currentItem.maxUses}");
        }
        else if (currentItem == null)
        {
            Debug.Log("No bubble item selected!");
        }
        else
        {
            Debug.Log("No uses left for the selected bubble item!");
        }
    }
}
