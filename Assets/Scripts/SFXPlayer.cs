using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioSource source;
    public List<AudioClip> clips;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        source.volume = SettingsManager.instance.sfxVolume;
    }

    public void PlaySFX(int i)
    {
        if (source == null) source = GetComponent<AudioSource>();
        source.PlayOneShot(clips[i]);
    }
}
