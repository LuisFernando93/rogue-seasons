using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactable
{
    CanvasAnimationAccess canvas;
    WeaponChangeSetup weaponChange;
    GameObject tempPlayer;
    private float repulsionForce = 5f;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Drop"))
        {
            if(collision.gameObject != this.gameObject)
            {
                Rigidbody2D otherRigidbody = collision.GetComponent<Rigidbody2D>();

                if (otherRigidbody != null)
                {
                    // Calcula a direção da repulsão e aplica o impulso
                    Vector2 repulsionDirection = (transform.position - otherRigidbody.transform.position).normalized;
                    otherRigidbody.AddForce(-repulsionDirection * repulsionForce, ForceMode2D.Impulse);
                }
            }
            

        }
    }
}
