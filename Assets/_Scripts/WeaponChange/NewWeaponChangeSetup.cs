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
    Configuration.LanguageOption language;
    public bool IsOpen { get; private set; }

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
        IsOpen = true;
        language = GameObject.FindGameObjectWithTag("Manager").GetComponent<Configuration>().GetLanguage();
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
        Transform WeaponDamage = newWeaponInfoBox.transform.GetChild(1);

        //fecha as abas de armas especificas 
        newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = drop.GetWeaponInformations("Name");
        if (drop.GetWeaponInformations("Type") == "Melee")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: "+drop.GetWeaponInformations("Damage");
                    newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    newWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Vel. Atq: "+drop.GetWeaponInformations("AtkSpeed");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + drop.GetWeaponInformations("Damage");
                    newWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    newWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Atk Speed: " + drop.GetWeaponInformations("AtkSpeed");
                    break;
            }
        }
        else if (drop.GetWeaponInformations("Type") == "Ranged")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: "+drop.GetWeaponInformations("Damage");
                    newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    newWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Munição: "+drop.GetWeaponInformations("MaxAmmo");
                    newWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Freq. Tiros: "+drop.GetWeaponInformations("FireFreq");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + drop.GetWeaponInformations("Damage");
                    newWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    newWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Ammo: " + drop.GetWeaponInformations("MaxAmmo");
                    newWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Fire Freq: " + drop.GetWeaponInformations("FireFreq");
                    break;
            }
        }

    }

    void FillLeftWeaponInfo()
    {
        Transform WeaponName = leftWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = leftWeaponInfoBox.transform.GetChild(1);

        //fecha as abas de armas especificas 
        leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Name");
        if (combatManager.LeftWeaponType == "melee")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: " + combatManager.GetLeftWeaponInfo("Damage");
                    leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    leftWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Vel. Atq: " + combatManager.GetLeftWeaponInfo("AtkSpeed");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + combatManager.GetLeftWeaponInfo("Damage");
                    leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    leftWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Atk Speed: " + combatManager.GetLeftWeaponInfo("AtkSpeed");
                    break;
            }
            /*WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Damage");
            leftWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("AtkSpeed");*/
        }
        else if (combatManager.LeftWeaponType == "ranged")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: "+ combatManager.GetLeftWeaponInfo("Damage");
                    leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Munição: "+ combatManager.GetLeftWeaponInfo("MaxAmmo");
                    leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Freq. Tiros: "+ combatManager.GetLeftWeaponInfo("FireFreq");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + combatManager.GetLeftWeaponInfo("Damage");
                    leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Ammo: " + combatManager.GetLeftWeaponInfo("MaxAmmo");
                    leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Fire Freq: " + combatManager.GetLeftWeaponInfo("FireFreq");
                    break;
            }
            /*WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("Damage");
            leftWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("FireFreq");
            leftWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetLeftWeaponInfo("MaxAmmo");*/
        }


    }
    void FillRightWeaponInfo()
    {
        Transform WeaponName = rightWeaponInfoBox.transform.GetChild(0);
        Transform WeaponDamage = rightWeaponInfoBox.transform.GetChild(1);

        //fecha as abas de armas especificas 
        rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(false);
        rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(false);

        WeaponName.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Name");
        if (combatManager.RightWeaponType == "melee")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: " + combatManager.GetRightWeaponInfo("Damage");
                    rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    rightWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Vel. Atq: " + combatManager.GetRightWeaponInfo("AtkSpeed");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + combatManager.GetRightWeaponInfo("Damage");
                    rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
                    rightWeaponInfoBox.transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Atk Speed: " + combatManager.GetRightWeaponInfo("AtkSpeed");
                    break;
            }
            /*WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Damage");
            rightWeaponInfoBox.transform.GetChild(2).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("AtkSpeed");*/
        }
        else if (combatManager.RightWeaponType == "ranged")
        {
            switch (language)
            {
                case Configuration.LanguageOption.PTBR:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Dano: " + combatManager.GetRightWeaponInfo("Damage");
                    rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Munição: " + combatManager.GetRightWeaponInfo("MaxAmmo");
                    rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Freq. Tiros: " + combatManager.GetRightWeaponInfo("FireFreq");
                    break;
                case Configuration.LanguageOption.ING:
                    WeaponDamage.GetComponent<TextMeshProUGUI>().text = " Damage: " + combatManager.GetRightWeaponInfo("Damage");
                    rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
                    rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " Ammo: " + combatManager.GetRightWeaponInfo("MaxAmmo");
                    rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = " Fire Freq: " + combatManager.GetRightWeaponInfo("FireFreq");
                    break;
            }
            /*WeaponDamage.GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("Damage");
            rightWeaponInfoBox.transform.GetChild(3).gameObject.SetActive(true);
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("FireFreq");
            rightWeaponInfoBox.transform.GetChild(3).transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = combatManager.GetRightWeaponInfo("MaxAmmo");*/
        }


    }

    public void IsNotOpen()
    {
        IsOpen = false;
    }

}
