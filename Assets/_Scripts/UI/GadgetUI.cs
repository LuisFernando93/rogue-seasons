using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetUI : MonoBehaviour
{
    [SerializeField] GameObject gadgetContainer;
    [SerializeField] Image gadgetIcon;
    [SerializeField] TextMeshProUGUI gadgetName, gadgetDescription, gadgetHistory;

    Gadget gadget;

    private void Start()
    {
        InitialStatus();
    }

    private void InitialStatus()
    {
        gadgetContainer.SetActive(false);
        gadgetIcon.gameObject.SetActive(true);
        gadgetName.gameObject.SetActive(true);
        gadgetDescription.gameObject.SetActive(true);
        gadgetHistory.gameObject.SetActive(true);
        gadgetHistory.transform.parent.gameObject.SetActive(false);
    }

    private void SetInfos()
    {
        gadgetIcon.sprite = gadget.GetGadgetIcon();
        gadgetName.text = gadget.GetGadgetName();
        gadgetDescription.text = gadget.GetGadgetDescription();
        gadgetHistory.text = gadget.GetGadgetHistory();
    }

    public void SetGadget(Gadget gadget)
    {
        this.gadget = gadget;
        gadgetContainer.SetActive(true);
        SetInfos();
    }

    public void AcceptGadget()
    {
        gadget.SetGadgetModifiers();
        gadget.DesactiveInteracIcon();
        Destroy(gadget.gameObject,0.2f);
    }

}
