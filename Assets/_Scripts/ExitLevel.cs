using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : Interactable
{
    [SerializeField] GameObject LevelManager;

    public override void Interact()
    {
        LevelManager.GetComponent<LevelManager>().nextLevel();
    }
}
