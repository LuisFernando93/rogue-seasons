using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactable
{
    CanvasAnimationAccess canvas;
    WeaponChangeSetup weaponChange;
    GameObject tempPlayer;

    private void Start()
    {
        weaponChange = GameObject.Find("Canvas").GetComponent<WeaponChangeSetup>();
        tempPlayer = GameObject.FindGameObjectWithTag("Player");
        GameObject tempGO = GameObject.FindGameObjectWithTag("Canvas");
        canvas = tempGO.GetComponent<CanvasAnimationAccess>();
    }

    public override void Interact()
    {
        canvas.PlayOpenWeaponChange();
        weaponChange.SetDropItem(this.gameObject);
        tempPlayer.GetComponent<WeaponChoice>().SetNewWeapon(this.gameObject);
    }
}
