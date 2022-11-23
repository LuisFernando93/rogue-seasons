using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactable
{
    CanvasAnimationAccess canvas;


    private void Start()
    {
        GameObject tempGO = GameObject.FindGameObjectWithTag("Canvas");
        canvas = tempGO.GetComponent<CanvasAnimationAccess>();
    }

    public override void Interact()
    {
        canvas.PlayOpenWeaponChange();
    }
}
