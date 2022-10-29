using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private RoomGrid<PathNode> grid;
    private int x, y;
    public int gCost, hCost, fCost;

    public PathNode prevNode;

    public PathNode(RoomGrid<PathNode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
