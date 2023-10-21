using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public abstract class EnemyController: FloatingDamage
{
    protected int pathIndex;
    protected float pathfindTimer = 0;
    protected float pathUpdateTime = 0.1f;
    protected GameObject player;
    protected List<Vector3> pathVectorList;
    

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public abstract void TakeDamage(int power);

    protected void FindPlayerPosition()
    {
        pathIndex = 0;
        try { pathVectorList = Pathfinding.Instance.FindPathWorld(transform.position, player.transform.position);
        }
        catch (NullReferenceException e)
        { pathVectorList = null;}
        if(pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
        }
    }
}
