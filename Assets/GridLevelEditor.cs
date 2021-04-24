using UnityEngine;
using UnityEngine.Tilemaps;

public class GridLevelEditor : MonoBehaviour
{
    public Tilemap tilemap;
    void Start()
    {
        foreach(Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPosition = new Vector3Int(pos.x, pos.y, 0);
            if (tilemap.HasTile(pos) && tilemap.GetSprite(localPosition).name != "ground")
            {
                Debug.Log(tilemap.GetSprite(localPosition).name);
                GameObject tempObj = Instantiate(Resources.Load<GameObject>("Prefabs/" + tilemap.GetSprite(localPosition).name), tilemap.CellToWorld(localPosition), Quaternion.identity, transform);
            }
        }
        tilemap.ClearAllTiles();
    }
}
