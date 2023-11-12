using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController :  EnemyController
{

    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private int life = 5;
    [SerializeField] private int speed = 1;
    [SerializeField] private int power = 1;
    [SerializeField] private float bulletForce = 10f;
    [SerializeField] private float attackDistanceMin = 1.8f;
    [SerializeField] private float attackDistanceMax = 2;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] private AudioClip damagedSound;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool faceRight = true;
    private float distance;
    private bool move = false;
    private bool moveReverse = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool canTakeDamage = true;
    private float attackCooldown = 1.5f;
    private float timeStampAtkCooldown;

    private Vector2 directionToPlayer;

    private float damageFlashTimer = 0f;


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if (damageFlashTimer > 0f)
        {
            // Atualize a cor para vermelho
            spriteRenderer.color = Color.red;

            // Reduza o temporizador
            damageFlashTimer -= Time.deltaTime;

            // Verifique se o temporizador expirou
            if (damageFlashTimer <= 0f)
            {
                // Retorna à cor original
                spriteRenderer.color = Color.white;
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
        distance = Vector2.Distance(player.transform.position, firePoint.transform.position);
        RaycastHit2D hit = Physics2D.Raycast(firePoint.transform.position, (player.transform.position - firePoint.transform.position).normalized, distance, obstacles);

        if (distance > attackDistanceMax)
        {
            move = true;
            moveReverse = false;
        }
        else if (distance < attackDistanceMin && hit.collider == null)
        {
            move = false;
            moveReverse = true;
            if (!isAttacking && canAttack)
            {
                animator.SetTrigger("Attack");
            }
        }
        else if (distance <= attackDistanceMax && distance >= attackDistanceMin && hit.collider == null)
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
        if (player != null) {
            

            //flip sprite
            if(player.transform.position.x > transform.position.x && !faceRight){
                Flip();
            } else if (player.transform.position.x < transform.position.x && faceRight) {
                Flip();
            }

            directionToPlayer = new Vector2(player.transform.position.x, player.transform.position.y);
            if (move) {
                pathfindTimer -= Time.deltaTime;

                if (base.pathfindTimer <= 0)
                {
                    base.FindPlayerPosition();
                    base.pathfindTimer = base.pathUpdateTime;
                }


                if (pathVectorList != null)
                {
                    Vector3 targetPosition = pathVectorList[pathIndex];
                    if (Vector3.Distance(transform.position, targetPosition) > 0.128f)
                    {
                        Vector3 direction = (targetPosition - transform.position).normalized;
                        transform.position += speed * Time.deltaTime * direction;
                    }
                    else
                    {
                        pathIndex++;
                        if (pathIndex >= pathVectorList.Count)
                        {
                            pathVectorList = null;
                        }
                    }
                } else
                {
                    Vector3 direction = (player.GetComponent<Transform>().position - transform.position).normalized;
                    transform.position += speed * Time.deltaTime * direction;
                }
            } else if (moveReverse) {
                pathfindTimer = 0;
                transform.position = Vector2.MoveTowards(transform.position, directionToPlayer, -1 * speed * Time.deltaTime);
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

    private void Attack()
    {
        firePoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, player.transform.position - firePoint.transform.position) * Quaternion.Euler(0, 0, 90);
        GameObject bullet = Instantiate(enemyBulletPrefab,firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.transform.right * bulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<EnemyBullet>().SetBulletDamage(power);
    }

    public override void TakeDamage(int power)
    {
        if (canTakeDamage)
        {
            if(damagedSound != null)
            {
                SoundManager.Instance.PlaySFX(damagedSound);
            }
            this.life -= power;
            //this.canTakeDamage = false;
            //animator.SetTrigger("Damaged");
            CreateFloatingDamage(power);
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void EnableDamage()
    {
        if (!canTakeDamage)
        {
            canTakeDamage = true;
        }
    }

}
