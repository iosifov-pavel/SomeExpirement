using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uRandom = UnityEngine.Random;

[System.Serializable]
public class LevelMap
{
    public static LevelMap Instance = null;
    int[,] map;
    [SerializeField]Vector2Int levelSize;
    int cellCount;
    int clearCellCount = 0;
    public void InitMap(Vector2Int size)
    {
        levelSize = size;
        map = new int[size.x, size.y];
        cellCount = size.x * size.y;
        Instance = this;
        FillArray();
    }

    private void FillArray()
    {
        for(int i=0;i<levelSize.x;i++)
            for(int j = 0; j < levelSize.y; j++)
            {
                map[i, j] = -1;
            }
    }

    public int[,] GetMap()
    {
        GenerateMap();
        return map;
    }

    public void GenerateMap()
    {
        Vector2Int point = Vector2Int.zero;
        point.x = uRandom.Range(0, levelSize.x);
        point.y = uRandom.Range(0, levelSize.y);
        map[point.x, point.y] = 0;
        clearCellCount++;
        int steps = 0;
        int maxSteps = uRandom.Range(5000, 10000);
        while (clearCellCount < cellCount / 2 && steps< maxSteps)
        {
            int moveX = uRandom.Range(-1, 2);
            int moveY=0;
            if (moveX == 0)
            {
                moveY = uRandom.Range(-1, 2);
            }
            point += new Vector2Int(moveX, moveY);
            if (point.x == levelSize.x-1)
            {
                point.x = levelSize.x - 2;
            }
            if (point.x == 0)
            {
                point.x = 1;
            }
            if (point.y == levelSize.y-1) 
            { 
                point.y = levelSize.y - 2;
            }
            if (point.y == 0)
            {
                point.y = 1;
            }
            if (map[point.x, point.y] == -1)
            {
                map[point.x, point.y] = 0;
                clearCellCount++;
            }
            steps++;
            if (steps == maxSteps) Debug.Log("Too Much steps");
        }
    }
}
