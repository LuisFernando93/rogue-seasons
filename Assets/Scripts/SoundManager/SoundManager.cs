using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSource, _effectsSource;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
