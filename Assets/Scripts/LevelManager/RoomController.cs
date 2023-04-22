using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController: MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;

    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Player;

    private Vector2 nodePos;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinding.Instance = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);


        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                nodePos = Pathfinding.Instance.GetNodeWorldPositionCenter(i, j);

                if (Wall.GetComponent<Rigidbody2D>().OverlapPoint(nodePos))
                {
                    PathNode node = Pathfinding.Instance.GetNode(i, j);
                    node.SetIsWalkable(false);
                }
            }
        }

        Wall.GetComponent<TilemapCollider2D>().usedByComposite = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Pathfinding.Instance.GetGrid().GetXY(Player.transform.position, out int x, out int y);
            Debug.Log("X: " + x + " Y: " + y);
            List<PathNode> path = Pathfinding.Instance.FindPath(1,1,x,y);
            if(path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 0.64f + Vector3.one * 0.32f, new Vector3(path[i+1].x, path[i+1].y) * 0.64f + Vector3.one * 0.32f, Color.green, 100f);
                }
            }
        }
    }
}
