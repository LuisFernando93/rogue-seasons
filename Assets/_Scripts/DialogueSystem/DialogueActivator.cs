using UnityEngine;

public class DialogueActivator : Interactable
{
    [SerializeField] public DialogueObject dialogueObject;
    DialogueUI dialogueUI;

    public void UpdateDialogueobject(DialogueObject dialogueObject) //permite mudar o dialogo usando evento, só colocar o dialogue activator no trigger do evento e selecionar este metodo
    {
        this.dialogueObject = dialogueObject;
    }

    public override void Interact()
    {
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        if(GetComponent<DialogueResponseEvents>() != null)
        {
            GetComponent<DialogueResponseEvents>().ChangeResposeDialogue(dialogueObject);
            foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())//atualiza as respostas caso o dialogo tenha mudado com um evento
            {
                if (responseEvents.DialogueObject == dialogueObject)
                {
                    dialogueUI.AddResponseEvents(responseEvents.Events);
                    break;
                }
            }
        }
        dialogueUI.ShowDialogue(dialogueObject);
    }
}
