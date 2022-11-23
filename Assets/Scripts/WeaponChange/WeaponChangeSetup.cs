using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponChangeSetup : MonoBehaviour
{
    [SerializeField] Transform leftWeaponBox, leftWeaponInfoBox, rightWeaponBox, rightWeaponInfoBox, newWeaponBox, newWeaponInfoBox;
    DropedWeapon drop;
    CombatManager combatManager;
    Sprite left, right, newWeapon;

    private void Start()
    {
        combatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CombatManager>();
    }
    //Recebe as informações do prefab 
    public void SetDropItem(GameObject dropSystem)
    {
        drop = dropSystem.GetComponent<DropedWeapon>();
    }
    //Preenche todas as informações na UI
    public void FillUI()
    {
        UpdateIcons();
        FillNewWeaponInfo();
        FillLeftWeaponInfo();
        FillRightWeaponInfo();
    }

    void UpdateIcons()
    {
        left = combatManager.GetWeaponIcon(0);
        right = combatManager.GetWeaponIcon(1);
        newWeapon = drop.GetSprite();
        SetIcons();
    }

    void SetIcons()
    {
        leftWeaponBox.GetComponent<Image>().sprite = left;
        rightWeaponBox.GetComponent<Image>().sprite = right;
        newWeaponBox.GetComponent<Image>().sprite = newWeapon;
    }

    void FillNewWeaponInfo()
    {
        Transform WeaponName = newWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = newWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);


        WeaponName.GetComponent<TextMeshProUGUI>().text = drop.GetDropName();
        if(drop.GetDropWeaponType() == "Melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetMeleeDropDamage();
        }
        else
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetDropDamage().ToString();
        }
        
    }

    void FillLeftWeaponInfo()
    {
        Transform WeaponName = leftWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = leftWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponName();
        if (combatManager.GetWeaponType(0) == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetMeleeWeaponDamage(0);
        }
        else
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponDamage().ToString();
        }
        

    }
    void FillRightWeaponInfo()
    {
        Transform WeaponName = rightWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = rightWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponName();
        if (combatManager.GetWeaponType(1) == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetMeleeWeaponDamage(1);
        }
        else
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponDamage().ToString();
        }
        

    }
}
