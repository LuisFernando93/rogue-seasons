using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDrop : MonoBehaviour
{
    [SerializeField] GameObject drop;
    DropedWeapon dropScript;
    WeaponChangeSetup weaponChange;

    private void Start()
    {
        weaponChange = GameObject.Find("Canvas").GetComponent<WeaponChangeSetup>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InstantiateDrop();
        }
    }

    void InstantiateDrop()
    {
        GameObject tempDrop = Instantiate(drop, transform.position, transform.rotation);
        dropScript = tempDrop.GetComponent<DropedWeapon>();
        dropScript.SpawnLoot();
        weaponChange.SetDropItem(tempDrop);
    }
}
