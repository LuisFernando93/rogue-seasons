using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager: MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;
    private int totalRooms;

    [SerializeField] private AudioClip defaultSummerOST;
    [SerializeField] private AudioClip battleSummerOST;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject player;

    private int level;
    private int roomsCleared;
    private bool showDebugPathfinder = false;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        roomsCleared = 0;
        totalRooms = GameObject.FindGameObjectsWithTag("Room").Length;
        SoundManager.Instance.PlayMusic(defaultSummerOST);
        Pathfinding.Instance = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector2 nodePos = Pathfinding.Instance.GetNodeWorldPositionCenter(i, j);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(nodePos, (gridCellSize * 0.95f) / 2, layerMask);
                if (colliders.Length > 0) //verifica se existe parede na grid aqui
                {
                    PathNode node = Pathfinding.Instance.GetNode(i, j);
                    node.SetIsWalkable(false);
                }
            }
        }
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
            DebugPathfinderInitToPlayer();

        }

        if(player.GetComponent<Player>().IsDead())
        {
            NewGame();
        }
    }

    private void DebugPathfinderInitToPlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Pathfinding.Instance.GetGrid().GetXY(player.transform.position, out int x, out int y);
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

    public void RoomCleared()
    {
        roomsCleared++;
        if (roomsCleared >= totalRooms)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Destroy(this);
    }
}
