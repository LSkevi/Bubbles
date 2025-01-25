using UnityEngine;

public class ShieldBubble : BubbleBase
{
    public float bounceForce = 10f;

    private void Awake()
    {
        friendlyTag.Add("Ground");
        Debug.Log($"Friendly Tags: {string.Join(", ", friendlyTag)}");
    }

    private void Start()
    {
        if (PlayerManager.Instance.playerTransform != null)
            transform.SetParent(PlayerManager.Instance.playerTransform);
    }

    protected override void BubbleLogic() { }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!friendlyTag.Contains(collision.gameObject.tag))
        {
            var playerRb = PlayerManager.Instance.PlayerMovement.rb;

            if (transform.parent == playerTransform && playerRb != null)
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);

            Debug.Log($"Quem me estourou foi: {collision.gameObject.tag}");
            base.PopBubble();
        }
    }
}
