using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedTest : MonoBehaviour
{
    [SerializeField] MeleeWeaponController meleeControler;

    public float speedModifier;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            meleeControler.ModifyAtkSpeed(speedModifier);
        }
    }
}
