using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _musicSourceDefault, _musicSourceBattle, _effectsSource;
    [SerializeField] private AudioMixer _mixer;
    private bool _battleMusicOn;
    private float _volumeA, _volumeB;
    [SerializeField] private float _transitionRate = 1f;
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

    private void Update()
    {
        if(_battleMusicOn && (_volumeB < 0))
        {
            _volumeA -= _transitionRate;
            _volumeB += _transitionRate;
            if (_volumeA < -80f) _volumeA = -80f;
            if (_volumeB > 0f) _volumeB = 0f;
            Debug.Log(_volumeA);
            _mixer.SetFloat("VolumeA", _volumeA);
            _mixer.SetFloat("VolumeB", _volumeB);
        } else if (!_battleMusicOn && (_volumeA < 0))
        {
            _volumeB -= _transitionRate;
            _volumeA += _transitionRate;
            if (_volumeB < -80f) _volumeB = -80f;
            if (_volumeA > 0f) _volumeA = 0f;
            _mixer.SetFloat("VolumeA", _volumeA);
            _mixer.SetFloat("VolumeB", _volumeB);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void PlaySingleMusic(AudioClip clip)
    {
        _musicSourceDefault.clip = clip;
        _volumeA = 0f;
        _volumeB = -80f;
        _mixer.SetFloat("VolumeA", _volumeA);
        _mixer.SetFloat("VolumeB", _volumeB);
        _musicSourceDefault.Play();
    }

    public void PlayDualMusic(AudioClip defaultClip, AudioClip battleClip)
    {
        _musicSourceDefault.clip = defaultClip;
        _musicSourceBattle.clip = battleClip;
        _volumeA = 0f;
        _volumeB = -80f;
        _mixer.SetFloat("VolumeA", _volumeA);
        _mixer.SetFloat("VolumeB", _volumeB);
        _battleMusicOn = false;
        _musicSourceDefault.Play();
        _musicSourceBattle.Play();
    }

    public void ChangeToDefaultMusic()
    {
        _battleMusicOn = false;
    }

    public void ChangeToBattleMusic()
    {
        _battleMusicOn = true;
    }

    public void ChangeMasterVolume(float value)
    {
        _mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void ChangeMusicVolume(float value)
    {
        _mixer.SetFloat("VolumeMusic", Mathf.Log10(value) * 20);
    }

    public void ChangeSFXVolume(float value)
    {
        _mixer.SetFloat("VolumeSFX", Mathf.Log10(value) * 20);
    }
}
