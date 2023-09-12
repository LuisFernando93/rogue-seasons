using System.Collections;
using System.Collections.Generic;

public class PrimAlgorithm
{ 
    public static List<Edge> FindMST(Graph graph)
    {
        List<Edge> minimumSpanningTree = new List<Edge>();
        HashSet<int> visitedVertices = new HashSet<int>();

        visitedVertices.Add(0);

        while(visitedVertices.Count < graph.getNVertices())
        {
            Edge minEdge = null;

            foreach(int visitedVertex in visitedVertices)
            {
                foreach (Edge edge in graph.getAdjacencyList()[visitedVertex])
                {
                    if (!visitedVertices.Contains(edge.getTo()) && (minEdge == null || edge.getWeight() < minEdge.getWeight()))
                    {
                        minEdge = edge;
                    }
                }
            }

            if (minEdge != null)
            {
                minimumSpanningTree.Add(minEdge);
                visitedVertices.Add(minEdge.getTo());
            }
            else
            {
                break;
            }
        }


        return minimumSpanningTree;
    }
}
