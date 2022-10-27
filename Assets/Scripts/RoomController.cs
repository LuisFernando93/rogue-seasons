using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController: MonoBehaviour
{
    [SerializeField] private int gridWidth = 8;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private float gridCellSize = 0.64f;

    public GameObject floorPrefab;
    public GameObject wallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RoomGrid grid = new RoomGrid(gridWidth, gridHeight, gridCellSize, new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y));
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                if (grid.GetCellValue(x, y) == 0)
                {
                    Instantiate(floorPrefab, grid.GetWorldPositionCenter(x, y), Quaternion.identity);
                }
                else if (grid.GetCellValue(x, y) == 1)
                {
                    Instantiate(wallPrefab, grid.GetWorldPositionCenter(x, y), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
