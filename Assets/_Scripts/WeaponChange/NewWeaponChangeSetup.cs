using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewWeaponChangeSetup : MonoBehaviour
{
    [SerializeField] Transform leftWeaponBox, leftWeaponInfoBox, rightWeaponBox, rightWeaponInfoBox, newWeaponBox, newWeaponInfoBox;
    Drop drop;
    NewCombatManager combatManager;
    Sprite left, right, newWeapon;

    private void Start()
    {
        combatManager = GameObject.FindGameObjectWithTag("Player").GetComponent<NewCombatManager>();
    }
    //Recebe as informa��es do prefab 
    public void SetNewWeapon(GameObject dropSystem)
    {
        drop = dropSystem.GetComponent<Drop>();
    }
    //Preenche todas as informa��es na UI
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
        newWeapon = drop.GetComponent<SpriteRenderer>().sprite;
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

        WeaponName.GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("Name");
        if (drop.GetWeaponInformations("Type") == "Melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("Damage");

            newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            newWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("AtkSpeed");
        }
        else if (drop.GetWeaponInformations("Type") == "Ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("Damage");

            newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            newWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("MaxAmmo");
            newWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("FireFreq");
        }

    }

    void FillLeftWeaponInfo()
    {
        Transform WeaponName = leftWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = leftWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        //fecha as abas de armas especificas 
        leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Name");
        if (combatManager.LeftWeaponType == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Damage");

            leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("AtkSpeed");
        }
        else if (combatManager.LeftWeaponType == "ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Damage");

            leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("FireFreq");
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("MaxAmmo");
        }


    }
    void FillRightWeaponInfo()
    {
        Transform WeaponName = rightWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = rightWeaponInfoBox.transform.GetChild(1).transform.GetChild(0);

        //fecha as abas de armas especificas 
        rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Name");
        if (combatManager.RightWeaponType == "melee")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Damage");

            rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("AtkSpeed");
        }
        else if (combatManager.RightWeaponType == "ranged")
        {
            WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Damage");

            rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("FireFreq");
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("MaxAmmo");
        }


    }

}
