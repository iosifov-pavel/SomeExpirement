using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField] Tilemap mapBaseLayer = null;
    [SerializeField] Vector3Int mapSize = Vector3Int.zero;
    [SerializeField] Sprite test = null;
    [SerializeField] int seed = 0;
    [SerializeField] List<Floor> levels = new List<Floor>();
    Floor currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        mapBaseLayer.size = mapSize;
        Random.InitState(seed);
        GenerateLevel();
        DrawTiles();
    }

    public void DrawTiles()
    {
        int[,] levelMap = currentLevel.GetLevelMap();
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        Vector3Int pos = new Vector3Int(0, 0, 0);
        tile.sprite = test;
        for(int x = 0; x < mapSize.x; x++)
            for(int y = 0; y < mapSize.y; y++)
            {
                if (levelMap[x, y] == 0) continue;
                pos.x = x;
                pos.y = y;
                mapBaseLayer.SetTile(pos, tile);
            }
    }

    void GenerateLevel()
    {
        Floor levelToGenerate = new Floor();
        levelToGenerate.InitFloor(mapSize);
        levelToGenerate.GenerateAreas();
        levelToGenerate.GenerateRooms();
        levelToGenerate.GenerateCoridors();
        levelToGenerate.CompleteLevelMap();
        currentLevel = levelToGenerate;
        levels.Add(levelToGenerate);
    }

}
