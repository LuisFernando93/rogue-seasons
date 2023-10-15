using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language: MonoBehaviour
{
    private string languageKey = "Language";
    private string[] supportedLanguages = {"PT-BR","ENG"};

    public static Language Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public string getSelectedLanguage()
    {
        return PlayerPrefs.GetString(languageKey, "PT-BR");
    }

    public bool hasLanguageSupport(string language)
    {
        bool supportedLanguage = false;
        for (int i = 0; i < supportedLanguages.Length; i++)
        {
            if (supportedLanguages[i] == language)
            {
                supportedLanguage = true;
            }
        }
        return supportedLanguage;
    }

    public void changeLanguage(string language)
    {
        if(hasLanguageSupport(language))
        {
            PlayerPrefs.SetString(languageKey, language);
            Debug.Log("Language set to " + language);
        } else
        {
            Debug.LogError("Language not supported");
        }
    }

}
