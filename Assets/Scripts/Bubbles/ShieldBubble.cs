using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float bounceForce = 10f;

    private void Awake()
    {
        friendlyTag.Add("Ground");
    }

    private void Start()
    {
        PlayerManager.Instance.PlayerHealth.isShieldActive = true;
    }

    protected override void BubbleLogic() { }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!friendlyTag.Contains(collision.gameObject.tag))
        {
            PlayerManager.Instance.PlayerHealth.isShieldActive = true;

            Debug.Log($"Quem me estourou foi: {collision.gameObject.tag}");
            PopBubble();
        }
    }
}
