using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private Tile wall;
    [SerializeField] private Tile pass;

    void Start()
    {
        var pos = new Vector3Int(0, 0, 0);
        for (int y = -5; y < 5; ++y)
        {
            for (int x = -5; x < 5; ++x)
            {
                pos.x = x;
                pos.y = y;
                map.SetTile(pos, wall);
            }
        }
    }

    void Update()
    {
    }
}
