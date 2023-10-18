using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummerBoss : Boss
{
    Transform player;

    [SerializeField]
    AnimationClip IDLE, ATTACK01, ATTACK02_ENTRY, ATTACK02_PREP, ATTACK02_DAMAGE, EXAUSTION, RECOVER,
        TELEPORT, ULT, ULT_LOOP, ARROW, ARROWRAIN_ENTRY, ARROWRAIN;
    [SerializeField] GameObject attackPoint, arrowPrefab;
    [SerializeField] int arrowForce;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Attacks.Add("CallAttack01", 1);
        Attacks.Add("CallAttack02", 1);

        SelectNextAttack();
    }

    private void BackToIdle()
    {
        ChangeAnimation(IDLE.name);
    }

    public void SelectNextAttack()
    {
        BackToIdle();
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
        ChangeAnimation(ATTACK02_ENTRY.name);
    }

    public void ArrowShot()
    {
        GameObject arrow = Instantiate(arrowPrefab, attackPoint.transform.position, attackPoint.transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPoint.transform.right * arrowForce, ForceMode2D.Impulse);
    }

    private void ChangePosition(float x, float y, float z)
    {
        Vector3 attackPosition;
        attackPosition = new Vector3(x,y,z);
        transform.position = attackPosition;
        /*Debug.Log("Posição a ser chamada x:" + attackPosition.x +" y: "+ attackPosition.y +
            " Posição atual: z:" + transform.position.x +" y: "+ transform.position.y);*/
    }

}
