using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingDamage : MonoBehaviour
{
    [SerializeField] protected GameObject floatingPoints;
    int mediumDamage = 500, HighDamage = 1000;

    protected void CreateFloatingDamage(int damage)
    {
        GameObject point;
        if (Mathf.Abs(this.gameObject.transform.rotation.eulerAngles.y) > 0.01f)
        {
            // Se a rotação do objeto pai não for zero, defina uma rotação padrão para a instância
            Quaternion defaultRotation = Quaternion.Euler(0f, 0f, 0f); // Substitua pelos ângulos desejados

            // Crie a instância com a rotação padrão
            point = Instantiate(floatingPoints, transform.position, defaultRotation);
        }
        else
        {
            point = Instantiate(floatingPoints, transform.position, transform.rotation);

        }
        point.transform.SetParent(this.transform);
        point.GetComponent<TextMeshPro>().text = damage.ToString();
        if (damage <= mediumDamage)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
        else if (damage > mediumDamage && damage <= HighDamage)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 103, 0, 255);
        }
        else if (damage > HighDamage)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 0, 0, 255);
        }
        else
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
    }
}
