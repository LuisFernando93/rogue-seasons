using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDrop : MonoBehaviour
{
    [SerializeField] GameObject drop;
    DropedWeapon dropScript;
    DropManager dropManager;

    private void Start()
    {
        dropManager = GameObject.Find("Managers").GetComponent<DropManager>();
    }
    //modificar para chamar o drop dps
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            InstantiateDrop();
        }
    }

    public void InstantiateDrop()
    {
        GameObject tempDrop = Instantiate(drop, transform.position, transform.rotation);
        dropScript = tempDrop.GetComponent<DropedWeapon>();
        dropScript.SpawnLoot();
        dropManager.SetNewDrop(tempDrop);
        
    }

}
