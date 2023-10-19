using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Objetos
    [SerializeField] GameObject interactIcon;
    DialogueUI dialogueUI;
    NewWeaponChangeSetup weaponChangeSetup;
    private HealthManager healthManager;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator animator;
    NewCombatManager combatManager;
    SpriteRenderer spriteRenderer;

    //Variaveis
    [SerializeField] private float baseMoveSpeed = 5f;
    public float MoveSpeed = 0f;
    [SerializeField] private float baseLife = 30f;
    private float life = 1f, maxLife = 1f;
    //[SerializeField] private Sprite dashSprite;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public Vector2 movement;
    private Vector2 boxSize = new Vector2(0.1f, 1f);
    private bool dead = false;
    [SerializeField] private LayerMask solidLayer;

    List<float> lifeModifier = new List<float>(), speedModifier = new List<float>(), meleeModifier = new List<float>(), rangedModifier = new List<float>();
    List<float> atkSpeedModifier = new List<float>(), rechargeModifier = new List<float>(), bulletSizeModifier = new List<float>();

    //Variaveis Dash
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashingCooldown = 1f;
    private float dashingTime = .2f;

    //Variaveis after image
    private float lastImageXpos, lastImageYpos;
    public float distanceBetweenImages = 0.1f;

    private bool canTakeDamage = true;

    public float GetLife()
    {
        return life;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        combatManager = GetComponent<NewCombatManager>();
        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        weaponChangeSetup = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NewWeaponChangeSetup>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();

        maxLife = baseLife;
        life = baseLife;
        GetComponent<GadgetsManager>().UpdateStatus();
    }

    private void Update()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;

        //Atualiza o a posi��o do personagem
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Corrida
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MoveSpeed = 10f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            MoveSpeed = 5f;
        }

        //Ataque esquerdo
        if (Input.GetButtonDown("Fire1"))
        {
            if (combatManager.leftWeaponActive != true && combatManager.canSwitchWeapon)
            {
                //UpdateStatus();
                combatManager.WeaponConfiguration();
            }

        }

        //Ataque Direito
        if (Input.GetButtonDown("Fire2"))
        {
            if (combatManager.rightWeaponActive != true && combatManager.canSwitchWeapon)
            {
                //UpdateStatus();
                combatManager.WeaponConfiguration();
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(5);
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        //Interação
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }
        CheckDash();
        CheckLife();
    }

    private void FixedUpdate()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;
        if (isDashing) return;
        PlayerMovement();
    }

    private void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage"))
        {
            canTakeDamage = false;
        }
        else
        {
            canTakeDamage = true;
        }
    }

    //Inverte o sprite do personagem
    void Flip()
    {
        if (movement.x > 0 && !facingRight)
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0 && facingRight)
        {
            facingRight = !facingRight;
            spriteRenderer.flipX = true;
        }
    }

    void PlayerMovement()
    {
        rb.MovePosition(rb.position + movement.normalized * (MoveSpeed * Time.fixedDeltaTime)); //faz o player se mexer
        Flip();
        
    }

    private IEnumerator Dash()
    {
        canTakeDamage = false;
        canDash = false;
        isDashing = true;

        //GetComponent<SpriteRenderer>().sprite = dashSprite;


        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        float distanceToMove = dashPower * dashingTime;
        float distanceMoved = 0f;

        while (distanceMoved < distanceToMove)
        {
            float distanceThisFrame = Mathf.Min(dashPower * Time.fixedDeltaTime, distanceToMove - distanceMoved);

            // Lan�a um raio para detectar colis�es em dire��o ao movimento
            RaycastHit2D hit = Physics2D.Raycast(rb.position, dashDirection, distanceThisFrame, solidLayer);
            //Debug.DrawLine(transform.position, hit.point, Color.green, 500f);

            if (hit.collider != null)
            {
                // Detectou um obst�culo, interrompe o dash pr�ximo ao obst�culo

                rb.position = hit.point - dashDirection * coll.bounds.size.magnitude;
                break;
            }

            rb.position += dashDirection * distanceThisFrame;
            distanceMoved += distanceThisFrame;
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = rb.position.x;
            lastImageYpos = rb.position.y;

            yield return new WaitForFixedUpdate();

        }

        rb.velocity = Vector2.zero;
        isDashing = false;
        canTakeDamage = true;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;


    }

    private void CheckDash()
    {
        if(isDashing){
            if (dashingTime > 0)
            {         
                if (Mathf.Abs(rb.position.x - lastImageXpos) > distanceBetweenImages || Mathf.Abs(rb.position.y - lastImageYpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = rb.position.x;
                    lastImageYpos = rb.position.y;
                }
            }

        }
        
    }

    public void TakeDamage(int power)
    {
        if (canTakeDamage)
        {
            this.life -= power;
            this.canTakeDamage = false;
            //animator.SetTrigger("Damaged");

            GetComponent<PlayerAnimationManager>().DamageIndicator();
            healthManager.GetComponent<HealthManager>().UpdateHealth();;

            //Debug.Log("Vida Player: " + life);
        }
    }

    private void CheckLife()
    {
        if (life <= 0)
        {
            this.dead = true;
        }
    }

    public void OpenInteractableIcon()
    {
        interactIcon.SetActive(true);
    }

    public void CloseInteractableIcon()
    {
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.transform.GetComponent<Interactable>())
                {
                    rc.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }

    }

    public bool IsDead()
    {
        return this.dead;
    }

    public void SetLifeValue(float tempLife)
    {
        life += maxLife * tempLife;
        maxLife = baseLife + (baseLife * tempLife);
       
        if(healthManager != null)
        {
            healthManager.UpdateHealth();
        }
    }
    public void SetMoveSpeedValue(float tempSpeed)
    {
        MoveSpeed = baseMoveSpeed + (MoveSpeed * tempSpeed);
    }

    public float GetMaxLife()
    {
        return maxLife;
    }

}