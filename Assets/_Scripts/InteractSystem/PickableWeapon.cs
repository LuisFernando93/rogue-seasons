using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableWeapon : Interactable
{
    CanvasAnimationAccess canvas;
    NewWeaponChangeSetup weaponChange;
    GameObject tempPlayer;
    private float repulsionForce = 5f;

    private void Awake()
    {
        weaponChange = GameObject.Find("Canvas").GetComponent<NewWeaponChangeSetup>();
        GameObject tempGO = GameObject.FindGameObjectWithTag("Canvas");
        canvas = tempGO.GetComponent<CanvasAnimationAccess>();
    }

    public override void Interact()
    {
        canvas.PlayOpenWeaponChange();
        weaponChange.SetNewWeapon(this.gameObject);
        tempPlayer = GameObject.FindGameObjectWithTag("Player");
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
        if (collision.gameObject.CompareTag("Player")) GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().OpenInteractableIcon();

    }
}
