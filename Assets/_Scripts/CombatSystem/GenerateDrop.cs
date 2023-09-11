using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDrop : Interactable
{
    [SerializeField] GameObject drop;
    GameObject player;
    Drop dropScript;

    //modificar para chamar o drop dps
    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InstantiateDrop();
        }
    }*/

    public void RandomDrop()
    {
        GameObject tempDrop = Instantiate(drop, transform.position, transform.rotation);
        dropScript = tempDrop.GetComponent<Drop>();
        dropScript.GenerateRandomWeapon();
    }

    /*public void InstantiateNewDrop(int weaponToDrop)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject tempDrop = Instantiate(drop, player.transform.position, player.transform.rotation);
        tempDrop.GetComponent<Drop>().DropPlayerWeapon(weaponToDrop);
    }*/

    public void SetDrop(GameObject dropToSet)
    {
        drop = dropToSet;
    }

    public override void Interact()
    {
        RandomDrop();
        DestroyChest();
    }
    public void DestroyChest()
    {
        Destroy(this.gameObject, 0.1f);
    }
}
