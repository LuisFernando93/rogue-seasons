using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController: MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;

    [SerializeField] private GameObject Wall;

    private Pathfinding pathfinding;
    private Vector2 nodePos;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);

        for (int i = 0; i < gridWidth; i++) {
            for (int j = 0; j < gridHeight; j++) {

                nodePos = pathfinding.GetNodeWorldPositionCenter(i, j);
                
                if (Wall.GetComponent<Collider2D>().OverlapPoint(nodePos)) {
                    PathNode node = pathfinding.GetNode(i, j);
                    node.SetIsWalkable(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
