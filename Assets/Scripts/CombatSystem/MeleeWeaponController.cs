using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    //Objetos
    Player player;
    CombatManager combatManager;
    Animator animator;
    WeaponRotationController weaponRotation;
    Enemy enemy;

    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] int quantCombo = 1;
    [SerializeField] int[] CDamage;
    int currentCombo = 0;
    bool isAttacking = false;
    bool nextAttack = true;

    //Animações
    private string currentAnimation;
    [SerializeField] AnimationClip[] Ataques;
    [SerializeField] AnimationClip IDLE_ANIMATION;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
        combatManager = player.GetComponent<CombatManager>();
        weaponRotation = GetComponent<WeaponRotationController>();
        gameObject.name.Replace("(Clone)","");
    }

    private void Update()
    {
        //Verifica se o player Apertou o bot?o Esq ou Dir do mouse
        if (Input.GetButtonDown(combatManager.command[combatManager.commandIndex]))
        {
            ComboStart();
        }

    }

    private void FixedUpdate()
    {
        weaponRotation.WeaponRotation();
    }

    //Inicia o sistema de combo, impossibilitando a troca de arma
    public void ComboStart()
    {

        if (isAttacking == false)
        {
            if (nextAttack)
            {
                combatManager.NotReadyToSwitchWeapon();
                isAttacking = true;
                ChangeAnimation(Ataques[currentCombo].name);
                currentCombo++;
                ComboCheck();
            }
        }

    }

    //Finaliza o sistema de combo e possibilita a troca de arma
    void ComboFinish()
    {
        ChangeAnimation(IDLE_ANIMATION.name);
        combatManager.ReadyToSwitchWeapon();
        isAttacking = false;
        nextAttack = true;
        currentCombo = 0;
    }

    //Verifica se o combo ja acabou ou n?o
    void ComboCheck()
    {
        if (currentCombo <= quantCombo)
        {
            nextAttack = true;
        }
        else
        {
            nextAttack = false;
            ComboFinish();
        }
    }

    //Muda a anima??o e n?o permite que ela "reinicie" caso tente rodar uma anima??o que ja est? em andamento
    void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }
    //Essa fun??o ? usada pela anima??o para dizer quando pode atacar novamente
    void ChangeIsAttackValue()
    {
        isAttacking = false;
    }

    public int GetDamage()
    {
        return CDamage[currentCombo];
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.EnemyTakeDamage(CDamage[currentCombo - 1]);
        }
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

}
