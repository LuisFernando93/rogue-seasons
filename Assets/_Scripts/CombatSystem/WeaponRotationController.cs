using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationController : MonoBehaviour
{
    Vector3 mousePos;

    public void WeaponRotation()
    {
        //calcula a posi��o do mouse de acordo com a camera 
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Faz os ajustes para ela rodar em volta do proprio eixo e execuda a corre��o de inclina��o de acordo com o sentido que o player esta virado
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 90);

 
    }
}
