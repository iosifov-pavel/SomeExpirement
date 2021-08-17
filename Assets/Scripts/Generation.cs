using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField] Tilemap mapBaseLayer = null;
    [SerializeField] Vector2Int mapSize = Vector2Int.zero;
    [SerializeField] Sprite test = null, wall = null;
    [SerializeField] int seed = 0;
    [SerializeField] LevelMap currentLevel = null;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        mapBaseLayer.size = new Vector3Int(mapSize.x,mapSize.y,0);
        Random.InitState(seed);
        GenerateLevel();
        DrawTiles();
    }

    public void DrawTiles()
    {
        int[,] levelMap = currentLevel.GetMap();
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        Vector3Int pos = new Vector3Int(0, 0, 0);
        tile.sprite = test;
        for(int x = 0; x < mapSize.x; x++)
            for(int y = 0; y < mapSize.y; y++)
            {
                switch (levelMap[x,y])
                {
                    case -1:
                        tile.sprite = wall;
                        break;
                    case 0:
                        continue;
                    case 1:
                        tile.sprite = test;
                        break;
                } 
                
                pos.x = x;
                pos.y = y;
                mapBaseLayer.SetTile(pos, tile);
            }
    }

    void GenerateLevel()
    {
        currentLevel = new LevelMap();
        currentLevel.InitMap(mapSize);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int[,] levelMap = currentLevel.GetMap();
        for (int x = 0; x < mapSize.x; x++)
            for (int y = 0; y < mapSize.y; y++)
            {
                if (levelMap[x, y] == 0)
                {
                    Vector2 position = new Vector2(x+.5f, y+.5f);
                    GameObject player = Instantiate(this.player, position, Quaternion.identity);
                    return;
                }
            }
    }
}
