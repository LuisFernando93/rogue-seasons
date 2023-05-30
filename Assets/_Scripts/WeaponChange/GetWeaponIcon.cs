using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWeaponIcon : MonoBehaviour
{
    CombatManager combatManager;
    [SerializeField] Transform leftWeaponBox, rightWeaponBox;
    Sprite left, right;

    private void Start()
    {
        combatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatManager>();
    }

    public void UpdateIcons()
    {
        
        left = combatManager.GetWeaponIcon(0);
        right = combatManager.GetWeaponIcon(1);
        SetIcons();
    }

    void SetIcons()
    {
        leftWeaponBox.GetComponent<Image>().sprite = left;
        rightWeaponBox.GetComponent<Image>().sprite = right;
    }
}
