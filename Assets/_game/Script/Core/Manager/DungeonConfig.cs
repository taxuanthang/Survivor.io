using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DungeonConfig", menuName = "Dungeon/Config")]
public class DungeonConfig : ScriptableObject
{
    [Header("Kích thước Grid")]
    [Tooltip("Bản đồ tối đa bao nhiêu phòng ngang/dọc")]
    public int gridWidth = 10;
    public int gridHeight = 10;

    [Header("Sinh phòng")]
    [Range(5, 50)]
    public int roomCount = 12;

    [Range(0, 100)]
    [Tooltip("% khả năng đi thẳng thay vì rẽ")]
    public int straightChance = 40;

    [Header("Tham chiếu kích thước (Tự động)")]
    [Tooltip("Kéo Tile hoặc Prefab phòng (có Tilemap) vào đây.")]
    public Object sizeReferenceObject;

    [Header("Kích thước phòng (World Units)")]
    [Tooltip("Kích thước của 1 Ô GRID (Cell Size). Giá trị này dùng để tính khoảng cách giữa các phòng.")]
    public float roomWorldSize = 10f;
    public Vector2 totalRoomSize = new Vector2(10f, 10f);

    [Header("Prefabs")]
    public List<GameObject> prefabEnemyRooms;
    public List<GameObject> prefabRoomStart;
    public List<GameObject> prefabRoomBoss;
    public List<GameObject> prefabRoomTreasure;
    public GameObject prefabDoor;

    [Header("Seed (0 = ngẫu nhiên)")]
    public int seed = 0;

    /// <summary>
    /// Hàm lấy kích thước thực tế của 1 ô grid (Cell Size).
    /// LUÔN dùng hàm này để tính toán vị trí spawn.
    /// </summary>

    /// <summary>
    /// Tính kích thước thực tế (World Units) của các tile ĐANG TỒN TẠI trong Tilemap.
    /// Không bị ảnh hưởng bởi các tile đã từng bị xóa.
    /// </summary>
    public Vector2 GetActualRoomSize()
    {

        if (sizeReferenceObject is GameObject roomPrefab)
        {


            if (roomPrefab == null) return new Vector2(roomWorldSize, roomWorldSize);

            Tilemap tilemap = roomPrefab.GetComponent<Tilemap>();
            if (tilemap == null) return new Vector2(roomWorldSize, roomWorldSize);

            // 1. Lấy TẤT CẢ các tile đang thực sự tồn tại (khác null)
            // Hàm này quét toàn bộ cellBounds nhưng chỉ trả về các ô có tile
            TileBase[] allTiles = tilemap.GetTilesBlock(tilemap.cellBounds);

            // 2. Lọc ra các ô thực sự có tile (khác null)
            List<Vector3Int> activeCellPositions = new List<Vector3Int>();
            BoundsInt bounds = tilemap.cellBounds;

            for (int x = bounds.xMin; x < bounds.xMax; x++)
            {
                for (int y = bounds.yMin; y < bounds.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    if (tilemap.GetTile(pos) != null) // Chỉ lấy ô đang có tile
                    {
                        activeCellPositions.Add(pos);
                    }
                }
            }

            // 3. Nếu không có tile nào -> trả về 0
            if (activeCellPositions.Count == 0)
            {
                return Vector2.zero;
            }

            // 4. Tính toán khung bao (Min/Max) của CÁC Ô ĐANG CÓ TILE
            int minX = int.MaxValue, maxX = int.MinValue;
            int minY = int.MaxValue, maxY = int.MinValue;

            foreach (var pos in activeCellPositions)
            {
                if (pos.x < minX) minX = pos.x;
                if (pos.x > maxX) maxX = pos.x;
                if (pos.y < minY) minY = pos.y;
                if (pos.y > maxY) maxY = pos.y;
            }

            // 5. Tính kích thước thực tế (số ô * kích thước 1 ô)
            // Lưu ý: (maxX - minX + 1) vì tính cả 2 đầu mút. Ví dụ: từ 0 đến 2 là 3 ô.
            float actualWidth = (maxX - minX + 1) * Mathf.Abs(tilemap.cellSize.x);
            float actualHeight = (maxY - minY + 1) * Mathf.Abs(tilemap.cellSize.y);

            totalRoomSize = new Vector2(actualWidth, actualHeight);

            return totalRoomSize;
        }
        return new Vector2(0f, 0f);
    }

}