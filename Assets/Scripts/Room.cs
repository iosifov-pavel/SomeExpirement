using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public Vector2Int StartPosition { get; set; }
    Vector2Int endPosition;
    Vector2Int size;
    int[,] roomMap;

    public void InitRoom(Vector2Int startPos, Vector2Int endPos)
    {
        StartPosition = startPos;
        endPosition = endPos;
        size = endPos - startPos;
        roomMap = new int[size.x, size.y];
        TestGen();
    }

    void TestGen()
    {
        for(int i = 0; i < size.x;i++)
            for(int j = 0; j < size.y; j++)
            {
                if (i == 0 || j == 0) roomMap[i, j] = 0;
                else roomMap[i, j] = 1;
            }
    }

    public int[,] GetRoomMap()
    {
        return roomMap;
    }

}
