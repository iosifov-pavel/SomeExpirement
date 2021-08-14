using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public Vector2Int LevelSize { get; set; }
    [SerializeField] List<Area> allAreas= new List<Area>();
    [SerializeField] List<Area> areasCapableForRoom = new List<Area>();
    [SerializeField] List<Room> rooms= new List<Room>();
    [SerializeField] List<Coridor> coridors = new List<Coridor>();
    int[,] map;

    public void InitFloor(Vector3Int mapSize)
    {
        LevelSize = new Vector2Int(mapSize.x, mapSize.y);
        map = new int[mapSize.x, mapSize.y];
    }

    public void GenerateAreas()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(LevelSize.x, 0, 0), Color.green, 50000);
        Debug.DrawLine(Vector3.zero, new Vector3(0, LevelSize.y, 0), Color.green, 50000);
        Debug.DrawLine(new Vector3(LevelSize.x, 0, 0), new Vector3(LevelSize.x, LevelSize.y, 0), Color.green, 50000);
        Debug.DrawLine(new Vector3(0, LevelSize.y, 0), new Vector3(LevelSize.x, LevelSize.y, 0), Color.green, 50000);
        //--------------
        //--------------
        Queue<Area> toDivideQueue = new Queue<Area>();
        Area rootArea = new Area();
        rootArea.InitArea(Vector2Int.zero, LevelSize, 100);
        allAreas.Add(rootArea);
        toDivideQueue.Enqueue(rootArea);
        while (toDivideQueue.Count > 0)
        {
            Area toDivied = toDivideQueue.Dequeue();
            if (toDivied.CanBeDivided)
            {
                Area[] childs = toDivied.SplitArea();
                toDivied.CanBeDivided = false;
                toDivied.WasDivided = true;
                toDivideQueue.Enqueue(childs[0]);
                toDivideQueue.Enqueue(childs[1]);
                allAreas.Add(childs[0]);
                allAreas.Add(childs[1]);
            } 
        }
        foreach(Area area in allAreas)
        {
            if (!area.WasDivided) areasCapableForRoom.Add(area);
        }
    }

    public void GenerateRooms()
    {
        foreach(Area area in areasCapableForRoom)
        {
            rooms.Add(area.CreateRoom());
        }
    }

    public void GenerateCoridors()
    {

    }

    public void CompleteLevelMap()
    {
        foreach (Room room in rooms)
        {
            Vector2Int roomPosition = room.StartPosition;
            int[,] roomMap = room.GetRoomMap();
            for (int i = 0; i < roomMap.GetUpperBound(0); i++)
                for (int j = 0; j < roomMap.GetUpperBound(1); j++)
                {
                    if (roomMap[i, j] == 0)
                    {
                        map[roomPosition.x + i, roomPosition.y + j] = 0;
                        continue;
                    }
                    map[roomPosition.x + i, roomPosition.y + j] = 1;
                }
        }
    }

    public int[,] GetLevelMap()
    {
        return map;
    }
}
