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

    //Anima��es
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
    void Shot()
    {
        if (currentAmmo > 0 && readyToShot)
        {
            combatManager.NotReadyToSwitchWeapon();
            readyToShot = false;
            animator.Play(WEAPON_SHOT.name);
            currentAmmo--;  
        }
        else if(currentAmmo == 0 && readyToShot)
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
        combatManager.ReadyToSwitchWeapon();
        readyToShot = true;
        animator.Play(WEAPON_IDLE.name);
    }

    //Chamada pela anima��o para criar a instancia do projetil
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

    public Sprite GetIcon()
    {
        return icon;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
