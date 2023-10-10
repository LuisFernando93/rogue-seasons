using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Language", menuName ="LanguageManager")]
public class Language : ScriptableObject
{
    [SerializeField] private string selectedLanguage;
    private string[] supportedLanguages = {"PT-BR","ENG"};

    public string getSelectedLanguage()
    {
        return selectedLanguage;
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
            selectedLanguage = language;
        } else
        {
            Debug.LogError("Language not supported");
        }
    }

}
