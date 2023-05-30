using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChoice : MonoBehaviour
{
    [SerializeField] Player player;
    CombatManager combatManager;
    Transform NewWeapon;

    private void Start()
    {
        combatManager = player.GetComponent<CombatManager>();
    }

    //Pega o prefab do spawn
    void GetNewWeapon()
    {
        GameObject tempNewGameObject = GameObject.FindGameObjectWithTag("Drop");
        DropedWeapon tempNewWeapon = tempNewGameObject.GetComponent<DropedWeapon>();
        NewWeapon = tempNewWeapon.GetPrefab();
        tempNewWeapon.DestroyDrop();
    }

    //Muda a arma direita
    public void ChangeRightWeapon()
    {
        GetNewWeapon();
        Instantiate(NewWeapon, player.transform);
        player.transform.GetChild(3).SetSiblingIndex(1);
        player.transform.GetChild(2).SetSiblingIndex(3);
        combatManager.WeaponSelect();
        DetectWeaponTypeAndDestroy(3);
    }
    //Muda a arma esquerda
    public void ChangeLeftWeapon()
    {
        GetNewWeapon();
        Instantiate(NewWeapon, player.transform);
        player.transform.GetChild(3).SetSiblingIndex(0);
        player.transform.GetChild(1).SetSiblingIndex(3);
        combatManager.WeaponSelect();
        DetectWeaponTypeAndDestroy(3);  
        
    }

    //Destroi a arma anterior de uma maneira segura
    void DetectWeaponTypeAndDestroy(int i)
    {
        if (player.transform.GetChild(i).TryGetComponent(out MeleeWeaponController melee) == true)
        {
            MeleeWeaponController meleeWeapon = player.transform.GetChild(i).GetComponent<MeleeWeaponController>();
            meleeWeapon.DestroyThis();
        }
        else if (player.transform.GetChild(i).TryGetComponent(out RangedWeaponController ranged) == true)
        {
            RangedWeaponController rangedWeapon = player.transform.GetChild(i).GetComponent<RangedWeaponController>();
            rangedWeapon.DestroyThis();
        }
        /*else if (combatManager.GetWeaponType(i) == "magic")
        {
            Debug.Log("É Magic");
            MagicWeaponController magicWeapon = prefab.GetComponent<MagicWeaponController>();
            magicWeapon.DestroyThis();
        }*/
    }
}
