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

    [SerializeField] private int bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, destroyAfter);
        combatManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CombatManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.Play(HitEffect.name);
        if (collision.collider == player.GetComponent<Collider2D>())
        {
            Debug.Log("player atingido");
            //player.GetComponent<Player>().takeDamage(bulletDamage);
        }
    }
}
