using System.Collections;
using System.Collections.Generic;

public class Graph
{
    private int nVertices;
    private List<List<Edge>> AdjacencyList;

    public Graph(int verticesCount)
    {
        this.nVertices = verticesCount;
        this.AdjacencyList = new List<List<Edge>>(verticesCount);

        for (int i = 0; i < nVertices; i++)
        {
            AdjacencyList.Add(new List<Edge>());
        }
    }

    public int getNVertices()
    {
        return nVertices;
    }

    public List<List<Edge>> getAdjacencyList()
    {
        return AdjacencyList;
    }

    public void AddEdge(int from, int to, float weight, int id)
    {
        AdjacencyList[from].Add(new Edge(from, to, weight, id));
        AdjacencyList[to].Add(new Edge(from, to, weight, id));
    }

}
