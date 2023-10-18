using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    Boss boss;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(boss.power);
            //Debug.Log("dano: " + boss.power);
        }
    }
}
