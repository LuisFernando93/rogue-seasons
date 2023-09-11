using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    [SerializeField] public string teste01;
    [SerializeField] public string teste02;

    string[] text1;

    private void Start()
    {
        text1 = new string[2];

        text1[0] = teste01;
        text1[1] = teste02;

        Debug.Log(text1[0]);
        Debug.Log(text1[1]);

    }

}
