using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] public string history;

    public abstract void GetWeaponHistory();

    public abstract string GetWeaponInfos(string infoNeeded);



}
