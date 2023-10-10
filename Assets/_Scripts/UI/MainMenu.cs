using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuOST, _ButtonClick;
    [SerializeField] private Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider;
    [SerializeField] private Language _language;
    [SerializeField] private LevelCounter _levelCounter;
    

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
        _levelCounter.NewGame();
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

    public void Save()
    {
        OptionsData data = new OptionsData(_masterVolumeSlider.value, _musicVolumeSlider.value, _SFXVolumeSlider.value, "PT-BR");
        SaveSystem.SaveOptions(data);
    }

    public void Load()
    {
        OptionsData data = SaveSystem.LoadOptions();
        _masterVolumeSlider.value = data.getMasterVolume();
        _musicVolumeSlider.value = data.getVolumeMusic();
        _SFXVolumeSlider.value = data.getVolumeSFX();
        _language.changeLanguage(data.getLanguage());
    }

}
