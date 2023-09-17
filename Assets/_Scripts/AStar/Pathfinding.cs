using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; set; }

    private LevelGrid<PathNode> grid;
    private List<PathNode> openList, closedList;

    public Pathfinding(int width, int height, float cellSize, Vector3 originPos)
    {
        Instance = this;
        grid = new LevelGrid<PathNode>(width, height, cellSize, originPos, (LevelGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public List<Vector3> FindPathWorld(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        grid.GetXY(startWorldPos, out int startX, out int startY);
        grid.GetXY(endWorldPos, out int endX, out int endY);

        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            return null;
        } else
        {
            bool showDebug = true;
            if (showDebug)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 0.64f + Vector3.one * 0.32f, new Vector3(path[i+1].x, path[i+1].y) * 0.64f + Vector3.one * 0.32f, Color.green, 100f);
                }
            }


            List<Vector3> vectorPath = new List<Vector3>();
            foreach(PathNode node in path)
            {
                vectorPath.Add(new Vector3(node.x, node.y) * grid.GetCellSize() + new Vector3(grid.GetCellSize() * 0.5f, grid.GetCellSize() * 0.8f));
            }
            return vectorPath;
        }
    }
    
    public List<PathNode> FindPath(int xStart, int yStart, int xFinal, int yFinal)
    {
        PathNode startNode = grid.GetGridObject(xStart, yStart);
        PathNode endNode = grid.GetGridObject(xFinal, yFinal);

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

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.prevNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            // Left Down
            if (currentNode.y - 1 >= 0 && 
                (GetNode(currentNode.x - 1, currentNode.y - 1).isWalkable &&
                GetNode(currentNode.x, currentNode.y - 1).isWalkable))
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            // Left Up
            if (currentNode.y + 1 < grid.GetHeight() &&
                (GetNode(currentNode.x - 1, currentNode.y - 1).isWalkable &&
                GetNode(currentNode.x, currentNode.y + 1).isWalkable)) 
                neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            // Right Down
            if (currentNode.y - 1 >= 0 &&
                (GetNode(currentNode.x + 1, currentNode.y).isWalkable &&
                GetNode(currentNode.x, currentNode.y - 1).isWalkable)) 
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            // Right Up
            if (currentNode.y + 1 < grid.GetHeight() && 
                (GetNode(currentNode.x + 1, currentNode.y).isWalkable &&
                GetNode(currentNode.x, currentNode.y + 1).isWalkable)) 
                neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    public Vector3 GetNodeWorldPositionCenter(int x, int y)
    {
        return grid.GetWorldPositionCenter(x, y);
    }

    public LevelGrid<PathNode> GetGrid()
    {
        return grid;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.prevNode != null)
        {
            path.Add(currentNode.prevNode);
            currentNode = currentNode.prevNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
