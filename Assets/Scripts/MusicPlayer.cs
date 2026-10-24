
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        source.volume = SettingsManager.instance.musicVolume;
    }
}
