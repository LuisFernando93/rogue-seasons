using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuOST, _ButtonClick;
    [SerializeField] private Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySingleMusic(_mainMenuOST);
        Load();
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_ButtonClick);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Summer1");
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

    public void changeLanguage(string language)
    {
        Language.Instance.changeLanguage(language);
    }

    public void Save()
    {
        OptionsData data = new OptionsData(_masterVolumeSlider.value, _musicVolumeSlider.value, _SFXVolumeSlider.value, Language.Instance.getSelectedLanguage());
        SaveSystem.SaveOptions(data);
    }
    public void Load()
    {
        OptionsData data = SaveSystem.LoadOptions();
        if (data != null)
        {
            _masterVolumeSlider.value = data.getMasterVolume();
            _musicVolumeSlider.value = data.getVolumeMusic();
            _SFXVolumeSlider.value = data.getVolumeSFX();
            Language.Instance.changeLanguage(data.getLanguage());
            Debug.Log("Mudando lingua para " + data.getLanguage());
        }
    }
}
