using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuOST;
    [SerializeField] private Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySingleMusic(_mainMenuOST);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Summer");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void AdjustVolume(string type)
    {
        switch (type)
        {
            case "master":
                SoundManager.Instance.ChangeMasterVolume(_masterVolumeSlider.value);
                break;
            case "music":
                SoundManager.Instance.ChangeMusicVolume(_musicVolumeSlider.value);
                break;
            case "SFX":
                SoundManager.Instance.ChangeSFXVolume(_SFXVolumeSlider.value);
                break;
            default:
                //String incompativel
                break;
        }
    }

}
