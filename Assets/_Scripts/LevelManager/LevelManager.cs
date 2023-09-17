using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager: MonoBehaviour
{

    //LevelManager object must be placed at 0,0, to avoid problems with A*

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;
    
    [SerializeField] private AudioClip defaultSummerOST;
    [SerializeField] private AudioClip battleSummerOST;

    [SerializeField] private LayerMask solidLayer;
    [SerializeField] private GameObject player;

    private int level;
    private GameObject[] rooms;
    private int roomsCleared;
    private int totalRooms;
    private bool showDebugPathfinder = false;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        roomsCleared = 0;
        rooms = GameObject.FindGameObjectsWithTag("Room");
        totalRooms = rooms.Length;
        SoundManager.Instance.PlayMusic(defaultSummerOST);
        Pathfinding.Instance = new Pathfinding(gridWidth, gridHeight, gridCellSize, transform.position);

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector2 nodePos = Pathfinding.Instance.GetNodeWorldPositionCenter(i, j);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(nodePos, (gridCellSize * 0.95f) / 2, solidLayer);
                if (colliders.Length > 0) //verifica se existe parede na grid aqui
                {
                    PathNode node = Pathfinding.Instance.GetNode(i, j);
                    node.SetIsWalkable(false);
                }
            }
        }

        foreach (GameObject room in rooms)
        { //ativa a colisao composta das salas apos instanciar o pathfinding
            room.GetComponent<RoomController>().setWallCompositeCollision(true);
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
