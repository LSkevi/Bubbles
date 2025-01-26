using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BubbleBase : MonoBehaviour
{
    public Rigidbody2D myRb;
    public Transform playerTransform;
    public float stuckTime = 5f;

    [Header("Flags")]
    public bool isOnWind;
    public bool isStuck;

    public AudioClip popBubbleSound;

    protected List<string> friendlyTag =
        new List<string> { "CameraBounds", "BubblePlatform", "Player", "BouncySurface", "StickySurface", "Wind" };

    protected abstract void BubbleLogic();

    protected virtual void PopBubble(bool isTimeSensitive = false, float lifeTime = 0)
    {
        AudioManager.Instance.PlaySFX(popBubbleSound);
        if (!isStuck)
        {
            ReleasePlayer();

            if (isTimeSensitive)
            {
                Destroy(gameObject, lifeTime);
                return;
            }

            Destroy(gameObject);
        }
    }

    protected virtual void ParentPlayer()
    {
        playerTransform.SetParent(transform);
    }

    protected virtual void ReleasePlayer()
    {
        if (playerTransform != null) playerTransform.SetParent(null);
    }

    protected virtual void StuckBubble(Transform parent)
    {
        if (!isStuck)
        {
            isStuck = true;

            transform.SetParent(parent);
            myRb.bodyType = RigidbodyType2D.Static;

            transform.position = new Vector3
                (transform.position.x, parent.position.y, transform.position.z);
            transform.localScale *= 1.75f;

            StartCoroutine(WaitStuckTime());
        }
    }

    private IEnumerator WaitStuckTime()
    {
        yield return new WaitForSeconds(stuckTime);
        isStuck = false;
        PopBubble();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Estoura a bolha se entrar em contato com !friendlyTag
        if (!friendlyTag.Contains(collision.gameObject.tag))
        {
            Debug.Log($"Quem me estourou foi: {collision.gameObject.tag}");
            PopBubble();
        }

        if (collision.CompareTag("StickySurface")
            && this is not ShieldBubble)
        {
            StuckBubble(collision.transform);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision) { }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }

    protected virtual void OnCollisionExit2D(Collision2D collision) { }
}