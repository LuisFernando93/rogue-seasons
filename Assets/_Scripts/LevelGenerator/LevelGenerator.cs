using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private int nRooms, nPaths;
    private GameObject[] rooms;
    private GameObject[] paths;
    private Graph levelLayout;
    private List<Edge> MinimumSpanningTree;

    
    // Start is called before the first frame update
    void Start()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        nRooms = rooms.Length;
        paths = GameObject.FindGameObjectsWithTag("Path");
        nPaths = paths.Length;
        Debug.Log(nPaths);
        CalculateLevelLayout();
        GenerateLevelLayout();
    }

    private void CalculateLevelLayout()
    {
        //Debug.Log("iniciando calculo de mapa");
        levelLayout = new Graph(nRooms);

        for (int i = 0; i < nPaths; i++)
        {
            levelLayout.AddEdge(paths[i].GetComponent<Path>().getFrom(), paths[i].GetComponent<Path>().getTo(), paths[i].GetComponent<Path>().getWeight(), paths[i].GetComponent<Path>().getId()) ;
            //Debug.Log("Peso da aresta " + paths[i].GetComponent<Path>().getId() + ": " + paths[i].GetComponent<Path>().getWeight());
        }

        MinimumSpanningTree = PrimAlgorithm.FindMST(levelLayout);
    }

    private void GenerateLevelLayout()
    {
        //Debug.Log("iniciando geracao de mapa");
        foreach (GameObject path in paths)
        {
            bool existsInMST = false;
            foreach (Edge edge in MinimumSpanningTree)
            {
                if (edge.getId() == path.GetComponent<Path>().getId())
                {
                    //Debug.Log("Aresta " + path.GetComponent<Path>().getId() + " existe dentro do MST");
                    existsInMST = true;
                }
            }
            if (!existsInMST)
            {
                path.SetActive(false);
            }
        }
    }
}
