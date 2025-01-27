using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public void PauseGame() {
        if (Time.timeScale == 1) {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        } else {
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void BackToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OpenSound() {
        AudioManager.Instance.OpenAudioSettings();
    }
}