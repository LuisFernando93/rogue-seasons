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

    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] int damage = 1;
    int currentAmmo;
    bool readyToShot = true;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] private AudioClip attackSound;
    string weaponDamage, weaponMaxAmmo, weaponFireFreq, weaponRecharge;

    //Anima��es
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

    }

    private void Update()
    {
        //Verifica se o player Apertou o bot�o Esq ou Dir do mouse
        if (Input.GetButtonDown(combatManager.command[combatManager.commandIndex]))
        {
            Shot();
        }

    }
    private void FixedUpdate()
    {
        weaponRotationController.WeaponRotation();
    }

    //Realiza a anima��o de tiro e verifica a rela��o entre a muni��o
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

    //Realiza a anima��o de recarga e reseta a muni��o atual
    void Recharge()
    {
        readyToShot = false;
        combatManager.NotReadyToSwitchWeapon();
        currentAmmo = maxAmmo;
        animator.Play(WEAPON_RECHARGE.name);
    }

    //Chamada pela anima��o
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

    //Chamada pela anima��o para criar a instancia do projetil
    public void BulletShot()
    {
        if (attackSound != null)
        {
            SoundManager.Instance.PlaySFX(attackSound);
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);           
    }

    //Acesso a informa��es da arma
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
            Debug.Log("Informa��o de arma Ranged n�o encontrada. Possivel erro de solicitacao.");
            return null;
        }
    }
}
