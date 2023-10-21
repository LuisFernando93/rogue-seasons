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

    private LanguageOption language;
    private string languageKey = "language";

    public LanguageOption GetLanguage()
    {
        string savedLanguage = PlayerPrefs.GetString(languageKey, "PTBR");
        switch (savedLanguage)
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
                this.language = LanguageOption.PTBR;
                PlayerPrefs.SetString(languageKey, language);
                break;
            case "ING":
                this.language= LanguageOption.ING;
                PlayerPrefs.SetString(languageKey, language);
                break;
        }
    }

}
