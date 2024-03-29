using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] bool followPlayer;
    [SerializeField] int power;
    [SerializeField] float destroyAfter;
    [SerializeField] float followSpeed;
    Player player;
    Transform playerPosition;
    Vector3 direction, moveDirection;
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        player = hitInfo.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(power);
        }
    }

    private void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(this.gameObject, destroyAfter);

    }

    private void Update()
    {
        if (followPlayer)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        direction = playerPosition.position - transform.position;
        float distance = direction.magnitude;

        if (distance > 0.1f) // Evitar divis�o por zero
        {
            moveDirection = direction.normalized;
            float moveSpeed = followSpeed * Time.deltaTime;

            if (distance < moveSpeed)
            {
                transform.position = playerPosition.position;
            }
            else
            {
                transform.position += moveDirection * moveSpeed;
            }
        }
    }
}
