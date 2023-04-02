using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class EnemyController: MonoBehaviour
{
    [SerializeField] protected GameObject floatingPoints;
    private int pathIndex;
    protected GameObject player;
    private List<Vector3> pathVectorList;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public abstract void TakeDamage(int power);

    protected void FloatingDamage(int damage)
    {
        GameObject point = Instantiate(floatingPoints, transform.position, transform.rotation);
        point.transform.SetParent(this.transform);
        point.GetComponent<TextMeshPro>().text = damage.ToString();
        if (damage <= 1)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
        else if (damage > 1 && damage <= 2)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 103, 0, 255);
        }
        else if (damage > 2 && damage <= 5)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 0, 0, 255);
        }
        else
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
    }

    protected void GetPlayerPosition()
    {

    }
}
