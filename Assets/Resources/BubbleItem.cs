using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleItem", menuName = "Items/Bubble")]
public class BubbleItem : ScriptableObject
{
    public string itemName;
    public GameObject bubblePrefab;
    public int maxUses;

    // Método para inicializar um clone, se necessário
    public void Initialize(int initialUses)
    {
        maxUses = initialUses;
    }
}