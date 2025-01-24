using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<BubbleItem> items = new List<BubbleItem>();
    private int currentIndex = 0;

    public BubbleItem CurrentItem => items.Count > 0 ? items[currentIndex] : null;

    void Update()
    {
        if (items.Count == 0) return;

        // Alternar itens com Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentIndex = (currentIndex + 1) % items.Count;
            Debug.Log($"Current Item: {CurrentItem?.itemName}");
        }
    }

    public void AddItem(BubbleItem item)
    {
        if (item == null) return;

        // Clonar o ScriptableObject
        BubbleItem clonedItem = Instantiate(item);
        items.Add(clonedItem);

        Debug.Log($"Picked up: {clonedItem.itemName}, Uses: {clonedItem.maxUses}");
    }
}
