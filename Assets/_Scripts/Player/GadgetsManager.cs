using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetsManager : MonoBehaviour
{
    NewCombatManager combatManager;
    Player player;

    List<float> lifeModifier = new List<float>(), speedModifier = new List<float>(), meleeModifier = new List<float>(), rangedModifier = new List<float>();
    List<float> atkSpeedModifier = new List<float>(), rechargeModifier = new List<float>(), bulletSizeModifier = new List<float>(), dashModifier = new List<float>();
    public void Start()
    {
        player = GetComponent<Player>();
        combatManager = GetComponent<NewCombatManager>();

        UpdateStatus();
    }

    //Recebe o modificador adicionando-o na lista

    public void IncreaseLifeModifier(float modifier)
    {
        lifeModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseSpeedModifier(float modifier)
    {
        speedModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseMeleeModifier(float modifier)
    {
        meleeModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseRangedModifier(float modifier)
    {
        rangedModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseAtkSpeedModifier(float modifier)
    {
        atkSpeedModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseRechargeModifier(float modifier)
    {
        rechargeModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseBulletSizeModifier(float modifier)
    {
        bulletSizeModifier.Add(modifier);
        UpdateStatus();
    }
    public void IncreaseDashModifier(float modifier)
    {
        dashModifier.Add(modifier);
        UpdateStatus();
    }

    //Implementa o modificador

    public void UpdateStatus()
    {
        float tempLife = 0, tempSpeed = 0, tempMeleeDamage = 0, tempRangedDamage = 0, tempAtkSpeed = 0, tempRechargeSpeed = 0, tempBulletSize = 0, tempDash = 0;

        if (lifeModifier.Count != 0)
        {
            foreach (float modifier in lifeModifier) tempLife += modifier;
            //Debug.Log("Aumento da vida: " + tempLife * 100 + "%");
        }
        if (speedModifier.Count != 0)
        {
            foreach (float modifier in speedModifier) tempSpeed += modifier;
            //Debug.Log("Aumento da velocidade: " + tempSpeed * 100 + "%");
        }
        if (meleeModifier.Count != 0)
        {
            foreach (float modifier in meleeModifier) tempMeleeDamage += modifier;
            //Debug.Log("Aumento do dano Melee: " + tempMeleeDamage * 100 + "%");

            if (combatManager.RightWeaponType == "melee")
            {
                transform.GetChild(1).GetComponent<MeleeWeaponController>().ModifyMeleeDamage(tempMeleeDamage);
            }
            if (combatManager.LeftWeaponType == "melee")
            {
                transform.GetChild(0).GetComponent<MeleeWeaponController>().ModifyMeleeDamage(tempMeleeDamage);
            }
        }
        if (rangedModifier.Count != 0)
        {
            foreach (float modifier in rangedModifier) tempRangedDamage += modifier;
            //Debug.Log("Aumento do dano Ranged: " + tempRangedDamage * 100 + "%");
            if (combatManager.RightWeaponType == "ranged")
            {
                combatManager.ModifyRangedDamage(tempRangedDamage);
            }
            if (combatManager.LeftWeaponType == "ranged")
            {
                combatManager.ModifyRangedDamage(tempRangedDamage);
            }
        }
        if (atkSpeedModifier.Count != 0)
        {
            foreach (float modifier in atkSpeedModifier) tempAtkSpeed += modifier;
            //Debug.Log("Aumento de Atk Speed: " + tempAtkSpeed * 100 + "%");

            if (combatManager.RightWeaponType == "melee")
            {
                transform.GetChild(1).GetComponent<MeleeWeaponController>().ModifyAtkSpeed(tempAtkSpeed);
            }
            if (combatManager.LeftWeaponType == "melee")
            {
                transform.GetChild(0).GetComponent<MeleeWeaponController>().ModifyAtkSpeed(tempAtkSpeed);
            }
        }
        if (rechargeModifier.Count != 0)
        {
            foreach (float modifier in rechargeModifier) tempRechargeSpeed += modifier;
            //Debug.Log("Diminuição do tempo de recarga: " + tempRechargeSpeed * 100 + "%");
            if (combatManager.RightWeaponType == "ranged")
            {
                transform.GetChild(1).GetComponent<RangedWeaponController>().ModifyRechargeTime(tempRechargeSpeed);
            }
            if (combatManager.LeftWeaponType == "ranged")
            {
                transform.GetChild(0).GetComponent<RangedWeaponController>().ModifyRechargeTime(tempRechargeSpeed);
            }
        }
        if (bulletSizeModifier.Count != 0)
        {
            foreach (float modifier in bulletSizeModifier) tempBulletSize += modifier;
            Debug.Log("Aumento do tamanho do tiro: " + tempBulletSize * 100 + "%");
            if (combatManager.RightWeaponType == "ranged")
            {
                transform.GetChild(1).GetComponent<RangedWeaponController>().ModifyBulletSize(tempBulletSize);
            }
            if (combatManager.LeftWeaponType == "ranged")
            {
                transform.GetChild(0).GetComponent<RangedWeaponController>().ModifyBulletSize(tempBulletSize);
            }
        }
        if(dashModifier.Count != 0)
        {
            foreach (float modifier in dashModifier) tempDash += modifier;
        }
        player.SetLifeValue(tempLife);
        player.SetMoveSpeedValue(tempSpeed);
        player.SetDashValue(tempDash);
    }
}
