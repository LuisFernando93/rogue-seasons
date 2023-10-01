using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSourceDefault, _musicSourceBattle, _effectsSource;
    [SerializeField] private AudioMixer _mixer;
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

    public void PlayMusic(AudioClip defaultClip, AudioClip battleClip)
    {
        _musicSourceDefault.clip = defaultClip;
        _musicSourceBattle.clip = battleClip;
        _mixer.SetFloat("VolumeA", 0f);
        _mixer.SetFloat("VolumeB", -80f);
        _musicSourceDefault.Play();
        _musicSourceBattle.Play();
    }

    public void ChangeToDefaultMusic()
    {
        _mixer.SetFloat("VolumeA", 0f);
        _mixer.SetFloat("VolumeB", -80f);
    }

    public void ChangeToBattleMusic()
    {
        _mixer.SetFloat("VolumeA", -80f);
        _mixer.SetFloat("VolumeB", 0f);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
