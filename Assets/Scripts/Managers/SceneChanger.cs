using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    public static SceneChanger instance;
    private void Awake() {
        instance = this;
    }

    public void LoadScene(int scene) {
        SceneManager.LoadSceneAsync(scene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}