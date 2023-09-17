using System;
using UnityEngine;

public class Connector : MonoBehaviour
{

    //Always set the from and to correctly, otherwise the Prim Algorithm will malfunction. The direction does not matter, as long as it connects the respective two rooms

    private float Weight;
    [SerializeField] private int From; //from which room
    [SerializeField] private int To; //to which room
    [SerializeField] private int Id; //unique identifiers

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
