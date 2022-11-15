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
    [SerializeField] private int maxAmmo = 1;
    [SerializeField] int damage = 1;
    int currentAmmo;
    bool readyToShot = true;
    [SerializeField] float bulletForce = 20f;

    //Animações
    string WEAPON_SHOT;
    string WEAPON_RECHARGE;
    string WEAPON_IDLE;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        weaponRotationController = GetComponent<WeaponRotationController>();
        combatManager = player.GetComponent<CombatManager>();
        animator = GetComponent<Animator>();

        WEAPON_SHOT = gameObject.name + "Shot";
        WEAPON_RECHARGE = gameObject.name + "Recharge";
        WEAPON_IDLE = gameObject.name + "Idle";
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
    void Shot()
    {
        if (currentAmmo > 0 && readyToShot)
        {
            combatManager.NotReadyToSwitchWeapon();
            readyToShot = false;
            animator.Play(WEAPON_SHOT);
            currentAmmo--;  
        }
        else if(currentAmmo == 0 && readyToShot)
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
        animator.Play(WEAPON_RECHARGE);
    }

    //Chamada pela animação
    void ReturnToIdle()
    {
        combatManager.ReadyToSwitchWeapon();
        readyToShot = true;
        animator.Play(WEAPON_IDLE);
    }

    //Chamada pela animação para criar a instancia do projetil
    public void BulletShot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb =  bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

    //Acesso ao dano da arma
    public int GetDamage()
    {
        return damage;
    }
}
