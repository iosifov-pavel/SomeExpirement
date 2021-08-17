using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{
    public static Floor Instance = null;
    public Vector2Int LevelSize { get; set; }
    [SerializeField] List<Area> allAreas= new List<Area>();
    [SerializeField] List<Area> areasCapableForRoom = new List<Area>();
    [SerializeField] List<Room> rooms= new List<Room>();
    [SerializeField] List<Coridor> coridors = new List<Coridor>();
    int[,] map;

    public void InitFloor(Vector3Int mapSize)
    {
        Instance = this;
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
            Room room = area.CreateRoom();
            room.SetID(areasCapableForRoom.IndexOf(area));
            rooms.Add(room);
        }

    }

    public void GenerateCoridors()
    {
        foreach (Room room in rooms)
        {
            room.Neighbors();
        }
        List<Coridor> newCoridors = new List<Coridor>();
        foreach(Coridor cor in coridors)
        {
            bool alreadyHas = false;
            foreach(Coridor cor2 in newCoridors)
            {
                if (cor.Equality(cor2)) { alreadyHas = true; break; }
            }
            if (alreadyHas) {continue;}
            newCoridors.Add(cor);
            coridors = new List<Coridor>(newCoridors);
        }
    }

    public void CompleteLevelMap()
    {
        foreach (Room room in rooms)
        {
            Vector2Int roomPosition = room.GetStartPosition();
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

    public List<Room> GetRooms()
    {
        return rooms;
    }

    public List<Coridor> GetCoridors()
    {
        return coridors;
    }
}
