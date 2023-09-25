using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Speaker : MonoBehaviour
{
    [SerializeField] TMP_Text speakerNameBox;
    [SerializeField] Transform _playerImage, _otherImage;
    Image playerImage, otherImage;
    Vector3 centralized = new Vector3(178, -0, 0f);
    Vector3 left = new Vector3(50, -0, 0f);
    Vector3 right = new Vector3(-50, -0, 0f);
    string OTHER = "Circulo", PLAYER = "Rouge";

    private void Start()
    {
        playerImage = _playerImage.GetComponent<Image>();
        otherImage = _otherImage.GetComponent<Image>();
    }

    public void ChangeSpeaker(string actualSpeaker)
    {
        if (actualSpeaker == "other" || actualSpeaker == "Other")
        {
            actualSpeaker = OTHER;
        }
        else if (actualSpeaker == "rouge" || actualSpeaker == "Rouge")
        {
            actualSpeaker = PLAYER;
        }
        speakerNameBox.text = actualSpeaker;
        ShowSpeaker(actualSpeaker);
    }

    private void ShowSpeaker(string actualSpeaker)
    {
        if (actualSpeaker == OTHER)
        {
            otherImage.gameObject.SetActive(true);

            /*if (playerImage.gameObject.activeSelf == false)
            {
                otherImage.transform.position = centralized;
            }
            else
            {
                otherImage.transform.position = right;
            }*/

            otherImage.color = new Color32(255, 255, 255, 255);
            playerImage.color = new Color32(255, 165, 0, 125);
        }
        else if (actualSpeaker == PLAYER)
        {
            playerImage.gameObject.SetActive(true);

            /*if(otherImage.gameObject.activeSelf == false)
            {
                playerImage.transform.position = centralized;
            }
            else{
                playerImage.transform.position = left;
            }*/

            playerImage.color = new Color32(255, 165, 0, 255);
            otherImage.color = new Color32(255, 255, 255, 125);

        }
        else
        {
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

}
