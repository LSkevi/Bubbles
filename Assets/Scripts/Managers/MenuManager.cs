using UnityEngine;

public class MenuManager : MonoBehaviour {
    public void OpenAudioConfig (){
        AudioManager.Instance.OpenAudioSettings();
    }

    public void LoadScene(int scene) {
        SceneChanger.instance.LoadScene(scene);
    }

    public void QuitGame() {
        Application.Quit();
    }
}