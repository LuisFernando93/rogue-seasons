using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Objetos
    [SerializeField] private float destroyAfter = 5f;
    private NewCombatManager combatManager;
    private Animator animator;
    [SerializeField] private AnimationClip HitEffect;
    private GameObject player;
    private GameObject[] enemies;

    private int bulletDamage;

    public void SetBulletDamage(int power)
    {
        bulletDamage = power;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, destroyAfter);
        combatManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<NewCombatManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.Play(HitEffect.name);
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().TakeDamage(bulletDamage);
            GetComponent<BoxCollider2D>().isTrigger = false;
            if (HitEffect != null)
                animator.Play(HitEffect.name);
        }
    }



    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
