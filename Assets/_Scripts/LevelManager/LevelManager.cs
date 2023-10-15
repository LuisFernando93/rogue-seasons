using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager: MonoBehaviour
{
    //LevelManager object must be placed at 0,0, to avoid problems with A*
    [SerializeField] WeightedRandomList<GameObject> levelLayouts;

    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private float gridCellSize = 0.64f;
    
    [SerializeField] private AudioClip defaultSummerOST;
    [SerializeField] private AudioClip battleSummerOST;

    [SerializeField] private LayerMask solidLayer;
    private GameObject player;

    private GameObject[] rooms;
    private GameObject[] walls;
    private GameObject entrance;
    [HideInInspector] public int roomsCleared;
    [HideInInspector] public int totalRooms;
    private bool showDebugPathfinder = false;
    int level;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        CreateLevelLayout();
        gameObject.AddComponent<LevelGenerator>();

        roomsCleared = 0;
        rooms = GameObject.FindGameObjectsWithTag("Room");
        entrance = GameObject.FindGameObjectWithTag("Entrance");
        walls = GameObject.FindGameObjectsWithTag("Wall");
        totalRooms = rooms.Length - 1;
        SoundManager.Instance.PlayDualMusic(defaultSummerOST, battleSummerOST);
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

        foreach (GameObject wall in walls)
        { //ativa a colisao composta das salas apos instanciar o pathfinding
            wall.GetComponent<TilemapCollider2D>().usedByComposite = true;
        }

        SetPlayerPositionToEntrance();
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
            BackToHub();
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
            Debug.Log("Todas as salas concluidas!!");
        }
    }

    public void nextLevel()
    {
        
    }

    private void BackToHub()
    {
        SceneManager.LoadScene("Hub");
    }

    private void SetPlayerPositionToEntrance()
    {
        if (entrance != null)
        {
            player.transform.position = entrance.transform.position;
        }
    }

    private void CreateLevelLayout()
    {
        GameObject layout = levelLayouts.GetRandom();

        gridHeight = layout.GetComponent<LevelLayout>().gridheight;
        gridWidth = layout.GetComponent<LevelLayout>().gridWidth;

        Instantiate(layout);
    }
}
