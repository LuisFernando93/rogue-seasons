using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Objetos
    [SerializeField] float destroyAfter = 5f;
    CombatManager combatManager;
    Animator animator;
    [SerializeField]AnimationClip HitEffect;
    Enemy enemy;

    //Variaveis
    int bulletDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, destroyAfter);
        combatManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CombatManager>();
        bulletDamage = combatManager.GetActiveWeaponDamage();
    }

    //Verifica as informa��es do alvo usando uma collis�o de trigger
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy = hitInfo.GetComponent<Enemy>();  
    }

    //Executa a anima��o de contato da bala e faz alguma fun��o dependendo do que foi acertado
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play(HitEffect.name);
        if (enemy != null)
        {
            enemy.EnemyTakeDamage(bulletDamage);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
