using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")] //Cria uma nova opção na unity para criar um novo objeto
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue; //cria uma lista com textos dentro do objeto
    [SerializeField] private Response[] responses;
    
    public string[] Dialogue => dialogue; //não permite que nada de fora mude o texto
    public bool HasResponses => Responses != null && Responses.Length > 0;

    public Response[] Responses => responses;
}
