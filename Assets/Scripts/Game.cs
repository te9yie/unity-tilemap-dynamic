using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    [SerializeField] private Tilemap map = default;
    [SerializeField] private Tile wall = default;
    [SerializeField] private Tile pass = default;
    [SerializeField] private GameObject playerObject = default;
    [SerializeField] private GameObject cameraObject = default;

    private Vector3Int playerPos = new Vector3Int();

    void Start()
    {
        Assert.IsNotNull(map);
        Assert.IsNotNull(wall);
        Assert.IsNotNull(pass);
        Assert.IsNotNull(playerObject);
        Assert.IsNotNull(cameraObject);
        {
            var minX = -3;
            var maxX = 3;
            var minY = -3;
            var maxY = 3;
            var pos = new Vector3Int(0, 0, 0);
            for (int y = minY; y <= maxY; ++y)
            {
                for (int x = minX; x <= maxX; ++x)
                {
                    pos.x = x;
                    pos.y = y;
                    var tile = (y == minY || y == maxY || x == minX || x == maxX) ? wall : pass;
                    map.SetTile(pos, tile);
                }
            }
            pos.x = -1;
            pos.y = -1;
            map.SetTile(pos, wall);
        }
        {
            var loc = map.GetCellCenterLocal(playerPos);
            playerObject.transform.position = loc;
            cameraObject.transform.position = loc;
        }
    }

    void Update()
    {
        var nextPos = playerPos;
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.B))
        {
            --nextPos.x;
        }
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.N))
        {
            ++nextPos.x;
        }
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N))
        {
            --nextPos.y;
        }
        if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.U))
        {
            ++nextPos.y;
        }
        if (nextPos != playerPos) {
            var tile = map.GetTile(nextPos);
            if (tile != null && tile == pass)
            {
                playerPos = nextPos;
                var loc = map.GetCellCenterLocal(playerPos);
                playerObject.transform.position = loc;
            }
        }
        {
            var playerPos = playerObject.transform.position;
            var cameraPos = cameraObject.transform.position;
            var pos = (playerPos - cameraPos) * 0.1f + cameraPos;
            cameraObject.transform.position = pos;
        }
    }
}
