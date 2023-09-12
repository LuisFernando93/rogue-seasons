using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : MonoBehaviour
{
    [SerializeField] GameObject[] weaponsPrefab;
    string[] weaponsID;

    private void Start()
    {
        GenerateIDs();
    }
    private void GenerateIDs()
    {
        int i = 0;
        weaponsID = new string[weaponsPrefab.Length];

        foreach (GameObject weapon in weaponsPrefab)
        {
            weaponsID[i] = weapon.name;
            i++;
        }
    }

    public GameObject GetWeaponByID(string ID)
    {
        int index = 0;
        for (int i = 0; i < weaponsID.Length; i++)
        {
            if(weaponsID[i] == ID)
            {
                index = i;
                break;
            }
        }
        return weaponsPrefab[index];
    }
}
