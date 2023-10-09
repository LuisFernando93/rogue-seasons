using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    private GameObject player;
    private int explosionDamage;

    public void SetExplosionDamage(int power)
    {
        explosionDamage = power;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().TakeDamage(explosionDamage);
        }
    }

    private void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
