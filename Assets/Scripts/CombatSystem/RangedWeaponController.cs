using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponController : MonoBehaviour
{
    //Objetos
    Player player;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    WeaponRotationController weaponRotationController;
    CombatManager combatManager;
    Animator animator;

    //Variaveis
    [SerializeField] Sprite icon;
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] int damage = 1;
    int currentAmmo;
    bool readyToShot = true;
    [SerializeField] float bulletForce = 20f;

    //Animações
    [SerializeField] AnimationClip WEAPON_SHOT;
    [SerializeField] AnimationClip WEAPON_RECHARGE;
    [SerializeField] AnimationClip WEAPON_IDLE;

   

    private void Start()
    {
        player = GetComponentInParent<Player>();
        weaponRotationController = GetComponent<WeaponRotationController>();
        combatManager = player.GetComponent<CombatManager>();
        animator = GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);           
    }

    //Acesso a informações da arma
    public int GetDamage()
    {
        return damage;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }
    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetRechargeTime()
    {
        return WEAPON_RECHARGE.length.ToString("F2")+"s";
    }

    public string GetFireFreq()
    {
        return WEAPON_SHOT.length.ToString("F2")+"s";
    }
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

}
