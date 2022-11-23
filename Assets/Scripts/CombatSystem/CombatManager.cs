using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Objetos
    [SerializeField] Player player;
    [SerializeField] Canvas canvas;
    Transform leftWeaponController;
    Transform rightWeaponController;
    Transform activeWeapon;
    MeleeWeaponController meleeCheck;
    RangedWeaponController rangedCheck;
    SpriteRenderer activeWeaponSR;
    Sprite leftWeaponIcon, rightWeaponIcon;

    //Variaveis
    [HideInInspector] public string[] command = new string[] { "Fire1", "Fire2" };
    [HideInInspector] public int commandIndex = 0;
    [HideInInspector] public int activeWeaponDamage;
    [HideInInspector] public bool leftWeaponActive = true;
    [HideInInspector] public bool rightWeaponActive = false;
    [HideInInspector] public bool canSwitchWeapon = true;
    string RightWeaponType, LeftWeaponType;
    int LeftWeaponDamage = 0, RightWeaponDamage = 0;

    private void Start()
    {
        WeaponSelect();
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

    

    //Define as armas usando a posição dos child dentro do player, é chamado quando uma arma é trocada para atualizar as referencias 
    public void WeaponSelect()
    {
        leftWeaponController = player.gameObject.transform.GetChild(0);
        rightWeaponController = player.gameObject.transform.GetChild(1);
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.GetChild(1).gameObject.SetActive(false);
        activeWeapon = leftWeaponController;
        canvas.GetComponent<GetWeaponIcon>().UpdateIcons();
    }
    //Diz o tipo da arma
    public string GetWeaponType(int i)
    {
        if (player.transform.GetChild(i).TryGetComponent(out MeleeWeaponController melee) == true)
        {
            if(i == 1)
            {
                RightWeaponType = "melee";
            }
            else if(i == 0)
            {
                LeftWeaponType = "melee";
            }
            meleeCheck = player.transform.GetChild(i).GetComponent<MeleeWeaponController>();
        }
        else if (player.transform.GetChild(i).TryGetComponent(out RangedWeaponController ranged) == true)
        {
            if (i == 1)
            {
                RightWeaponType = "ranged";
            }
            else if (i == 0)
            {
                LeftWeaponType = "ranged";
            }
            rangedCheck = player.transform.GetChild(i).GetComponent<RangedWeaponController>();
        }
        /*else if ((player.transform.GetChild(i).TryGetComponent(out MagicWeaponController magic) == true)
        {
            if(i == 1)
            {
                RightWeaponType = "magic";
            }
            else if(i == 0)
            {
                LeftWeaponType = "magic";
            }
        }*/
        if (i == 1)
        {
            return RightWeaponType;
        }
        else if (i == 0)
        {
            return LeftWeaponType;
        }
        else
        {
            return null;
        }
    }

    public Sprite GetWeaponIcon(int i)
    {
        if (i == 0)
        {
            if (GetWeaponType(0) == "melee")
            {
                leftWeaponIcon = meleeCheck.GetIcon();            
            }
            else if (GetWeaponType(0) == "ranged")
            {
                leftWeaponIcon = rangedCheck.GetIcon();
            }
            return leftWeaponIcon;
        }
        else if (i == 1)
        {
            if (GetWeaponType(1) == "melee")
            {
                rightWeaponIcon = meleeCheck.GetIcon();
            }
            else if (GetWeaponType(1) == "ranged")
            {
                rightWeaponIcon = rangedCheck.GetIcon();
            }
            return rightWeaponIcon;
        }
        else return null;
    }

    //Verifica o tipo da arma, assim pegando o dano que foi inserido nela
    void GetWeaponDamage()
    {
        if (leftWeaponActive == true)
        {
            if (GetWeaponType(0) == "ranged")
            {
                rangedCheck = player.transform.GetChild(0).GetComponent<RangedWeaponController>();
                LeftWeaponDamage = rangedCheck.GetDamage();
            }
            else if (GetWeaponType(0) == "melee")
            {
                meleeCheck = player.transform.GetChild(0).GetComponent<MeleeWeaponController>();
                LeftWeaponDamage = meleeCheck.GetDamage();
            }
        }
        if (rightWeaponActive == true)
        {
            if (GetWeaponType(1) == "ranged")
            {
                rangedCheck = player.transform.GetChild(1).GetComponent<RangedWeaponController>();
                RightWeaponDamage = rangedCheck.GetDamage();
            }
            else if (GetWeaponType(1) == "melee")
            {
                meleeCheck = player.transform.GetChild(1).GetComponent<MeleeWeaponController>();
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

    public int GetLeftWeaponDamage()
    {
        if (GetWeaponType(0) == "ranged")
        {
            rangedCheck = player.transform.GetChild(0).GetComponent<RangedWeaponController>();
            LeftWeaponDamage = rangedCheck.GetDamage();
        }
        else if (GetWeaponType(0) == "melee")
        {
            meleeCheck = player.transform.GetChild(0).GetComponent<MeleeWeaponController>();
            LeftWeaponDamage = meleeCheck.GetDamage();
        }
        /*else if (GetWeaponType(0) == "magic")
        {
            LeftWeaponDamage = magicCheck.GetDamage();
        }*/
        return LeftWeaponDamage;

    }
    public int GetRightWeaponDamage()
    {
        if (GetWeaponType(1) == "ranged")
        {
            rangedCheck = player.transform.GetChild(1).GetComponent<RangedWeaponController>();
            RightWeaponDamage = rangedCheck.GetDamage();
        }
        else if (GetWeaponType(1) == "melee")
        {
            meleeCheck = player.transform.GetChild(1).GetComponent<MeleeWeaponController>();
            RightWeaponDamage = meleeCheck.GetDamage();
        }
        /*else if (GetWeaponType(1) == "magic")
        {
            RightWeaponDamage = magicCheck.GetDamage();
        }*/
        return RightWeaponDamage;

    }

    public string GetMeleeWeaponDamage(int i)
    {
        meleeCheck = player.transform.GetChild(i).GetComponent<MeleeWeaponController>();
        return meleeCheck.GetDamageinText();
    }

    public string GetLeftWeaponName()
    {
        return leftWeaponController.name.Replace("(Clone)", "");
    }
    public string GetRightWeaponName()
    {
        return rightWeaponController.name.Replace("(Clone)", "");
    }
}
