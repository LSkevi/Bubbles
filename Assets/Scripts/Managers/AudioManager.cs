using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Audio Mixers")]
    public AudioMixer mixer;
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("Texts")]
    public TextMeshProUGUI masterText;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI sfxText;

    [Header("Window")]
    public GameObject settingsPanel;

    private void Awake() {
        // Garante que apenas um AudioManager exista no jogo
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o AudioManager entre cenas
        } else {
            Destroy(gameObject);
        }

        LoadVolumes();
    }

    public void PlayAudio(AudioClip clip, bool loop = false) {
        if (bgmAudioSource == null || clip == null) return;
        if (bgmAudioSource.isPlaying && bgmAudioSource.clip == clip) return;

        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = loop;
        bgmAudioSource.Play();
    }

    public void StopAudio() {
        if (bgmAudioSource.isPlaying) {
            bgmAudioSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip) {
        if (sfxAudioSource == null || clip == null) return;

        sfxAudioSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume) {
        masterText.text = "Master - " + (volume*100).ToString("F0");

        mixer.SetFloat("Master Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master Volume", volume);
    }

    public void ChangeBGMVolume(float volume) {
        bgmText.text = "BGM - " + (volume * 100).ToString("F0");

        mixer.SetFloat("BGM Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGM Volume", volume);
    }

    public void ChangeSFXVolume(float volume) {
        sfxText.text = "SFX - " + (volume * 100).ToString("F0");

        mixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX Volume", volume);
    }

    public void LoadVolumes() {
        masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Master Volume", 0.5f));
        ChangeMasterVolume(masterSlider.value);
        bgmSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("BGM Volume", 0.5f));
        ChangeBGMVolume(bgmSlider.value);
        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX Volume", 0.5f));
        ChangeSFXVolume(sfxSlider.value);
    }

    public void OpenAudioSettings() {
        settingsPanel.SetActive(true);
    }

    public void CloseAudioSettings() {
        settingsPanel.SetActive(false);
    }

}