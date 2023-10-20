using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : EnemyController
{
    
    [SerializeField] private int life = 5;
    [SerializeField] private int speed = 2;
    [SerializeField] public int power = 1;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private AudioClip damagedSound;

    SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool faceRight = true;
    private bool move = false;
    private bool isAttacking = false;
    private bool canTakeDamage = true;
    private float distance;
    private Vector3 direction;

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

    public void DamageIndicator()
    {
        damageFlashTimer = 0.3f;
    }

    void FixedUpdate()
    {
        if  (move)
        {
            MoveEnemy();
        }
    }

    private void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        } else
        {
            isAttacking = false;
        }
    }

    private void UpdateEnemy()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > attackDistance)
        {
            move = true;
            animator.SetBool("IsIdle", false);
        }
        else
        {
            move = false;
            animator.SetBool("IsIdle", true);
            if (!isAttacking)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    private void MoveEnemy()
    {

        if (player != null)
        {
            //flip sprite
            if (player.transform.position.x > transform.position.x && !faceRight)
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && faceRight)
            {
                Flip();
            }

            pathfindTimer -= Time.deltaTime;

            if (base.pathfindTimer <= 0)
            {
                base.FindPlayerPosition();
                base.pathfindTimer = base.pathUpdateTime;
            }

            
            if (pathVectorList != null)
            {
                Vector3 targetPosition = pathVectorList[pathIndex];
                //Debug.Log("Distance: " + Vector3.Distance(transform.position, targetPosition));
                if (Vector3.Distance(transform.position, targetPosition) > 0.32f)
                {
                    //Debug.Log("Andando. PathIndex: " + pathIndex);
                    direction = (targetPosition - transform.position).normalized;
                    transform.position += speed * Time.deltaTime * direction;
                } else
                {
                    pathIndex++;
                    if (pathIndex >= pathVectorList.Count)
                    {
                        pathVectorList = null;
                    }
                }
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

    public override void TakeDamage(int power)
    {
        if (canTakeDamage)
        {
            if (damagedSound != null)
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
        if(!canTakeDamage)
        {
            canTakeDamage = true;
        }
    }
}
