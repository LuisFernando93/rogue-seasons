using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    public bool IsOpen { get; private set; }

    private Speaker speaker;
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private DialogueObject dialogueObjectActor;

    const string SPEAKER_TAG = "speaker", NARRATOR_TAG = "narrator";

    private void Start()
    {
        typewriterEffect = GetComponent<TypewriterEffect>();//chama o type writter
        responseHandler = GetComponent<ResponseHandler>();//chama o handler
        speaker = GetComponent<Speaker>();
    }
  
    //Chama o dialogo, mais recomendado usar o Interact do Dialogue activator
    public void ShowDialogue(DialogueObject dialogueObject)
    {  
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i =0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            dialogue = HandleTags(dialogue);
            yield return RunTypingEffect(dialogue);
            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break; //se tiver uma resposta ele sai do for
            yield return null; //impede que passa p prox dialogo quando vc pula o dialogo
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0)); //passa p prox dialogo c botão
        }
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);//mostra as respostas caso tenha no final do dialogo
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue) //torna possivel pular o efeito de digitar 
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    private string HandleTags(string dialogue)
    {
        string[] splitText = dialogue.Split('#');
        if (splitText.Length != 1)
        {
            if (splitText.Length != 2)
            {
                Debug.LogError("mais de um # encontrado.");
            }
            string tagMass = splitText[0].Trim();
            string splipDialogue = splitText[1].Trim();

            string[] splitTag = tagMass.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("mais de um : encontrado.");
            }
            string tagKey = splitTag[0];
            string tagValue = splitTag[1];

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    speaker.ActivateSpeakerBox(true);
                    speaker.ChangeSpeaker(tagValue);
                    break;
                case NARRATOR_TAG:
                    speaker.ActivateSpeakerBox(false);
                    break;
                default:
                    Debug.LogWarning("Tag recebida, porem, não reconhecida." + tagKey);
                    break;
            }
            return splipDialogue;
        }
        return dialogue;
        
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;      
    }
}

