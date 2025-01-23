using UnityEngine;

public class BubblePickup : MonoBehaviour
{
    public BubbleItem bubbleItem; // O item que será adicionado ao inventário

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null && bubbleItem != null)
            {
                // Adiciona o item ao inventário do jogador
                inventory.AddItem(bubbleItem);
                Debug.Log($"Picked up: {bubbleItem.itemName}");
                Destroy(gameObject); // Remove o pickup da cena
            }
        }
    }
}
