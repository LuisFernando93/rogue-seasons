using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationAccess : MonoBehaviour
{
    Animator animator;
    [SerializeField] AnimationClip openWeaponChange;
    [SerializeField] AnimationClip closeWeaponChange;
    NewWeaponChangeSetup weaponChangeSetup;

    private void Start()
    {
        animator = GetComponent<Animator>();
        weaponChangeSetup = GetComponent<NewWeaponChangeSetup>();
    }

    public void PlayOpenWeaponChange()
    {
        animator.Play(openWeaponChange.name);
    }

    public void PlayCloseWeaponChange()
    {
        animator.Play(closeWeaponChange.name);
    }
}
