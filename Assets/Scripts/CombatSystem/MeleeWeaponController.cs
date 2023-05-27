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
    EnemyController  enemy;

    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] int quantCombo = 1;
    [SerializeField] int[] CDamage;
    int currentCombo = 0;
    bool isAttacking = false;
    bool nextAttack = true;

    //Anima��es
    private string currentAnimation;
    [SerializeField] AnimationClip[] Ataques;
    [SerializeField] AnimationClip IDLE_ANIMATION;
    [SerializeField] AudioClip attackSound;

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
            if (attackSound != null)
            {
                SoundManager.Instance.PlaySFX(attackSound);
            }
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
    public string GetDamageinText()
    {
        string allDamage = "" + CDamage[0].ToString();
        for (int i = 1; i < CDamage.Length; i++)
        {
            allDamage += " + " + CDamage[i].ToString();
        }
        return allDamage;
    }

    public string GetAttackSpeed()
    {
        float veloMedia = 0;
        for (int i = 0; i < Ataques.Length; i++)
        {
            veloMedia += Ataques[i].length;
        }
        veloMedia = veloMedia / Ataques.Length;

        return veloMedia.ToString("F2");
    }


    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        enemy = hitInfo.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(CDamage[currentCombo - 1]);
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
