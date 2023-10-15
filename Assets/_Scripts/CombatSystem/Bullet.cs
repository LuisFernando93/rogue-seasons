using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Objetos
    [SerializeField] float destroyAfter = 5f;
    NewCombatManager combatManager;
    Animator animator;
    [SerializeField]AnimationClip HitEffect;
    EnemyController enemy;


    //Variaveis
    private int bulletDamage;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, destroyAfter);
        combatManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<NewCombatManager>();
        bulletDamage = combatManager.GetActiveWeaponDamage();
    }

    /*
     Efeito de "rebote" funciona pois a bala só é destruida ao final do "HitEffect", caso isso seja desabilitado,
     é possivel criar um tiro perfurante.
     */

    //Verifica as informações do alvo usando uma collisão de trigger
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            GetComponent<BoxCollider2D>().isTrigger = false;
            animator.Play(HitEffect.name);
        }
    }

    //Executa a animação de contato da bala e faz alguma função dependendo do que foi acertado
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play(HitEffect.name);
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
        }
    }*/

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}