using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float bounceForce = 10f;

    private void Awake()
    {
        friendlyTag.Add("Ground");
        PlayerManager.Instance.PlayerHealth.OnDamageTaken += BubbleLogic;
    }

    private void Start()
    {
        PlayerManager.Instance.PlayerHealth.isShieldActive = true;
    }

    protected override void BubbleLogic() 
    {
        PlayerManager.Instance.PlayerHealth.isShieldActive = false;
        PopBubble();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
