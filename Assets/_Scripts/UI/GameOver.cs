using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public static bool gameIsOver { get; private set; } = false;
    [SerializeField] private GameObject _gameOverMenuContainer;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private GameObject resetButton, exitButton;
    [SerializeField] private PauseMenuAssets[] assets;

    void Start()
    {
        changeLanguage(PlayerPrefs.GetString("language"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.isDead) {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            gameIsOver = true;
        }

        if(gameIsOver)
        {
            _gameOverMenuContainer.SetActive(true);
        }
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_buttonClick);
    }

    public void ResetButton()
    {
        gameIsOver = false;
        SceneManager.LoadScene("Main menu");
    }

    public void ExitGameButton()
    {
        Application.Quit();
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
        resetButton.GetComponent<Image>().sprite = assets.options;
        exitButton.GetComponent<Image>().sprite = assets.exit;
    }
    
}
