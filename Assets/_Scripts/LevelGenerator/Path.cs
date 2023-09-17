using System;
using UnityEngine;

public class Path : MonoBehaviour
{
    private float Weight;
    [SerializeField] private int From;
    [SerializeField] private int To;

    // Start is called before the first frame update
    void Start()
    {
        this.Weight = UnityEngine.Random.value;
    }

}
