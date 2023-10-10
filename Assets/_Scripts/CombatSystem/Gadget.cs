using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Gadget : Interactable
{
    [TextArea] public string gadgetName, history, description;

    SpriteRenderer sr;
    [SerializeField] Sprite icon;
    [SerializeField] public int Id;
    [SerializeField] float lifeModifier, meleeModifier, rangedModifier, speedModifier, atkSpeedModifier, rechargeModifier, bulletSizeModifier;
    [SerializeField] bool lifeModified, meleeModified, rangedModified, speedModified, atkSpeedModified, rechargeModified, bulletSizeModified;

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
            Debug.Log("Gadget: " + gadgetName + " sem ID");
        }
        if (lifeModifier > 1 || meleeModifier > 1 || rangedModifier > 1 || speedModifier > 1) Debug.Log("Modificador do gadget " + gadgetName + " maior que 100%");
    }
    public override void Interact()
    {
        UI.SetGadget(this.gameObject.GetComponent<Gadget>());
        //SetGadgetModifiers();
    }

    public string GetGadgetName()
    {
        return gadgetName;
    }
    public string GetGadgetHistory()
    {
        return history;
    }
    public string GetGadgetDescription()
    {
        return description;
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
