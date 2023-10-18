using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] bool isBullet, isExplosion, isFollower;
    [SerializeField] int power;
    [SerializeField] int destroyAfter;
    Player player;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            if(isBullet)
            {
                player.TakeDamage(power);
            }         
        }
        Destroy(gameObject, destroyAfter);
    }
}
