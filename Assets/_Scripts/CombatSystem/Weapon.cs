using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string[] weaponInfo = new string[5];

    [SerializeField] public string history;

    public abstract void GetWeaponHistory();

    public abstract string GetWeaponInfos(string infoNeeded);



}
