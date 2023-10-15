using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PrimAlgorithm
{ 
    public static List<Edge> FindMST(Graph graph)
    {
        List<Edge> minimumSpanningTree = new List<Edge>();
        HashSet<int> visitedVertices = new HashSet<int>();

        visitedVertices.Add(0);
        //Debug.Log("Numero de vertices no grafo: " + graph.getNVertices());

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
                //Debug.Log("Adicionando conector " + minEdge.getId() + " ao MST");
                minimumSpanningTree.Add(minEdge);
                visitedVertices.Add(minEdge.getTo());
            }
            else
            {
                Debug.Log("Saindo");
                break;
            }
        }


        return minimumSpanningTree;
    }
}
