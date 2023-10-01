using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")] //Cria uma nova opção na unity para criar um novo objeto
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogueBR, dialogueING; //cria uma lista com textos dentro do objeto
    [SerializeField] private Response[] responses;

    public string[] Dialogue
    {
        get
        {
            Configuration.LanguageOption language = GameObject.FindGameObjectWithTag("Manager").GetComponent<Configuration>().GetLanguage();
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    return dialogueBR;
                case Configuration.LanguageOption.ING:
                    return dialogueING;
                default:
                    return dialogueBR;
            }
        }
    } //não permite que nada de fora mude o texto
    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
