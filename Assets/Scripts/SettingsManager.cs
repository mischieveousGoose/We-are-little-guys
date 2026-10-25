using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;
    public bool didReset = false;

    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private Slider musicSlider;
    private Slider sfxSlider;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Menu")
            return;

        GameObject sfxSliderObject = GameObject.Find("SFX Slider");
        GameObject musicSliderObject = GameObject.Find("Music Slider");

        sfxSlider = sfxSliderObject.GetComponent<Slider>();
        musicSlider = musicSliderObject.GetComponent<Slider>();

        sfxSlider.value = sfxVolume;
        musicSlider.value = musicVolume;
        sfxVolume = sfxSlider.value;
        musicVolume = musicSlider.value;

        sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetMusicVolume(float value)
    {
        musicVolume = value;
    }

    private void SetSfxVolume(float value)
    {
        sfxVolume = value;
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
