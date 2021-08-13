using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField] Tilemap mapBaseLayer = null;
    [SerializeField] Vector3Int mapSize = Vector3Int.zero;
    [SerializeField] Sprite test = null;
    int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        mapBaseLayer.size = mapSize;
        map = new int[mapSize.x, mapSize.y];
        DrawTiles();
    }

    public void DrawTiles()
    {
        Tile tile = new Tile();
        Vector3Int pos = new Vector3Int(0, 0, 0);
        tile.sprite = test;
        for(int x = 0; x < mapSize.x; x++)
            for(int y = 0; y < mapSize.y; y++)
            {
                pos.x = x;
                pos.y = y;
                mapBaseLayer.SetTile(pos, tile);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
