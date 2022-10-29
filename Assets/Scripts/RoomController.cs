using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController: MonoBehaviour
{
    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private float gridCellSize = 0.64f;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding pathfinding = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
