using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager: MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;

    [SerializeField] private AudioClip defaultSummerOST;
    [SerializeField] private AudioClip battleSummerOST;

    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject Player;

    private int level;
    private bool showDebugPathfinder = false;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        SoundManager.Instance.PlayMusic(defaultSummerOST);
        Pathfinding.Instance = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector2 nodePos = Pathfinding.Instance.GetNodeWorldPositionCenter(i, j);

                if (Wall.GetComponent<Rigidbody2D>().OverlapPoint(nodePos))
                {
                    PathNode node = Pathfinding.Instance.GetNode(i, j);
                    node.SetIsWalkable(false);
                }
            }
        }

        Wall.GetComponent<TilemapCollider2D>().usedByComposite = true;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showDebugPathfinder)
        {
            debugPathfinderInitToPlayer();
        }
    }

    private void debugPathfinderInitToPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pathfinding.Instance.GetGrid().GetXY(Player.transform.position, out int x, out int y);
            Debug.Log("X: " + x + " Y: " + y);
            List<PathNode> path = Pathfinding.Instance.FindPath(1, 1, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * gridCellSize + Vector3.one * gridCellSize / 2, new Vector3(path[i + 1].x, path[i + 1].y) * gridCellSize + Vector3.one * gridCellSize / 2, Color.green, 100f);
                }
            }
        }
    }
}
