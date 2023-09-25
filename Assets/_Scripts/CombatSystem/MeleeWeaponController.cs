using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : Weapon
{
    //Objetos
    Player player;
    NewCombatManager combatManager;
    Animator animator;
    WeaponRotationController weaponRotation;
    EnemyController  enemy;

    DialogueUI dialogueUI;
    NewWeaponChangeSetup weaponChangeSetup;

    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] int quantCombo = 1;
    [SerializeField] int[] CDamage;
    int currentCombo = 0;
    bool isAttacking = false;
    bool nextAttack = true;
    string weaponDamage, weaponAtkSpeed;

    //Anima��es
    private string currentAnimation;
    [SerializeField] AnimationClip[] Ataques;
    [SerializeField] AnimationClip IDLE_ANIMATION;
    [SerializeField] AudioClip attackSound;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        animator = GetComponent<Animator>();
        combatManager = player.GetComponent<NewCombatManager>();
        weaponRotation = GetComponent<WeaponRotationController>();
        gameObject.name.Replace("(Clone)","");
        this.gameObject.tag = "meleeWeapon";

        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        weaponChangeSetup = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NewWeaponChangeSetup>();
    }

    private void Update()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;
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

            if (attackSound != null)
            {
                SoundManager.Instance.PlaySFX(attackSound);
            }

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
    private void SetWeaponInfos()
        {
        //dano do combo
        string allDamage = "" + CDamage[0].ToString();
            for (int i = 1; i < CDamage.Length; i++)
            {
                allDamage += " + " + CDamage[i].ToString();
            }    
        weaponDamage = allDamage;
        //atk speed
        float veloMedia = 0;
        for (int i = 0; i < Ataques.Length; i++)
        {
            veloMedia += Ataques[i].length;
        }
        veloMedia = veloMedia / Ataques.Length;
        weaponAtkSpeed = veloMedia.ToString("F2");
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

    public override void GetWeaponHistory()
    {
        throw new System.NotImplementedException();
    }

    public override string GetWeaponInfos(string infoNeeded)
    {
        /*
        Damage, AtkSpeed
        */
        SetWeaponInfos();
        if (infoNeeded == "Name")
        {
            return this.name;
        }
        else if (infoNeeded == "Type")
        {
            return "melee"; 
        }
        else if (infoNeeded == "Damage")
        {
            return weaponDamage; 
        }
        else if (infoNeeded == "AtkSpeed")
        {
            return weaponAtkSpeed;
        }
        else
        {
            Debug.Log("Informação de arma Melee não encontrada. Possivel erro de solicitacao.");
            return null;
        }
    }

   
}
