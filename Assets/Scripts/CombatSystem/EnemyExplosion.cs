using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosion : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    private GameObject player;
    private Collider2D collider;
    private int explosionDamage;

    public void SetExplosionDamage(int power)
    {
        explosionDamage = power;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collider = hitBox.GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(collider.IsTouching(player.GetComponent<Collider2D>()))
        {
            player.GetComponent<Player>().takeDamage(explosionDamage);
        }
    }

    private void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
