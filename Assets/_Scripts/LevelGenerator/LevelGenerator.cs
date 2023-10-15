using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private int nRooms, nConnectors;
    private GameObject[] rooms;
    private GameObject[] connectors;
    private Graph levelLayout;
    private List<Edge> MinimumSpanningTree;

    
    // Start is called before the first frame update
    void Start()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        nRooms = rooms.Length;
        connectors = GameObject.FindGameObjectsWithTag("Connector");
        nConnectors = connectors.Length;
        //Debug.Log("Numero de salas: " + nRooms);
        //Debug.Log("Numero de conectores: " + nConnectors);
        CalculateLevelLayout();
        GenerateLevelLayout();
    }

    private void CalculateLevelLayout()
    {
        //Debug.Log("iniciando calculo de mapa");
        levelLayout = new Graph(nRooms);

        for (int i = 0; i < nConnectors; i++)
        {
            levelLayout.AddEdge(connectors[i].GetComponent<Connector>().getFrom(), connectors[i].GetComponent<Connector>().getTo(), connectors[i].GetComponent<Connector>().getWeight(), connectors[i].GetComponent<Connector>().getId()) ;
            //Debug.Log("Peso da aresta " + connectors[i].GetComponent<Connector>().getId() + ": " + connectors[i].GetComponent<Connector>().getWeight());
        }

        MinimumSpanningTree = PrimAlgorithm.FindMST(levelLayout);
    }

    private void GenerateLevelLayout()
    {
        //Debug.Log("iniciando geracao de mapa");
        foreach (GameObject connector in connectors)
        {
            bool existsInMST = false;
            foreach (Edge edge in MinimumSpanningTree)
            {
                if (edge.getId() == connector.GetComponent<Connector>().getId())
                {
                    //Debug.Log("Aresta " + path.GetComponent<Path>().getId() + " existe dentro do MST");
                    existsInMST = true;
                }
            }
            if (existsInMST)
            {
                connector.GetComponent<ConnectorActuator>().SetActive();
            } else
            {
                connector.GetComponent<ConnectorActuator>().SetDeactive();
            }
        }
    }
}
