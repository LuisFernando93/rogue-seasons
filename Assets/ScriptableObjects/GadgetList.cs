using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gadget List", menuName = "GadgetList")]
public class GadgetList : ScriptableObject
{
    [SerializeField] List<GameObject> gadgets;
    WeightedRandomList<GameObject> randomGadgets;

    public GameObject GetRandomGadget()
    {
        randomGadgets = new WeightedRandomList<GameObject>();
        foreach (GameObject gadget in gadgets)
        {
            randomGadgets.Add(gadget, 1);
        }
        return randomGadgets.GetRandom();
    }

    public GameObject GetEspecificGadget(int id)
    {
        return gadgets.Find(g => g.GetComponent<Gadget>().Id == id);
    }
}
