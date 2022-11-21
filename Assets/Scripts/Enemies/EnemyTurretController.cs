using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretController : MonoBehaviour
{

    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject RoomController;
    [SerializeField] private int power = 1;
    [SerializeField] private float attackDistance = 2f;

    private GameObject player;
    private Animator animator;
    private bool faceRight = true;
    private float distance;
    private bool canAttack = true;
    private bool isAttacking = false;
    private bool sleep = false;
    private float attackCooldown = 2f;
    private float timeStampAtkCooldown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (player.transform.position.x > transform.position.x && !faceRight)
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && faceRight)
            {
                Flip();
            }
        }
    }
    private void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

    }

    private void UpdateEnemy()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < attackDistance && canAttack && !isAttacking)
        {
            animator.SetTrigger("Attack");
            canAttack = false;
            
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Sleep") && !sleep)
        {
            sleep = true;
            timeStampAtkCooldown = Time.time + attackCooldown;
        }

        if (timeStampAtkCooldown <= Time.time && sleep)
        {
            animator.SetTrigger("Awake");
            sleep = false;
        }

        if (!canAttack && animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            canAttack = true;
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        faceRight = !faceRight;
    }

    private void Attack()
    {
        GameObject explosion = Instantiate(attackPrefab, firePoint.transform.position, Quaternion.identity);
        explosion.GetComponent<EnemyExplosion>().SetExplosionDamage(power);
    }

}
