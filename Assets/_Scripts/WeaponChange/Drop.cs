using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    /*
    0 =  nome
    1 = tipo

    Melee : 2 - Damage 3 - AtkSpeed
    Ranged : 2 - Damage 3 - MaxAmmo 4 - FireFreq 5 - Recharge
    Magic:
    */


    [SerializeField] WeightedRandomList<Transform> Weapons;
    GameObject player;
    Transform weapon;
    WeaponList weaponList;

    string weaponName, weaponDamage, weaponType, meleeWeaponAtkSpeed, rangedWeaponAmmo, rangedWeaponFireFreq, rangedWeaponRecharge, weaponID;

    public void GenerateRandomWeapon()
    {
        weapon = Weapons.GetRandom();
        SetWeaponInformations();
        weaponID = weapon.name;
    }

    public void DropPlayerWeapon(int playerWeaponToDrop)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (playerWeaponToDrop == 0)
        {
            weapon = player.transform.GetChild(0);
        }
        else if (playerWeaponToDrop == 1)
        {
            weapon = player.transform.GetChild(1);
        }
        else
        {
            Debug.Log("Valor inserido incorreto (apenas 0/1)");
        }
        SetWeaponInformations();
        weaponID = weapon.name;

    }

    private void SetWeaponInformations()
    {
        weaponName = weapon.name;
        if (weapon.tag == "meleeWeapon")
        {
            weaponType = "Melee";
            weaponDamage = weapon.GetComponent<MeleeWeaponController>().GetWeaponInfos("Damage");
            meleeWeaponAtkSpeed = weapon.GetComponent<MeleeWeaponController>().GetWeaponInfos("AtkSpeed");
            GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<MeleeWeaponController>().GetIcon();
        }
        else if (weapon.tag == "rangedWeapon")
        {
            weaponType = "Ranged";
            weaponDamage = weapon.GetComponent<RangedWeaponController>().GetWeaponInfos("Damage");
            rangedWeaponAmmo = weapon.GetComponent<RangedWeaponController>().GetWeaponInfos("MaxAmmo");
            rangedWeaponFireFreq = weapon.GetComponent<RangedWeaponController>().GetWeaponInfos("FireFreq");
            rangedWeaponRecharge = weapon.GetComponent<RangedWeaponController>().GetWeaponInfos("Recharge");
            GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<RangedWeaponController>().GetIcon();
        }
        else if (weapon.tag == "magicWeapon")
        {
            Debug.Log("Sem armas magicas no momento");
        }
    }

    public string GetWeaponInformations(string infoNeeded)
    {
        if (infoNeeded == "Name")
        {
            return weaponName;
        }
        else if (infoNeeded == "Type")
        {
            return weaponType;
        }

        else if (infoNeeded == "Damage")
        {
            return weaponDamage;
        }
        else if (infoNeeded == "AtkSpeed")
        {
            return meleeWeaponAtkSpeed;
        }
        else if (infoNeeded == "MaxAmmo")
        {
            return rangedWeaponAmmo;
        }
        else if (infoNeeded == "FireFreq")
        {
            return rangedWeaponFireFreq;
        }
        else if (infoNeeded == "Recharge")
        {
            return rangedWeaponRecharge;
        }
        else
        {
            Debug.Log("Informação de arma Ranged não encontrada. Possivel erro de solicitacao. Input: " + infoNeeded);
            return null;
        }
    }

    public Transform GetWeapon()
    {
        weaponID = weaponID.Replace("(Clone)", "");
        weaponList = GameObject.FindGameObjectWithTag("Manager").GetComponent<WeaponList>();
        return weaponList.GetWeaponByID(weaponID).transform;
    }

    public void DestroyDrop()
    {
        Destroy(this.gameObject);
    }

}
