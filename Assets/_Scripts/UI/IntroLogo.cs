using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLogo : MonoBehaviour
{
    private void Update()
    {
        if(Input.anyKey)
        {
            ToMainMenu();
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
