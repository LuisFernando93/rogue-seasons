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

    public LanguageOption language;

    public LanguageOption GetLanguage()
    {
        return language;
    }

    public void SetLanguage(string language)
    {
        switch (language)
        {
            case "PTBR":
                this.language = LanguageOption.PTBR;
                break;
            case "ING":
                this.language= LanguageOption.ING;
                break;
        }
    }

    public void Start()
    {
        //Debug.Log(Language.Instance.getSelectedLanguage());
        SetLanguage(Language.Instance.getSelectedLanguage());
    }
}
