using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : Interactable
{
    [TextArea] public string gadgetName, history, description;


    [SerializeField] float lifeModifier, meleeModifier, rangedModifier, speedModifier, atkSpeedModifier, rechargeModifier, bulletSizeModifier;
    [SerializeField] bool lifeModified, meleeModified, rangedModified, speedModified, atkSpeedModified, rechargeModified, bulletSizeModified;

    GadgetsManager gadget;

    private void Start()
    {
        gadget = GameObject.FindGameObjectWithTag("Player").GetComponent<GadgetsManager>();
        if (lifeModifier > 1 || meleeModifier > 1 || rangedModifier > 1 || speedModifier > 1) Debug.Log("Modificador do gadget " + gadgetName + " maior que 100%");
    }
    public override void Interact()
    {
        if (lifeModified)
        {
            gadget.IncreaseLifeModifier(lifeModifier);
            //Debug.Log("Vida aumentada em: " + lifeModifier * 100 + "%");
        }
        if (speedModified)
        {
            gadget.IncreaseSpeedModifier(speedModifier);
            //Debug.Log("Velocidade aumentada em: " + speedModifier * 100 + "%");
        }
        if (meleeModified)
        {
            gadget.IncreaseMeleeModifier(meleeModifier);
            //Debug.Log("Dano Melee aumentado em: " + meleeModifier * 100 + "%");
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
    }

}
