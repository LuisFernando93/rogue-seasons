using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummerBoss : Boss
{
    Transform player;

    [SerializeField]
    AnimationClip IDLE, ATTACK01, ATTACK02_ENTRY, ATTACK02_PREP, ATTACK02_DAMAGE, EVOKE_RAIN, CUT, EXAUSTION, RECOVER,
        TELEPORT, ULT, ULT_LOOP, DEATH, GROUND_EXP;
    [SerializeField] GameObject attackPoint, arrowPrefab, arrowRainPrefab, cutPrefab, groundExpPrefab, lifeBarPrefab, blackHoleEffect, teleport;
    [SerializeField] int arrowForce;
    bool halfLife = false, ultimateUsed = false, attacking = false;
    Slider slider; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Attacks.Add("CallAttack01", 1);
        Attacks.Add("CallAttack02", 1);
        Attacks.Add("CallAttack03", 0.2f);
        Attacks.Add("CallAttack04", 0.5f);
        Attacks.Add("CallAttack05", 0.2f);

        StartBattle();
        SelectNextAttack();
    }

    private void Update()
    {
        if(life <= 0)
        {
            ChangeAnimation(DEATH.name);
            return;
        }
    }

    private void StartBattle()
    {
        slider = Instantiate(lifeBarPrefab, GameObject.FindGameObjectWithTag("Canvas").transform).GetComponent<Slider>();
        slider.maxValue = baseLife;
        slider.value = baseLife;
        life = baseLife;
    }

    private void BackToIdle()
    {
        attacking = false;
        baseArmor = 0.5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        ChangeAnimation(IDLE.name);
    }

    public void SelectNextAttack()
    {
        if (life <= 0) return;
        ResetRotation();
        BackToIdle();
        if (life <= (baseLife * 0.5f) && !halfLife)
        {
            HardMode();
            Invoke("Ultimate", recoverTime);
            return;
        }
        string nextAttack = Attacks.GetRandom();
        Invoke("SetArmor", recoverTime);
        Invoke(nextAttack, recoverTime);
        
    }

    //Corte de fogo
    private void CallAttack01()
    {
        ChangePosition(player.position.x, player.position.y + 1f, 0f);
        power = 5;
        ChangeAnimation(ATTACK01.name);
    }

    //Flecha
    private void CallAttack02()
    {
        if (Random.Range(0, 2) == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ChangePosition(player.position.x + 3, player.position.y, 0f);
        }
        else
        {
            ChangePosition(player.position.x - 3, player.position.y, 0f);
        }
        power = 10;
        ChangeAnimation(ATTACK02_ENTRY.name);
    }

    //Chuva de flechas
    private void CallAttack03()
    {
        ChangeAnimation(EVOKE_RAIN.name);
        Instantiate(arrowRainPrefab, player.transform.position, player.transform.rotation);
    }

    //Corte
    private void CallAttack04()
    {
        ChangePosition(0, 0, 0);
        if (player.transform.position.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        ChangeAnimation(CUT.name);
    }

    //Explos�es
    private void CallAttack05()
    {
        ChangePosition(0, 0, 0);
        ChangeAnimation(GROUND_EXP.name);
    }

    //Ultimate
    private void Ultimate()
    {
        if (ultimateUsed)
        {
            SelectNextAttack();
            return;
        }
        baseArmor = 0.25f;
        ChangePosition(0, 0, 0);
        power = 1;
        ChangeAnimation(ULT.name);
        Instantiate(blackHoleEffect,this.transform);
        ultimateUsed = true;
    }

    //Cria as instrancias dos ataques
    public void ArrowShot()
    {
        GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPoint.transform.right * arrowForce, ForceMode2D.Impulse);
    }

    public void CreateCut()
    {
        Vector3 position = new Vector3(0, 0, 0);

        GameObject cut = Instantiate(cutPrefab, position, transform.rotation);
        if (player.transform.position.x < 0f)
        {
            cut.GetComponent<Animator>().Play("VerticalCutL");
        }
        else
        {
            cut.GetComponent<Animator>().Play("VerticalCut");
        }
    }

    public void CreateExplosion()
    {
        Vector3 playerPosition = player.position;
        float x = 3, y = 3;
        Vector2 minSpawnPosition = new Vector2(playerPosition.x - x, playerPosition.y - y), maxSpawnPosition = new Vector2(playerPosition.x + x, playerPosition.y + y);
        float randomX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
        float randomY = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
        GameObject explosion = Instantiate(groundExpPrefab, spawnPosition, Quaternion.identity);
    }

    //Posiciona o boss na arena
    private void ChangePosition(float x, float y, float z)
    {
        Vector3 attackPosition;
        attackPosition = new Vector3(x, y, z);
        transform.position = attackPosition;
        /*Debug.Log("Posi��o a ser chamada x:" + attackPosition.x +" y: "+ attackPosition.y +
            " Posi��o atual: z:" + transform.position.x +" y: "+ transform.position.y);*/
    }

    private void HardMode()
    {
        halfLife = true;
        recoverTime = 1f;
        //Debug.Log("Metade da vida");
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void TakeDamage(int power)
    {
        if (life <= 0) return;
        float armor = 1;
        if (attacking) armor = baseArmor;
        //Debug.Log("Dano: " + Mathf.RoundToInt(power * armor) + "(" + power + " * " + armor + ")");
        this.life -= Mathf.RoundToInt(power * armor);
        CreateFloatingDamage(Mathf.RoundToInt(power * armor));
        slider.value = life;
        if (life <= 0)
        {
            Death();
        }
    }

    private void DestroyAfterDeath()
    {
        GameObject[] bossAtks = GameObject.FindGameObjectsWithTag("BossAtk");

        foreach (GameObject atk in bossAtks) Destroy(atk);
    }

    private void SetArmor()
    {
        attacking = true;
    }

    private void Exaustion()
    {
        baseArmor = 0.5f;
        attacking = false;
        if(transform.childCount > 1)
            Destroy(transform.GetChild(1).gameObject);
    }

    private void Death()
    {
        GetComponent<ShakeEffect>().perpetualTime = 0.5f;
        ChangeAnimation(DEATH.name);
        Destroy(gameObject, 5f);
        Destroy(slider.gameObject);
        DestroyAfterDeath();
        teleport.SetActive(true);
    }
}
