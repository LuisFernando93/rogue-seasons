using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused { get; private set; } = false;
    [SerializeField] private GameObject _pauseMenuContainer;
    [SerializeField] private Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private GameObject optionButton, exitButton, backButton, resumeButton, resetButton, gameOverResetButton, gameOverExitButton;
    [SerializeField] private PauseMenuAssets[] assets;

    void Start()
    {
        _masterVolumeSlider.value = SoundManager.Instance.GetVolumeFromMixer("master");
        _musicVolumeSlider.value = SoundManager.Instance.GetVolumeFromMixer("music");
        _SFXVolumeSlider.value = SoundManager.Instance.GetVolumeFromMixer("SFX");
        changeLanguage(PlayerPrefs.GetString("language"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        _pauseMenuContainer.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        _pauseMenuContainer.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_buttonClick);
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void ExitLevelButton()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Resume();
        Destroy(GameObject.FindGameObjectWithTag("Canvas"));
        SceneManager.LoadScene("Hub");
    }

    public void ExitGameButton()
    {
        Resume();
        SceneManager.LoadScene("Main menu");
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
        PauseMenuAssets selectedAssets = null;
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
        }
        else
        {
            Debug.Log("Assets nao encontrados");
        }
    }

    private void reloadMenuAssets(PauseMenuAssets assets)
    {
        optionButton.GetComponent<Image>().sprite = assets.options;
        exitButton.GetComponent<Image>().sprite = assets.exit;
        backButton.GetComponent<Image>().sprite = assets.back;
        resumeButton.GetComponent<Image>().sprite = assets.resume;
        resetButton.GetComponent<Image>().sprite = assets.reset;
        gameOverExitButton.GetComponent<Image>().sprite = assets.exit;
        gameOverResetButton.GetComponent<Image>().sprite = assets.reset;
    }

    public void Save()
    {
        OptionsData data = new OptionsData(_masterVolumeSlider.value, _musicVolumeSlider.value, _SFXVolumeSlider.value, PlayerPrefs.GetString("language", "PTBR"));
        SaveSystem.SaveOptions(data);
        Debug.Log("Opcoes salvas");
    }
}
