using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public enum LanguageOption
    {
        PTBR,
        ING
    }

    private string languageKey = "language";

    public LanguageOption GetLanguage()
    {
        string language = PlayerPrefs.GetString(languageKey, "PTBR");
        switch (language)
        {
            case "PTBR":
                return LanguageOption.PTBR;
            case "ING":
                return LanguageOption.ING;
            default:
                Debug.Log("Erro ao carregar a lingua pelo player prefs. Carregando portugues como padrao");
                return LanguageOption.PTBR;
        }
    }

    public void SetLanguage(string language)
    {
        switch (language)
        {
            case "PTBR":
                PlayerPrefs.SetString(languageKey, language);
                break;
            case "ING":
                PlayerPrefs.SetString(languageKey, language);
                break;
        }
    }

}
