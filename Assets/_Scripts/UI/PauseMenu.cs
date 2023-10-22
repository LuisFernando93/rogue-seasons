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

    void Start()
    {
        //_masterVolumeSlider.value = 
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
        Resume();
        SceneManager.LoadScene("Hub");
    }

    public void ExitGameButton()
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
        OptionsData data = new OptionsData(_masterVolumeSlider.value, _musicVolumeSlider.value, _SFXVolumeSlider.value, PlayerPrefs.GetString("language", "PTBR"));
        SaveSystem.SaveOptions(data);
        Debug.Log("Opcoes salvas");
    }
}
