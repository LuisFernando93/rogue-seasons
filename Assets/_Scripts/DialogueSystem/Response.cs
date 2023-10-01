using UnityEngine;

[System.Serializable] //torna possivel q a classe seja serializada
public class Response
{
    [SerializeField] private string responseTextBR, responseTextING;
    [SerializeField] private DialogueObject dialogueObject;

    //os get e set 

    public string ResponseText
    {
        get
        {
            Configuration.LanguageOption language = GameObject.FindGameObjectWithTag("Manager").GetComponent<Configuration>().GetLanguage();
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    return responseTextBR;
                case Configuration.LanguageOption.ING:
                    return responseTextING;
                default:
                    return responseTextBR;
            }
        }
    }
    public DialogueObject DialogueObject => dialogueObject;

}
