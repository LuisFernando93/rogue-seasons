using UnityEngine;

[System.Serializable] //torna possivel q a classe seja serializada
public class Response
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    //os get e set 

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;

}
