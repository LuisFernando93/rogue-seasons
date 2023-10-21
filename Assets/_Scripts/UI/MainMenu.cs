using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuOST, _ButtonClick;
    [SerializeField] private Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider;
    [SerializeField] private GameObject playButton, optionButton, creditsButton, exitButton, backButton; 
    [SerializeField] private GameObject masterVolumeLabel, musicVolumeLabel, sfxVolumeLabel, languageLabel;
    [SerializeField] private MainMenuAssets[] assets;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deletando playerPrefs");
        SoundManager.Instance.PlaySingleMusic(_mainMenuOST);
        Load();
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_ButtonClick);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Hub");
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
        PlayerPrefs.SetString("language", language);
        MainMenuAssets selectedAssets = null;
        foreach (var asset in assets)
        {
            if (asset.tag == language)
            {
                selectedAssets = asset;
            }
        }
        if (selectedAssets != null)
        {
            reloadMenuAssets(selectedAssets);
        } else
        {
            Debug.Log("Assets nao encontrados");
        }

    }

    private void reloadMenuAssets(MainMenuAssets assets)
    {
        playButton.GetComponent<Image>().sprite = assets.play;
        optionButton.GetComponent<Image>().sprite = assets.options;
        creditsButton.GetComponent<Image>().sprite = assets.credits;
        exitButton.GetComponent<Image>().sprite = assets.exit;
        backButton.GetComponent<Image>().sprite = assets.back;
        //masterVolumeLabel.GetComponent<Text>().text = assets.volumeMaster;
        //musicVolumeLabel.GetComponent<Text>().text = assets.volumeMusic;
        //sfxVolumeLabel.GetComponent<Text>().text = assets.volumeSFX;
        //languageLabel.GetComponent<Text>().text = assets.language;
    }

    public void Save()
    {
        OptionsData data = new OptionsData(_masterVolumeSlider.value, _musicVolumeSlider.value, _SFXVolumeSlider.value, PlayerPrefs.GetString("language","PTBR"));
        SaveSystem.SaveOptions(data);
        Debug.Log("Opcoes salvas");
    }
    public void Load()
    {
        OptionsData data = SaveSystem.LoadOptions();
        if (data != null)
        {
            _masterVolumeSlider.value = data.getMasterVolume();
            _musicVolumeSlider.value = data.getVolumeMusic();
            _SFXVolumeSlider.value = data.getVolumeSFX();
            this.changeLanguage(data.getLanguage());
            Debug.Log("Mudando lingua para " + data.getLanguage());
        }
    }
}
