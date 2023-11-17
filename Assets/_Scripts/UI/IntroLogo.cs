using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLogo : MonoBehaviour
{
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
