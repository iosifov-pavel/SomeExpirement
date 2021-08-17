using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    [SerializeField] int ID = -1;
    [SerializeField] Vector2Int startPosition;
    [SerializeField] Vector2Int endPosition;
    [SerializeField] Vector2Int size;
    [SerializeField] List<int> neighbors = new List<int>();
    int[,] roomMap;

    public void InitRoom(Vector2Int startPos, Vector2Int endPos)
    {
        startPosition = startPos;
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

    void RoomGeneration()
    {

    }

    public void Neighbors()
    {
        neighbors.Clear();
        List<Room> allRooms = Floor.Instance.GetRooms();
        List<Coridor> allCoridors = Floor.Instance.GetCoridors();
        Vector2Int point1 = Vector2Int.zero;
        Vector2Int point2 = Vector2Int.zero;
        for (int i = 0; i < size.x; i++)
        {    
            point1.x = startPosition.x + i;
            point2.x = startPosition.x + i;
            point1.y = startPosition.y+1 - 1;
            point2.y = endPosition.y-1 + 1;
            foreach(Room maybeNeighbor in allRooms)
            {
                if (maybeNeighbor.ContainsPoint(point1))
                {
                    if (neighbors.Contains(maybeNeighbor.GetID())) { break; }
                    neighbors.Add(maybeNeighbor.GetID());
                    Coridor coridor = new Coridor();
                    coridor.InitCoridor(GetID(), maybeNeighbor.GetID());
                    allCoridors.Add(coridor);
                    DebugDrawLine(maybeNeighbor, this);
                }
                else if (maybeNeighbor.ContainsPoint(point2))
                {
                    if (neighbors.Contains(maybeNeighbor.GetID())) break;
                    neighbors.Add(maybeNeighbor.GetID());
                    Coridor coridor = new Coridor();
                    coridor.InitCoridor(GetID(), maybeNeighbor.GetID());
                    allCoridors.Add(coridor);
                    DebugDrawLine(maybeNeighbor, this);
                }
            }
        }
        for (int i = 0; i < size.y; i++)
        {
            point1.y = startPosition.y + i;
            point2.y = startPosition.y + i;
            point1.x = startPosition.x+1 - 1;
            point2.x = endPosition.x-1 + 1;
            foreach (Room maybeNeighbor in allRooms)
            {
                if (maybeNeighbor.ContainsPoint(point1))
                {
                    if (neighbors.Contains(maybeNeighbor.GetID())) break;
                    neighbors.Add(maybeNeighbor.GetID());
                    Coridor coridor = new Coridor();
                    coridor.InitCoridor(GetID(), maybeNeighbor.GetID());
                    allCoridors.Add(coridor);
                    DebugDrawLine(maybeNeighbor, this);
                }
                else if (maybeNeighbor.ContainsPoint(point2))
                {
                    if (neighbors.Contains(maybeNeighbor.GetID())) break;
                    neighbors.Add(maybeNeighbor.GetID());
                    Coridor coridor = new Coridor();
                    coridor.InitCoridor(GetID(), maybeNeighbor.GetID());
                    allCoridors.Add(coridor);
                    DebugDrawLine(maybeNeighbor, this);
                }
            }
        }
        Debug.Log(neighbors.Count);
    }

    static void DebugDrawLine(Room one, Room two)
    {
        int X = (one.GetStartPosition().x + one.GetEndPosition().x) / 2;
        int Y = (one.GetStartPosition().y + one.GetEndPosition().y) / 2;
        Vector3 pos2 = new Vector3(X, Y, 0);
        int X2 = (two.GetStartPosition().x + two.GetEndPosition().x) / 2;
        int Y2 = (two.GetStartPosition().y + two.GetEndPosition().y) / 2;
        Vector3 pos1 = new Vector3(X2, Y2, 0);
        Debug.DrawLine(pos1, pos2, Color.green, 50000);
    }

    public int[,] GetRoomMap()
    {
        return roomMap;
    }

    public Vector2Int GetStartPosition()
    {
        return startPosition;
    }
    public Vector2Int GetSize()
    {
        return size;
    }
    public Vector2Int GetEndPosition()
    {
        return endPosition;
    }
    public int GetID()
    {
        return ID;
    }
    public void SetID(int id)
    {
        ID = id;
    }

    public bool ContainsPoint(Vector2Int point)
    {
        bool X = point.x >= startPosition.x && point.x <= endPosition.x;
        bool Y = point.y >= startPosition.y && point.y <= endPosition.y;
        return X && Y;
    }
}
