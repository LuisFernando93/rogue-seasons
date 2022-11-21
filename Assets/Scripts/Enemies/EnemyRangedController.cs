using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour
{
    
    [SerializeField] private GameObject RoomController;
    [SerializeField] private int speed = 1;
    [SerializeField] private int power = 1;
    [SerializeField] private float attackDistanceMin = 1.8f;
    [SerializeField] private float attackDistanceMax = 2;

    private GameObject Player;
    private Animator animator;
    private bool faceRight = true;
    private float distance;
    private bool move = false;
    private bool moveReverse = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private float attackCooldown = 1.5f;
    private float timeStampAtkCooldown;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    void FixedUpdate()
    {
        MoveEnemy();
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
        distance = Vector2.Distance(Player.transform.position, transform.position);

        if (distance > attackDistanceMax)
        {
            move = true;
            moveReverse = false;
        }
        else if (distance < attackDistanceMin)
        {
            move = false; 
            moveReverse = true;
            if (!isAttacking && canAttack)
            {
                animator.SetTrigger("Attack");
            }
        }
        else if (distance <= attackDistanceMax && distance >= attackDistanceMin)
        {
            move = false;
            moveReverse = false;
            if (!isAttacking && canAttack)
            {
                animator.SetTrigger("Attack");
                canAttack = false;
                timeStampAtkCooldown = Time.time + attackCooldown;
            }
        }

        if (!canAttack)
        {
            if (timeStampAtkCooldown <= Time.time)
            {
                canAttack = true;
            }
        }
    }

    private void MoveEnemy()
    {  
        if (Player != null) {
            

            //flip sprite
            if(Player.transform.position.x > transform.position.x && !faceRight){
                Flip();
            } else if (Player.transform.position.x < transform.position.x && faceRight) {
                Flip();
            }

            direction = new Vector2(Player.transform.position.x, Player.transform.position.y);
            if (move) {
                transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            } else if (moveReverse) {
                transform.position = Vector2.MoveTowards(transform.position, direction, -1 * speed * Time.deltaTime);
            }
        }

    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        faceRight = !faceRight;
    }
}
