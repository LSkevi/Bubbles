using UnityEngine;

public abstract class BubbleBase : MonoBehaviour
{
    public Transform player;

    protected abstract void BubbleLogic();

    protected virtual void PopBubble(bool isTimeSensitive = false, float lifeTime = 0)
    {
        ReleasePlayer();

        if (isTimeSensitive)
        {
            Destroy(gameObject, lifeTime);
            return;
        }

        Destroy(gameObject);
    }

    protected virtual void ParentPlayer()
    {
        player.SetParent(transform);
    }

    protected virtual void ReleasePlayer()
    {
        player.SetParent(null);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null) ParentPlayer();
        }

        if (!collision.CompareTag("Player") 
            || !collision.CompareTag("BouncySurface")) PopBubble();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) ReleasePlayer();
    }
}
