using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private RoomGrid<PathNode> grid;
    private List<PathNode> openList, closedList;

    public Pathfinding(int width, int height, float cellSize, Vector3 originPos)
    {
        grid = new RoomGrid<PathNode>(width, height, cellSize, originPos, (RoomGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    private List<PathNode> FindPath(int xStart, int yStart, int xFinal, int yFinal)
    {
        PathNode startNode = grid.GetGridObject(xStart, yStart);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < grid.GetWidth(); x++) {
            for (int y = 0; y < grid.GetHeight(); y++) {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.prevNode = null;
            }
        }
        return null;
    }
}
