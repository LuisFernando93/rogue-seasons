using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : Weapon
{
    //Objetos
    Player player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    WeaponRotationController weaponRotationController;
    NewCombatManager combatManager;
    Animator animator;

    DialogueUI dialogueUI;
    NewWeaponChangeSetup weaponChangeSetup;


    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] int damage = 1;
    int currentAmmo;
    bool readyToShot = true;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] private AudioClip attackSound;
    string weaponDamage, weaponMaxAmmo, weaponFireFreq, weaponRecharge;
    float rechargeSpeed, bulletSizeIncrease;
    Vector3 bulletBaseScale;

    //Animações
    [SerializeField] AnimationClip WEAPON_SHOT;
    [SerializeField] AnimationClip WEAPON_RECHARGE;
    [SerializeField] AnimationClip WEAPON_IDLE;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        weaponRotationController = GetComponent<WeaponRotationController>();
        combatManager = player.GetComponent<NewCombatManager>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
        this.gameObject.tag = "rangedWeapon";

        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        weaponChangeSetup = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NewWeaponChangeSetup>();
        bulletBaseScale = bulletPrefab.transform.localScale;

    }

    private void OnEnable()
    {
        ChangeRechargeTime();
    }

    private void Update()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;
        //Verifica se o player Apertou o botão Esq ou Dir do mouse
        if (Input.GetButtonDown(combatManager.command[combatManager.commandIndex]))
        {
            Shot();
        }

    }
    private void FixedUpdate()
    {
        weaponRotationController.WeaponRotation();
    }

    //Realiza a animação de tiro e verifica a relação entre a munição
    private void Shot()
    {
        if (currentAmmo > 0 && readyToShot)
        {
            combatManager.NotReadyToSwitchWeapon();
            readyToShot = false;
            animator.Play(WEAPON_SHOT.name);
            currentAmmo--;
        }
        else if(currentAmmo <= 0 && readyToShot)
        {
            Recharge();
        }
    }

    //Realiza a animação de recarga e reseta a munição atual
    void Recharge()
    {
        readyToShot = false;
        combatManager.NotReadyToSwitchWeapon();
        currentAmmo = maxAmmo;
        animator.Play(WEAPON_RECHARGE.name);
    }

    //Chamada pela animação
    void ReturnToIdle()
    {
        readyToShot = true;
        combatManager.ReadyToSwitchWeapon();
        animator.Play(WEAPON_IDLE.name);
    }
    void ReadyToShot()
    {
        if (readyToShot == false)
        {
            readyToShot = true;
        }
    }

    //Chamada pela animação para criar a instancia do projetil
    public void BulletShot()
    {
        if (attackSound != null)
        {
            SoundManager.Instance.PlaySFX(attackSound);
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        ChangeBulletSize(bullet.transform);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);           
    }

    //Acesso a informações da arma
    public int GetDamage()
    {
        return damage;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    private void SetWeaponInfos()
    {
        weaponDamage = GetDamage().ToString();
        weaponMaxAmmo = maxAmmo.ToString();
        weaponFireFreq = WEAPON_SHOT.length.ToString("F2") + "s";
        weaponRecharge = WEAPON_RECHARGE.length.ToString("F2") + "s";
    }
    public override void GetWeaponHistory()
    {
        throw new System.NotImplementedException();
    }

    public void ModifyRechargeTime(float speedIncrease)
    {
        if(speedIncrease <= 1.5f)
        {
            rechargeSpeed = speedIncrease;
        }
        else
        {
            rechargeSpeed = 1.5f;
        }
        ChangeRechargeTime();
    }

    private void ChangeRechargeTime()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("RechargeSpeed", (1+rechargeSpeed));
    }

   public void ModifyBulletSize(float sizeIncrease)
    {
        bulletSizeIncrease = sizeIncrease;
    }

    private void ChangeBulletSize(Transform bullet)
    {
        //Debug.Log(bulletPrefab.transform.localScale.x + " * " + (1.0f + bulletSizeIncrease));
        Vector3 newScale = new Vector3(bulletBaseScale.x * (1.0f + bulletSizeIncrease), bulletBaseScale.y * (1.0f + bulletSizeIncrease), bulletBaseScale.z * (1.0f + bulletSizeIncrease));
        bullet.localScale = newScale;
    }

    public override string GetWeaponInfos(string infoNeeded)
    {
        SetWeaponInfos();
        if(infoNeeded == "Damage")
        {
            return weaponDamage;
        }
        if (infoNeeded == "Name")
        {
            return this.name;
        }
        else if (infoNeeded == "Type")
        {
            return "ranged";
        }
        else if (infoNeeded == "MaxAmmo")
        {
            return weaponMaxAmmo;
        }
        else if (infoNeeded == "FireFreq")
        {
            return weaponFireFreq;
        }
        else if (infoNeeded == "Recharge")
        {
            return weaponRecharge;
        }
        else
        {
            Debug.Log("Informação de arma Ranged não encontrada. Possivel erro de solicitacao.");
            return null;
        }
    }
}
