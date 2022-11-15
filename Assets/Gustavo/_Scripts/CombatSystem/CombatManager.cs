using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Objetos
    [SerializeField] Player player;
    Transform leftWeaponController;
    Transform rightWeaponController;
    Transform activeWeapon;
    MeleeWeaponController meleeCheck;
    RangedWeaponController rangedCheck;
    SpriteRenderer activeWeaponSR;

    //Variaveis
    [HideInInspector] public string[] command = new string[] { "Fire1", "Fire2" };
    [HideInInspector] public int commandIndex = 0;
    [HideInInspector] public int activeWeaponDamage;
    [HideInInspector] public bool leftWeaponActive = true;
    [HideInInspector] public bool rightWeaponActive = false;
    [HideInInspector] public bool canSwitchWeapon = true;
    int LeftWeaponDamage = 0, RightWeaponDamage = 0;
    
    private void Start()
    {
        leftWeaponController = player.gameObject.transform.GetChild(0);
        rightWeaponController = player.gameObject.transform.GetChild(1);
        activeWeapon = leftWeaponController;
        GetWeaponDamage(); //Detecta o dano da arma ativa no começo do jogo
    }

    private void Update()
    {
        FlipActiveWeapon();
    }

    //Flipa a arma ativa
    private void FlipActiveWeapon()
    {
        if (leftWeaponActive == true)
        {
            activeWeaponSR = player.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else if (rightWeaponActive == true)
        {
            activeWeaponSR = player.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        if (activeWeapon.rotation.z > 0.90f || activeWeapon.rotation.z < -0.90f)
        {
            activeWeaponSR.flipY = true;
        }
        else
        {
            activeWeaponSR.flipY = false;
        }
    }

    //Função Chamada pelos weapons Controler para dizer se pode ou não trocar a arma
    public void ReadyToSwitchWeapon()
    {
        canSwitchWeapon = true;
    }
    //Função Chamada pelos weapons Controler para dizer se pode ou não trocar a arma
    public void NotReadyToSwitchWeapon()
    {
        canSwitchWeapon = false;
    }

    //Realiza a configuração das armas de acordo com o botão pressionado na classe Player
    public void WeaponConfiguration()
    {
        if (leftWeaponActive != true)
        {
            LeftWeaponActivate();
        }
        else if (rightWeaponActive != true)
        {
            RightWeaponActivate();
        }
        GetWeaponDamage();
    }

    //Muda o botão de ataque no manager
    void ChangeCommand(int value)
    {
        commandIndex = value;
    }

    // Ativa e desetiva as armas de acordo com o comando pressionado (chamada pela classe Player)
    void RightWeaponActivate()
    {
        leftWeaponController.gameObject.SetActive(false);
        leftWeaponActive = false;
        rightWeaponController.gameObject.SetActive(true);
        rightWeaponActive = true;
        activeWeapon = rightWeaponController;
        ChangeCommand(1);
    }
    void LeftWeaponActivate()
    {
        rightWeaponController.gameObject.SetActive(false);
        rightWeaponActive = false;
        leftWeaponController.gameObject.SetActive(true);
        leftWeaponActive = true;
        activeWeapon = leftWeaponController;
        ChangeCommand(0);
    }

    //Verifica o tipo da arma, assim pegando o dano que foi inserido nela
    void GetWeaponDamage()
    {
        if(leftWeaponActive == true)
        {
            meleeCheck = player.GetComponentInChildren<MeleeWeaponController>();
            if (meleeCheck == null)
            {
                rangedCheck = player.GetComponentInChildren<RangedWeaponController>();
                LeftWeaponDamage = rangedCheck.GetDamage();
            }
            else
            {
                LeftWeaponDamage = meleeCheck.GetDamage();
            }
        }
        if(rightWeaponActive == true)
        {
            meleeCheck = player.GetComponentInChildren<MeleeWeaponController>();
            if (meleeCheck == null)
            {
                rangedCheck = player.GetComponentInChildren<RangedWeaponController>();
                RightWeaponDamage = rangedCheck.GetDamage();
            }
            else
            {
                RightWeaponDamage = meleeCheck.GetDamage();
            }
        }
    }

    //Determina o dano da arma atual, é usado por outra classes
    public int GetActiveWeaponDamage()
    {
        if (leftWeaponActive == true)
        {
            activeWeaponDamage = LeftWeaponDamage;
        }
        if (rightWeaponActive == true)
        {
            activeWeaponDamage = RightWeaponDamage;
        }
        return activeWeaponDamage;
    }
}
