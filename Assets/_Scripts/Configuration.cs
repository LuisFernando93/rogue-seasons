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
}
