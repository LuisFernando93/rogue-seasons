using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadget : Interactable
{
    [TextArea] string gadgetName, history, description;
    [SerializeField] float lifeModifier, meleeModifier, rangedModifier, speedModifier;
    [SerializeField] bool lifeModified, meleeModified, rangedModified, speedModified;
    Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (lifeModifier > 1 || meleeModifier > 1 || rangedModifier > 1 || speedModifier > 1) Debug.Log("Modificador do gadget " + gadgetName + " maior que 100%");
    }
    public override void Interact()
    {
        if (lifeModified)
        {
            player.IncreaseLifeModifier(lifeModifier);
            //Debug.Log("Vida aumentada em: " + lifeModifier * 100 + "%");
        }
        if (speedModified)
        {
            player.IncreaseSpeedModifier(speedModifier);
            //Debug.Log("Velocidade aumentada em: " + speedModifier * 100 + "%");
        }
        if (meleeModified)
        {
            player.IncreaseMeleeModifier(meleeModifier);
            //Debug.Log("Dano Melee aumentado em: " + meleeModifier * 100 + "%");
        }
        if (rangedModified)
        {
            player.IncreaseRangedModifier(rangedModifier);
            //Debug.Log("Dano Ranged aumentado em: " + rangedModifier * 100 + "%");
        }
    }

}
