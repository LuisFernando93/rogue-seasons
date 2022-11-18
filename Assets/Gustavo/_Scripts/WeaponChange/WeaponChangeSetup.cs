using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChangeSetup : MonoBehaviour
{
    [SerializeField] Transform leftWeaponBox, rightWeaponBox, newWeaponBox;
    DropedWeapon drop;
    CombatManager combatManager;
    Sprite left, right, newWeapon;

    private void Start()
    {
        combatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatManager>();
        drop = GameObject.FindGameObjectWithTag("Drop").GetComponent<DropedWeapon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            UpdateIcons();
        }
    }
    public void UpdateIcons()
    {
        left = combatManager.GetWeaponIcon(0);
        right = combatManager.GetWeaponIcon(1);
        Debug.Log(drop.name);
        newWeapon = drop.GetSprite();
        SetIcons();
    }

    void SetIcons()
    {
        leftWeaponBox.GetComponent<Image>().sprite = left;
        rightWeaponBox.GetComponent<Image>().sprite = right;
        newWeaponBox.GetComponent<Image>().sprite = newWeapon;
    }
}
