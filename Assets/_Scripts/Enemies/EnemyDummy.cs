using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : EnemyController
{
    [SerializeField] int life = 1;
    Animator animator;

    [SerializeField] AnimationClip IDLE;
    [SerializeField] AnimationClip DAMAGE;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public override void TakeDamage(int power)
    {
        life -= power;
        CreateFloatingDamage(power);
        if (life <= 0)
        {
            Die();
        }
        else
        {
            animator.Play(DAMAGE.name);
        }

    }

    void Die()
    {
        animator.Play(DAMAGE.name);
        Destroy(gameObject, 0.2f);
    }

    void ReturnToIdle()
    {
        animator.Play(IDLE.name);
    }
}
