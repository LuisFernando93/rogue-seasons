using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    public bool isOver { get; private set; } = false;
    [SerializeField] private GameObject _gameOverMenuContainer;
    [SerializeField] private AudioClip _buttonClick;

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.isDead) {
            isOver = true;
        } else isOver = false;

        if (isOver)
        {
            _gameOverMenuContainer.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void MenuClickSFX()
    {
        SoundManager.Instance.PlaySFX(_buttonClick);
    }

    public void ResetButton()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        SceneManager.LoadScene("Hub");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
    
}
