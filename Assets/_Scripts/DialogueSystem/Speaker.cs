using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speaker : MonoBehaviour
{
    [SerializeField] TMP_Text speakerNameBox;
    [SerializeField] Image playerImage, otherImage;
    [SerializeField]Sprite rougeSprite, blacksmithSprite, guardSprite, summerBossSprite, errorSprite;
    Vector3 centralized = new Vector3(178, -0, 0f);
    Vector3 left = new Vector3(50, -0, 0f);
    Vector3 right = new Vector3(-50, -0, 0f);
    const string OTHER = "Circulo", PLAYER = "<color=red>Rouge</color>", BLACKSMITH = "Blacksmith", GUARD = "Guard", SUMMER = "Summer";

    private void Start()
    {
        playerImage.sprite = rougeSprite;
    }

    public void ChangeSpeaker(string actualSpeaker)
    {
        if (actualSpeaker == "Other")
        {
            actualSpeaker = OTHER;
        }
        else if(actualSpeaker == "Blacksmith" || actualSpeaker == "Ferreira")
        {
            actualSpeaker = BLACKSMITH;
        }
        else if (actualSpeaker == "Guard" || actualSpeaker == "Guarda")
        {
            actualSpeaker = GUARD;
        }
        else if (actualSpeaker == "Player" || actualSpeaker == "Rouge")
        {
            actualSpeaker = PLAYER;
        }
        else if(actualSpeaker == "Summer" || actualSpeaker == "summer")
        {
            actualSpeaker = SUMMER;
        }
        speakerNameBox.text = actualSpeaker;
        ShowSpeaker(actualSpeaker);
        SetOtherImage(actualSpeaker);
    }

    private void ShowSpeaker(string actualSpeaker)
    {
        if (actualSpeaker != PLAYER)
        {
            //habilita a imagem quando ele falar
            otherImage.gameObject.SetActive(true);
        }
        else if (actualSpeaker == PLAYER)
        {
            //habilita a imagem quando o player falar
            playerImage.gameObject.SetActive(true);
        }
        else
        {
            //desabilita todas as imagens para narração
            playerImage.gameObject.SetActive(false);
            otherImage.gameObject.SetActive(false);
        }
    }

    public void ActivateSpeakerBox(bool activate)
    {
        speakerNameBox.transform.parent.gameObject.SetActive(activate);
        if(activate == false)
        {
            playerImage.gameObject.SetActive(false);
            otherImage.gameObject.SetActive(false);
        }
    }

    private void SetOtherImage(string actualSpeaker)
    {
        switch (actualSpeaker)
        {
            case GUARD:
                otherImage.sprite = guardSprite;
                otherImage.color = Color.white;
                playerImage.color = Color.gray;
                break;
            case BLACKSMITH:
                otherImage.sprite = blacksmithSprite;
                otherImage.color = Color.white;
                playerImage.color = Color.gray;
                break;
            case SUMMER:
                otherImage.sprite = summerBossSprite;
                otherImage.color = Color.white;
                playerImage.color = Color.gray;
                break;
            case PLAYER:
                playerImage.color = Color.white;
                otherImage.color = Color.gray;
                break;
            default:
                otherImage.sprite = null;
                Debug.Log("Sprite não encontrado");
                break;
        }
    }
}
