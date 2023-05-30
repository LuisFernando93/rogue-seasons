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

        //fecha as abas de armas especificas 
        newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);


        WeaponName.GetComponent<TextMeshProUGUI>().text = drop.GetDropName();
        if(drop.GetDropWeaponType() == "Melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetMeleeInfos(0);

            newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            newWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = drop.GetMeleeInfos(1);
        }
        else if (drop.GetDropWeaponType() == "Ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetRangedInfos(0);

            newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            newWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = drop.GetRangedInfos(1);
            newWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = drop.GetRangedInfos(2);
        }
        
    }

    void FillLeftWeaponInfo()
    {
        Transform WeaponName = leftWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = leftWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        //fecha as abas de armas especificas 
        leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponName();
        if (combatManager.GetWeaponType(0) == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetMeleeWeaponDamage(0);

            leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfos(0);
        }
        else if (combatManager.GetWeaponType(0) == "ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponDamage().ToString();

            leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfos(1);
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfos(2);
        }
        

    }
    void FillRightWeaponInfo()
    {
        Transform WeaponName = rightWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = rightWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        //fecha as abas de armas especificas 
        rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponName();
        if (combatManager.GetWeaponType(1) == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetMeleeWeaponDamage(1);

            rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfos(0);
        }
        else if(combatManager.GetWeaponType(1) == "ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponDamage().ToString();

            rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfos(1);
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfos(2);
        }
        

    }

}
