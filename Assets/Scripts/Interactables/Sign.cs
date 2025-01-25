using TMPro;
using UnityEngine;

public class Sign : MonoBehaviour {
    [Header("Components")]
    public GameObject signPanel;
    public TextMeshPro signText;
    [Header("Message")]
    public string message;

    private void Start() {
      signText.text = message;
    }

    public void ShowSign(bool show) {
      signPanel.SetActive(show);
	  if(show) signText.text = message;
    }
    
    private void OnTriggerEnter2D(Collider2D col) {
      if(col.tag == "Player") {
        ShowSign(true);
      }
    }

    private void OnTriggerExit2D(Collider2D col) {
      if (col.tag == "Player") {
        ShowSign(false);
      }
    }
}