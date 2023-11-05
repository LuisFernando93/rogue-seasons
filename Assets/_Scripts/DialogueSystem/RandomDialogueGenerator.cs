using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDialogueGenerator : MonoBehaviour
{
    [SerializeField] WeightedRandomList<DialogueObject> randomDialogues;
    [SerializeField] bool OneTimeDialogue;
    [SerializeField] DialogueObject dialogue;
    [SerializeField] DialogueActivator dialogueActivator;
    
    private bool EspeciftAlreadyPlayed = false;

    private void Start()
    {
        dialogueActivator.dialogueObject = GetDialogue();
    }

    private DialogueObject GetRandomDialogue()
    {
        return randomDialogues.GetRandom();
    }

    private DialogueObject GetEspecificDialogue()
    {
        return dialogue;
    }

    public DialogueObject GetDialogue()
    {
        if (GetEspecificDialogue() != null && !EspeciftAlreadyPlayed)
        {
            if (OneTimeDialogue) EspeciftAlreadyPlayed = true;
            return GetEspecificDialogue();          
        }
        else
        {
            return GetRandomDialogue();
        }
    }
}
