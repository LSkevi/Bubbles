using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleItem", menuName = "Items/Bubble")]
public class BubbleItem : ScriptableObject
{
    public string itemName;
    public GameObject bubblePrefab;
    public int maxUses;

    // M�todo para inicializar um clone, se necess�rio
    public void Initialize(int initialUses)
    {
        maxUses = initialUses;
    }
}