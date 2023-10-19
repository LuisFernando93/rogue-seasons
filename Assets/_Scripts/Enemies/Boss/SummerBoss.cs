using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerBoss : Boss
{
    Transform player;

    [SerializeField]
    AnimationClip IDLE, ATTACK01, ATTACK02_ENTRY, ATTACK02_PREP, ATTACK02_DAMAGE, EVOKE_RAIN, CUT, EXAUSTION, RECOVER,
        TELEPORT, ULT, ULT_LOOP;
    [SerializeField] GameObject attackPoint, arrowPrefab, arrowRainPrefab, cutPrefab;
    [SerializeField] int arrowForce;
    [SerializeField] bool halfLife = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Attacks.Add("CallAttack01", 1);
        Attacks.Add("CallAttack02", 1);
        Attacks.Add("CallAttack03", 1);
        Attacks.Add("CallAttack04", 1);

        life = baseLife;
        SelectNextAttack();
    }

    private void BackToIdle()
    {
        ChangeAnimation(IDLE.name);
    }

    public void SelectNextAttack()
    {
        ResetRotation();
        BackToIdle();
        if(life <= (baseLife * 0.5f) && !halfLife)
        {
            HardMode();
        }
        string nextAttack = Attacks.GetRandom();
        Invoke(nextAttack, recoverTime);
    }

    private void CallAttack01()
    {
        ChangePosition(player.position.x, player.position.y+1f, 0f);
        power = 5;
        ChangeAnimation(ATTACK01.name);
    }

    private void CallAttack02()
    {
        ChangePosition(player.position.x-3, player.position.y, 0f);
        power = 10;
        ChangeAnimation(ATTACK02_ENTRY.name);
    }

    private void CallAttack03()
    {
        ChangeAnimation(EVOKE_RAIN.name);
        Instantiate(arrowRainPrefab, player.transform.position, player.transform.rotation);
    }

    private void CallAttack04()
    {
        ChangePosition(0, 0, 0);
        if(player.transform.position.x < 0f){
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        ChangeAnimation(CUT.name);
    }

    private void Ultimate()
    {
        ChangePosition(0, 0, 0);
        power = 1;
        ChangeAnimation(ULT.name);
    }
    public void ArrowShot()
    {
        GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPoint.transform.right * arrowForce, ForceMode2D.Impulse);
    }

    public void CreateCut()
    {
        Vector3 position = new Vector3(0, 5.56f, 0);
        GameObject cut = Instantiate(cutPrefab, position, transform.rotation);
        Destroy(cut, 0.5f);
    }


    private void ChangePosition(float x, float y, float z)
    {
        Vector3 attackPosition;
        attackPosition = new Vector3(x,y,z);
        transform.position = attackPosition;
        /*Debug.Log("Posição a ser chamada x:" + attackPosition.x +" y: "+ attackPosition.y +
            " Posição atual: z:" + transform.position.x +" y: "+ transform.position.y);*/
    }

    private void HardMode()
    {
        halfLife = true;
        recoverTime = 0.5f;
        Attacks.Add("Ultimate", 1);
        //Debug.Log("Metade da vida");
    }
    
    private void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
