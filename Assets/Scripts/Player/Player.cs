using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Objetos
    [SerializeField] GameObject interactIcon;
    [SerializeField] private GameObject healthManager;
    private Rigidbody2D rb;
    private Animator animator;
    CombatManager combatManager;
    SpriteRenderer spriteRenderer;

    //Variaveis
    [SerializeField] private float MoveSpeed = 5f;
    [SerializeField] private int life = 4;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public Vector2 movement;
    private Vector2 boxSize = new Vector2(0.1f, 1f);

    //Variaveis Dash
    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashPower = 5f;
    [SerializeField] private float dashingCooldown = 1f;
    private float dashingTime = 0.2f;

    //Variaveis after image
    private float lastImageXpos, lastImageYpos;
    public float distanceBetweenImages = 0.1f;

    private bool canTakeDamage = true;

    public int GetLife()
    {
        return life;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        combatManager = GetComponent<CombatManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Atualiza o a posição do personagem
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
                combatManager.WeaponConfiguration();
            }

        }

        //Ataque Direito
        if (Input.GetButtonDown("Fire2"))
        {
            if (combatManager.rightWeaponActive != true && combatManager.canSwitchWeapon)
            {
                combatManager.WeaponConfiguration();
            }
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            
        }
        CheckDash();
    }

    private void FixedUpdate()
    {
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
        rb.velocity = new Vector2(movement.x * dashPower, movement.y * dashPower);

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
        lastImageYpos = transform.position.y;

        yield return new WaitForSeconds(dashingTime);
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
                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages || Mathf.Abs(transform.position.y - lastImageYpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                    lastImageYpos = transform.position.y;
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
            animator.SetTrigger("Damaged");
            Debug.Log(life);
            healthManager.GetComponent<HealthManager>().UpdateHealth();
        }
    }

    private void CheckLife()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
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
}