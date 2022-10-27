using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGrid {

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPos;
    private int[,] gridArray;

    public RoomGrid(int width, int height, float cellSize, Vector3 originPos)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        gridArray = new int[width, height];
        
        gridArray = new int [8,8]  {{ 1, 1, 1, 1, 1, 1, 1, 1},{ 1, 0, 0, 0, 0, 0, 0, 1},{ 1, 0, 1, 0, 0, 1, 0, 1},{ 1, 0, 1, 0, 0, 1, 0, 1},{ 1, 0, 1, 0, 0, 1, 0, 1},{ 1, 0, 1, 0, 0, 1, 0, 1},{ 1, 0, 0, 0, 0, 0, 0, 1},{ 1, 1, 1, 1, 1, 1, 1, 1}};
        
        for (int x = 0; x < this.width; x++)
        { for   (int y = 0; y < this.height; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                //gridArray[x, y] = 0;
            }
        }
        Debug.DrawLine(GetWorldPosition(0, this.height), GetWorldPosition(this.width, this.height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(this.width, 0), GetWorldPosition(this.width, this.height), Color.white, 100f);
        

    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * this.cellSize + originPos;
    }

    public Vector3 GetWorldPositionCenter(int x, int y)
    {
        return new Vector3(x, y) * this.cellSize + originPos + new Vector3(this.cellSize/2,this.cellSize/2);
    }

    private void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos-originPos).x / this.cellSize);
        y = Mathf.FloorToInt((worldPos-originPos).y / this.cellSize);
    }

    public int GetCellValue(int x, int y)
    {
        return gridArray[x, y];
    }

}
