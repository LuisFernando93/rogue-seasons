using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    EnemyMeleeController meleeEnemy;

    private void Start()
    {
        meleeEnemy = GetComponentInParent<EnemyMeleeController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(meleeEnemy.power);
            //Debug.Log("dano: " + meleeEnemy.power);
        }
    }
}
