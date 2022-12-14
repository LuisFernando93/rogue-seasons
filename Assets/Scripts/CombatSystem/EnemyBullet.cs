using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Objetos
    [SerializeField] private float destroyAfter = 5f;
    private CombatManager combatManager;
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
        combatManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CombatManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        animator.Play(HitEffect.name);
        if (collision.collider == player.GetComponent<Collider2D>())
        {
            player.GetComponent<Player>().TakeDamage(bulletDamage);
        }
    }



    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
