using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private int nRooms, nPaths;
    private GameObject[] rooms;
    private GameObject[] paths;
    private Graph levelLayout;

    
    // Start is called before the first frame update
    void Start()
    {
        rooms = GameObject.FindGameObjectsWithTag("Room");
        nRooms = rooms.Length;
        paths = GameObject.FindGameObjectsWithTag("Path");
        nPaths = paths.Length;

        CalculateLevelLayout();
        GenerateLevelLayout();
    }

    private void CalculateLevelLayout()
    {
        levelLayout = new Graph(nRooms);

        for (int i = 0; i < nPaths; i++)
        {
            //levelLayout.AddEdge(paths[i]);
        }
    }

    private void GenerateLevelLayout()
    {

    }
}
