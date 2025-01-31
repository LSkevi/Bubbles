using TMPro;
using UnityEngine;

public class PlayerUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI bubbleText;
    public TextMeshProUGUI scoreText;

    private void Update() {
        lifeText.text = PlayerHealth.Instance.currentHealth + "x";
        bubbleText.text = PlayerHealth.Instance.currentAmmo + "x";
        scoreText.text = PlayerHealth.Instance.score + "x";
    }
}
