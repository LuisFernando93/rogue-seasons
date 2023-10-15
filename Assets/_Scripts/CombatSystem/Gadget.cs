using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Gadget : Interactable
{
    [TextArea] public string gadgetNameBR, gadgetNameING, historyBR, historyING, descriptionBR, descriptionING;

    SpriteRenderer sr;
    [SerializeField] Sprite icon;
    [SerializeField] public int Id;
    [SerializeField] float lifeModifier, meleeModifier, rangedModifier, speedModifier, atkSpeedModifier, rechargeModifier, bulletSizeModifier;
    [SerializeField] bool lifeModified, meleeModified, rangedModified, speedModified, atkSpeedModified, rechargeModified, bulletSizeModified;
    string language;
    GadgetsManager gadget;

    GadgetUI UI;

    private void Start()
    {
        gadget = GameObject.FindGameObjectWithTag("Player").GetComponent<GadgetsManager>();
        UI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GadgetUI>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = icon;
        if (Id == null)
        {
            Debug.Log("Gadget: " + gadgetNameING + " sem ID");
        }
        if (lifeModifier > 1 || meleeModifier > 1 || rangedModifier > 1 || speedModifier > 1) Debug.Log("Modificador do gadget " + gadgetNameING + " maior que 100%");
        language = GameObject.FindGameObjectWithTag("Manager").GetComponent<Configuration>().GetLanguage().ToString();
    }
    public override void Interact()
    {
        UI.SetGadget(this.gameObject.GetComponent<Gadget>());
        //SetGadgetModifiers();
        Destroy(this.gameObject, 0.2f);
    }

    public string GetGadgetName()
    {
        if(language == "PTBR")
        {
            return gadgetNameBR;
        }
        else if(language == "ING")
        {
            return gadgetNameING;
        }
        else
        {
            Debug.Log("Sem lingua");
            return gadgetNameING;
        }
    }
    public string GetGadgetHistory()
    {
        if (language == "PTBR")
        {
            return historyBR;
        }
        else if (language == "ING")
        {
            return historyING;
        }
        else
        {
            Debug.Log("Sem lingua");
            return historyING;
        }
    }
    public string GetGadgetDescription()
    {
        if (language == "PTBR")
        {
            return descriptionBR;
        }
        else if (language == "ING")
        {
            return descriptionING;
        }
        else
        {
            Debug.Log("Sem lingua");
            return descriptionING;
        }
    }
    public Sprite GetGadgetIcon()
    {
        return icon;
    }

    public void SetGadgetModifiers()
    {
        if (lifeModified)
        {
            gadget.IncreaseLifeModifier(lifeModifier);
            Debug.Log("Vida aumentada em: " + lifeModifier * 100 + "%");
        }
        if (speedModified)
        {
            gadget.IncreaseSpeedModifier(speedModifier);
            //Debug.Log("Velocidade aumentada em: " + speedModifier * 100 + "%");
        }
        if (meleeModified)
        {
            gadget.IncreaseMeleeModifier(meleeModifier);
            Debug.Log("Dano Melee aumentado em: " + meleeModifier * 100 + "%");
        }
        if (rangedModified)
        {
            gadget.IncreaseRangedModifier(rangedModifier);
            //Debug.Log("Dano Ranged aumentado em: " + rangedModifier * 100 + "%");
        }
        if (atkSpeedModified)
        {
            gadget.IncreaseAtkSpeedModifier(atkSpeedModifier);
        }
        if (rechargeModified)
        {
            gadget.IncreaseRechargeModifier(rechargeModifier);
        }
        if (bulletSizeModified)
        {
            gadget.IncreaseBulletSizeModifier(bulletSizeModifier);
        }

        Destroy(this.gameObject, 0.2f);
    }


}
