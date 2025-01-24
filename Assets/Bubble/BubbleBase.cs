using System.Collections.Generic;
using UnityEngine;

public abstract class BubbleBase : MonoBehaviour
{
    public Rigidbody2D myRb;
    public Transform playerTransform;

    protected List<string> friendlyTag = 
        new List<string> { "Player", "BouncySurface", "StickySurface" };

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
        playerTransform.SetParent(transform);
    }

    protected virtual void ReleasePlayer()
    {
        playerTransform.SetParent(null);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Estoura a bolha se entrar em contato com !friendlyTag
        if (!friendlyTag.Contains(collision.gameObject.tag)) PopBubble();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerTransform != null) ParentPlayer();
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) ReleasePlayer();
    }
}
