using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedWeapon : MonoBehaviour
{
    [SerializeField] WeightedRandomList<Transform> Weapons;
    Transform prefab;  
    SpriteRenderer sr;
    MeleeWeaponController meleeWeapon;
    RangedWeaponController rangedWeapon;
    public Sprite icon;
    //MagicWeaponController magicWeapon;

    bool isRanged = false, isMelee = false, isMagic = false;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SpawnLoot();
        
    }

    //Usado por outros scripts para spawnar a arma
    public void SpawnLoot()
    {
        ChooseWeaponLoot();
    }
    //Escolhe aleatoriamente a arma que irá spawnar de acordo com a lista de armas,
    void ChooseWeaponLoot()
    {
        prefab = Weapons.GetRandom();
        GetWeaponType();
        SetSprite();
    }

    //Determina o tipo de arma para permitir facil acesso a features dos diferentes tipos de armas
    void GetWeaponType()
    {
        if (prefab.TryGetComponent(out MeleeWeaponController melee) == true)
        {
            meleeWeapon = prefab.GetComponent<MeleeWeaponController>();
            isMelee = true;
        }
        else if (prefab.TryGetComponent(out RangedWeaponController ranged) == true)
        {
            rangedWeapon = prefab.GetComponent<RangedWeaponController>();
            isRanged = true;
        }
        /*else if (prefab.TryGetComponent(out MagicWeaponController magic) == true)
        {
            magicWeapon = prefab.GetComponent<MagicWeaponController>();
            isMagic = true;
        }*/
    }
    //Define o sprite que será mostrado de acordo com o objeto selecionado 
    void SetSprite()
    { 
        if (isMelee)
        {
            sr.sprite = meleeWeapon.GetIcon();
        }
        else if (isRanged)
        {
            sr.sprite = rangedWeapon.GetIcon();
        }
        /*else if (isMagic)
        {
            sr.sprite = magicWeapon.GetIcon();
        }*/
        icon = sr.sprite;
          
    }
    //Manda o prefab para outros scripts
    public Transform GetPrefab()
    {
        return prefab;
    }

    public Sprite GetSprite()
    {
        return icon;
    }
}
