using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinderManager : MonoBehaviour
{
    public Tilemap blockTileMap;
    public Vector3Int[,] walkableGrid;
    private BoundsInt _bounds;
    private AStarMap _aStarMap;
    
    private static PathFinderManager _instance;
    public static PathFinderManager Instance{
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<PathFinderManager>();
            }

            return _instance;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        blockTileMap.CompressBounds();
        _bounds = blockTileMap.cellBounds;
        
        CreateGrid();
        _aStarMap = new AStarMap(walkableGrid, _bounds.size.x, _bounds.size.y);
    }

    public void CreateGrid()
    {
        walkableGrid = new Vector3Int[_bounds.size.x, _bounds.size.y];
        for (int x = _bounds.xMin, i = 0; i < _bounds.size.x; x++, i++)
        {
            for (int y = _bounds.yMin, j = 0; j < _bounds.size.y; y++, j++)
            {
                if (blockTileMap.HasTile(new Vector3Int(x, y, 0)))
                {
                    walkableGrid[i,j] = new Vector3Int(x, y, 1);
                }
                else
                {
                    walkableGrid[i,j] = new Vector3Int(x, y, 0);
                }
            }
        }
    }

    public List<AStarSpot> CreatePath(Vector3 start, Vector3 end, int length)
    {
        Vector3Int startGridPos = blockTileMap.WorldToCell(start);
        Vector3Int endGridPos = blockTileMap.WorldToCell(end);
        
        return _aStarMap.CreatePath(walkableGrid, new Vector2Int(startGridPos.x, startGridPos.y),
            new Vector2Int(endGridPos.x, endGridPos.y), length);
    }

    public Vector3 GridPosToWorld(int x, int y)
    {
        return blockTileMap.CellToWorld(new Vector3Int(x, y, 0));
    }
}
