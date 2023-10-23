using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInit : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip1, _musicClip2;
    void Start()
    {
        if (_musicClip1 != null)
        {
            if (_musicClip2 != null)
            {
                SoundManager.Instance.PlayDualMusic(_musicClip1, _musicClip2);
            }
            else
            {
                SoundManager.Instance.PlaySingleMusic(_musicClip1);
            }
        }
    }
}
