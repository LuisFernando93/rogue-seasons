using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCombatManager : MonoBehaviour
{
    //Objetos
    Player player;
    Canvas canvas;
    Transform leftWeaponController;
    Transform rightWeaponController;
    Transform activeWeapon;
    MeleeWeaponController meleeCheck;
    RangedWeaponController rangedCheck;
    SpriteRenderer activeWeaponSR;
    Sprite leftWeaponIcon, rightWeaponIcon;

    DialogueUI dialogueUI;
    NewWeaponChangeSetup weaponChangeSetup;

    //Variaveis
    [HideInInspector] public string[] command = new string[] { "Fire1", "Fire2" };
    [HideInInspector] public int commandIndex = 0;
    [HideInInspector] public int activeWeaponDamage;
    [HideInInspector] public bool leftWeaponActive = true;
    [HideInInspector] public bool rightWeaponActive = false;
    [HideInInspector] public bool canSwitchWeapon = true;
    [HideInInspector] public string[] RightWeaponInfo, LeftWeaponInfo;
    [HideInInspector] public string RightWeaponType, LeftWeaponType;
    float rangedModifier;
    int LeftWeaponDamage = 0, RightWeaponDamage = 0;
    string LweaponName, LweaponDamage, LmeleeWeaponAtkSpeed, LrangedWeaponAmmo, LrangedWeaponFireFreq, LrangedWeaponRecharge;
    string RweaponName, RweaponDamage, RmeleeWeaponAtkSpeed, RrangedWeaponAmmo, RrangedWeaponFireFreq, RrangedWeaponRecharge;

    private void Start()
    {
        player = GetComponent<Player>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        WeaponType();
        GetWeaponDamage();
        WeaponSelect();
        //Detecta o dano da arma ativa no começo do jogo   

        dialogueUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogueUI>();
        weaponChangeSetup = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NewWeaponChangeSetup>();

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
        SetWeaponsInfo();
    }

    //Muda o botão de ataque no manager
    void ChangeCommand(int value)
    {
        commandIndex = value;
    }

    // Ativa e desetiva as armas de acordo com o comando pressionado (chamada pela classe Player)
    void RightWeaponActivate()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;

        leftWeaponController.gameObject.SetActive(false);
        leftWeaponActive = false;
        rightWeaponController.gameObject.SetActive(true);
        rightWeaponActive = true;
        activeWeapon = rightWeaponController;
        ChangeCommand(1);
    }
    void LeftWeaponActivate()
    {
        if (dialogueUI.IsOpen) return;
        if (weaponChangeSetup.IsOpen) return;

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
        WeaponType();
    }


    //Verifica o tipo da arma, assim pegando o dano que foi inserido nela
    void GetWeaponDamage()
    {
        if (leftWeaponActive == true)
        {
            if (LeftWeaponType == "ranged")
            {
                rangedCheck = player.transform.GetChild(0).GetComponent<RangedWeaponController>();
                LeftWeaponDamage = rangedCheck.GetDamage();
            }
            else if (LeftWeaponType == "melee")
            {
                meleeCheck = player.transform.GetChild(0).GetComponent<MeleeWeaponController>();
                LeftWeaponDamage = meleeCheck.GetDamage();
            }
        }
        if (rightWeaponActive == true)
        {
            if (RightWeaponType == "ranged")
            {
                rangedCheck = player.transform.GetChild(1).GetComponent<RangedWeaponController>();
                RightWeaponDamage = rangedCheck.GetDamage();
            }
            else if (RightWeaponType == "melee")
            {
                meleeCheck = player.transform.GetChild(1).GetComponent<MeleeWeaponController>();
                RightWeaponDamage = meleeCheck.GetDamage();
            }
        }
    }

    public string GetLeftWeaponName()
    {
        return leftWeaponController.name.Replace("(Clone)", "");
    }
    public string GetRightWeaponName()
    {
        return rightWeaponController.name.Replace("(Clone)", "");
    }

    void WeaponType()
    {
        if (player.transform.GetChild(0).TryGetComponent(out MeleeWeaponController meleeLeft) == true)
        {
            LeftWeaponType = "melee";
        }
        else if (player.transform.GetChild(0).TryGetComponent(out RangedWeaponController rangedLeft) == true)
        {
            LeftWeaponType = "ranged";
        }
        if (player.transform.GetChild(1).TryGetComponent(out MeleeWeaponController meleeRight) == true)
        {
            RightWeaponType = "melee";
        }
        else if (player.transform.GetChild(1).TryGetComponent(out RangedWeaponController rangedRight) == true)
        {
            RightWeaponType = "ranged";
        }
    }

    public Sprite GetWeaponIcon(int i)
    {
        WeaponType();
        if (i == 0)
        {
            if (LeftWeaponType == "melee")
            {
                leftWeaponIcon = player.transform.GetChild(0).GetComponent<MeleeWeaponController>().GetIcon();
            }
            else if (LeftWeaponType == "ranged")
            {
                leftWeaponIcon = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetIcon();
            }
            return leftWeaponIcon;
        }
        else if (i == 1)
        {
            if (RightWeaponType == "melee")
            {
                rightWeaponIcon = player.transform.GetChild(1).GetComponent<MeleeWeaponController>().GetIcon();
            }
            else if (RightWeaponType == "ranged")
            {
                rightWeaponIcon = player.transform.GetChild(1).GetComponent<RangedWeaponController>().GetIcon();
            }
            return rightWeaponIcon;
        }
        else
        {
            Debug.Log("Erro em retornar Icon");
            return null;
        }
    }

    private void SetWeaponsInfo()
    {
        if (LeftWeaponType == "melee")
        {
            //LweaponName = player.transform.GetChild(0).GetComponent<MeleeWeaponController>().GetWeaponInfos("Name");
            LweaponDamage = player.transform.GetChild(0).GetComponent<MeleeWeaponController>().GetWeaponInfos("Damage");
            LmeleeWeaponAtkSpeed = player.transform.GetChild(0).GetComponent<MeleeWeaponController>().GetWeaponInfos("AtkSpeed");
        }
        else if (LeftWeaponType == "ranged")
        {
            //LweaponName = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("Name");
            LweaponDamage = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("Damage");
            LrangedWeaponAmmo = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("MaxAmmo");
            LrangedWeaponFireFreq = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("FireFreq");
            LrangedWeaponRecharge = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("Recharge");
        }
        if (RightWeaponType == "melee")
        {
            //RweaponName = player.transform.GetChild(0).GetComponent<MeleeWeaponController>().GetWeaponInfos("Name");
            RweaponDamage = player.transform.GetChild(1).GetComponent<MeleeWeaponController>().GetWeaponInfos("Damage");
            RmeleeWeaponAtkSpeed = player.transform.GetChild(1).GetComponent<MeleeWeaponController>().GetWeaponInfos("AtkSpeed");
        }
        else if (RightWeaponType == "ranged")
        {
            //RweaponName = player.transform.GetChild(0).GetComponent<RangedWeaponController>().GetWeaponInfos("Name");
            RweaponDamage = player.transform.GetChild(1).GetComponent<RangedWeaponController>().GetWeaponInfos("Damage");
            RrangedWeaponAmmo = player.transform.GetChild(1).GetComponent<RangedWeaponController>().GetWeaponInfos("MaxAmmo");
            RrangedWeaponFireFreq = player.transform.GetChild(1).GetComponent<RangedWeaponController>().GetWeaponInfos("FireFreq");
            RrangedWeaponRecharge = player.transform.GetChild(1).GetComponent<RangedWeaponController>().GetWeaponInfos("Recharge");
        }
        LweaponName = GetLeftWeaponName();
        RweaponName = GetRightWeaponName();
    }

    public string GetLeftWeaponInfo(string infoNeeded)
    {
        SetWeaponsInfo();
        if (infoNeeded == "Name")
        {
            return LweaponName;
        }
        else if (infoNeeded == "Type")
        {
            return LeftWeaponType;
        }
        else if (infoNeeded == "Damage")
        {
            return LweaponDamage;
        }
        else if (infoNeeded == "AtkSpeed")
        {
            return LmeleeWeaponAtkSpeed;
        }

        if (infoNeeded == "Damage")
        {
            return LweaponDamage;
        }
        else if (infoNeeded == "MaxAmmo")
        {
            return LrangedWeaponAmmo;
        }
        else if (infoNeeded == "FireFreq")
        {
            return LrangedWeaponFireFreq;
        }
        else if (infoNeeded == "Recharge")
        {
            return LrangedWeaponRecharge;
        }
        else
        {
            Debug.Log("Informação não encontrada. Input: " + infoNeeded);
            return "nothing";
        }
    }
    public string GetRightWeaponInfo(string infoNeeded)
    {
        SetWeaponsInfo();
        if (infoNeeded == "Name")
        {
            return RweaponName;
        }
        else if (infoNeeded == "Type")
        {
            return RightWeaponType;
        }

        else if (infoNeeded == "AtkSpeed")
        {
            return RmeleeWeaponAtkSpeed;
        }

        else if (infoNeeded == "Damage")
        {
            return RweaponDamage;
        }
        else if (infoNeeded == "MaxAmmo")
        {
            return RrangedWeaponAmmo;
        }
        else if (infoNeeded == "FireFreq")
        {
            return RrangedWeaponFireFreq;
        }
        else if (infoNeeded == "Recharge")
        {
            return RrangedWeaponRecharge;
        }

        else
        {
            Debug.Log("Informação não encontrada. Input: " + infoNeeded);
            return "nothing";
        }
    }
    //Determina o dano da arma atual, é usado por outra classes
    public int GetActiveWeaponDamage()
    {
        if (leftWeaponActive == true)
        {
            //Debug.Log("Modificador a ser somado: " + Mathf.RoundToInt(LeftWeaponDamage * rangedModifier) + " dano da arma: " + LeftWeaponDamage + " modificador: " + rangedModifier);

            activeWeaponDamage = LeftWeaponDamage + Mathf.RoundToInt(LeftWeaponDamage * rangedModifier);
        }
        if (rightWeaponActive == true)
        {
            activeWeaponDamage = RightWeaponDamage + Mathf.RoundToInt(RightWeaponDamage * rangedModifier);
        }
        return activeWeaponDamage;
    }

    public void ModifyRangedDamage(float modifier)
    {
        rangedModifier = modifier;
    }
}
