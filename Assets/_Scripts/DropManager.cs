using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] int repulsionStrength;
    List<GameObject> allDrops = new List<GameObject>();

    private void FixedUpdate()
    {
        DropDistance();
    }

    public void SetNewDrop(GameObject newDrop)
    {
        allDrops.Add(newDrop);

        //apagar 

        string lista = "Armas:";
                foreach (GameObject i in allDrops)
        {
            lista += " | " + i.GetComponent<DropedWeapon>().GetDropName();
        }
        Debug.Log(lista);
    }

    public List<GameObject> GetAllDrops()
    {
        return allDrops;
    }

    private void DropDistance()
    {
        //cu
    }
}
