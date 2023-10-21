using UnityEngine;
using TMPro;

public abstract class Boss : FloatingDamage
{
    [SerializeField] public float recoverTime, baseArmor;
    [SerializeField] public int baseLife;
    [HideInInspector] public int power;

    [HideInInspector] public WeightedRandomList<string> Attacks;

    private bool canTakeDamage;
    protected int life;
    private string currentAnimation;
    protected Animator animator;

    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }

    public abstract void TakeDamage(int power);

}
