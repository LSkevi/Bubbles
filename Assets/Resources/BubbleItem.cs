using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleItem", menuName = "Items/Bubble")]
public class BubbleItem : ScriptableObject
{
    public BubbleType Type { get; set; }
    public string itemName;
    public GameObject bubblePrefab;
    public int maxUses;

    // Método para inicializar um clone, se necessário
    public void Initialize(int initialUses)
    {
        maxUses = initialUses;
    }
}

public enum BubbleType
{
    Floating,
    Horizontal,
    Shield,
    Explosive
}