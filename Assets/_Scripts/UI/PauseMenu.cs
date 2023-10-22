using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused { get; private set; } = false;
    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private AudioClip _buttonClick;

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
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_buttonClick);
    }

    public void ExitLevelButton()
    {
        SceneManager.LoadScene("Hub");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
