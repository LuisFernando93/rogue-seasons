using System;
using UnityEngine;

public class Path : MonoBehaviour
{
    private float Weight;
    [SerializeField] private int From;
    [SerializeField] private int To;
    [SerializeField] private int Id;

    // Start is called before the first frame update
    void Awake()
    {
        this.Weight = UnityEngine.Random.value;
    }

    public float getWeight()
    {
        return this.Weight;
    }

    public int getFrom()
    {
        return this.From;
    }

    public int getTo()
    {
        return this.To;
    }

    public int getId()
        { return this.Id; }
}
