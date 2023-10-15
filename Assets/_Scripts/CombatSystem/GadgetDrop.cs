using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetDrop : Interactable
{
    [SerializeField] GadgetList gadgetList;
    [SerializeField] bool randomGadget = true;
    [SerializeField] int ID;

    public override void Interact()
    {
        InstantiateGadget(ID);
        Destroy(this.gameObject, 0.1f);
    }

    private void InstantiateGadget(int id)
    {
        if (randomGadget)
        {
            GameObject gadget = Instantiate(gadgetList.GetRandomGadget(), transform.position, transform.rotation);
            //Debug.Log("Random Gadget");
        }
        else
        {
            GameObject gadget = Instantiate(gadgetList.GetEspecificGadget(id), transform.position, transform.rotation);
            //Debug.Log("Especific Gadget: " + ID);
        }

    }

}
