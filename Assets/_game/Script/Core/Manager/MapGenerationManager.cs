using NaughtyAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerationManager : MonoBehaviour
{
    [Expandable]
    public DungeonConfig config;

    [SerializeField] private DataLogger dataLogger;

    // Lưu tất cả phòng đã sinh (key = vị trí grid)
    private Dictionary<Vector2Int, RoomData> _roomdataMap = new();
    private Dictionary<Room,Vector3> _roomMap = new();

    // RNG có seed (để tái tạo lại cùng 1 map)
    public  System.Random _rng;

    public Transform mapTransform;

    public List<RoomData> rooms = new List<RoomData>();
    public void Awake()
    {
        mapTransform = this.transform;
        config = Instantiate(config);
    }


    private void DeleteAllCurrentRoom()
    {
        foreach(Room room in _roomMap.Keys)
        {
            Destroy(room.gameObject);
        }

        _roomMap.Clear();
        _roomdataMap.Clear();
        rooms.Clear();
    }

    [Button]
    /// <summary>
    /// Hàm chính: Gọi hàm này để sinh toàn bộ dungeon
    /// </summary>
    public List<RoomData> Generate()
    {
        DeleteAllCurrentRoom();

        // --- BƯỚC 0: Khởi tạo ---
        int seed = config.seed == 0 ? Random.Range(int.MinValue, int.MaxValue) : config.seed;
        _rng = new System.Random(seed);
        _roomdataMap.Clear();

        dataLogger?.LogEvent("DUNGEON_START", $"Seed={seed}, TargetRooms={config.roomCount}");

        // --- BƯỚC 1: Random Walk để tạo layout ---
        rooms = GenerateLayout();

        // --- BƯỚC 2: Gán loại phòng (Start, Boss, Treasure) ---
        RoomTypeAssigner.AssignTypes(rooms, _rng);

        // --- BƯỚC 3: Instantiate prefab vào scene ---
        InstantiateRooms(rooms);

        // --- BƯỚC 4: Log dữ liệu ---
        LogDungeonData(rooms, seed);

        return rooms;
    }

    // ================================================================
    // BƯỚC 1: RANDOM WALK TRÊN GRID
    private List<RoomData> GenerateLayout()
    {
        //// Chọn điểm bắt đầu ở giữa grid
        //Vector2Int startPos = new Vector2Int(config.gridWidth / 2, config.gridHeight / 2);


        // Mới: Đặt tâm tại 0,0
        Vector2Int startPos = Vector2Int.zero;

        // Tạo phòng đầu tiên
        var startRoom = new RoomData(startPos);
        _roomdataMap[startPos] = startRoom;

        // 4 hướng: Lên, Xuống, Trái, Phải
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };

        Vector2Int currentPos = startPos;
        int ran = _rng.Next(4);
        Vector2Int currentDir = directions[ran];

        // Đi bộ cho đến khi đủ số phòng
        while (_roomdataMap.Count < config.roomCount)
        {
            // Quyết định: đi thẳng hay rẽ
            if (_rng.Next(100) > config.straightChance)
            {
                // Rẽ: chọn hướng vuông góc với hướng hiện tại
                Vector2Int[] perpendicularDirs = GetPerpendicularDirs(currentDir);
                currentDir = perpendicularDirs[_rng.Next(perpendicularDirs.Length)];
            }

            Vector2Int nextPos = currentPos + currentDir;

            // Kiểm tra biên
            if (!IsInBounds(nextPos))
            {
                // Nếu ra ngoài biên, chọn hướng khác
                currentDir = directions[_rng.Next(4)];
                continue;
            }

            // Nếu chưa có phòng ở vị trí này → tạo mới
            if (!_roomdataMap.ContainsKey(nextPos))
            {
                var newRoom = new RoomData(nextPos);
                _roomdataMap[nextPos] = newRoom;

                // Kết nối với phòng cũ
                _roomdataMap[currentPos].Connect(newRoom);
            }
            else
            {
                // Đã có phòng → vẫn kết nối (tạo vòng lặp/loop trong map)
                _roomdataMap[currentPos].Connect(_roomdataMap[nextPos]);
            }

            currentPos = nextPos;
        }
        PrintMapToConsole();
        return _roomdataMap.Values.ToList();
    }

    // ================================================================
    // BƯỚC 3: INSTANTIATE VÀO SCENE
    // ================================================================
    private void InstantiateRooms(List<RoomData> rooms)
    {
        // Lấy kích thước chuẩn 1 lần để tối ưu hiệu năng
        float cellSize = config.GetActualRoomSize().x;

        foreach (var roomData in rooms)
        {
            // Chọn prefab theo loại phòng
            List<GameObject> prefabList = new List<GameObject>();
            switch (roomData.Type)
            {
                case RoomType.StartingRoom: prefabList = config.prefabRoomStart; break;
                case RoomType.BossReadyRoom: prefabList = config.prefabRoomBoss; break;
                case RoomType.TreasureRoom: prefabList = config.prefabRoomTreasure; break;
                case RoomType.EnemyRoom: prefabList = config.prefabEnemyRooms; break;
            }

            int randomNum = _rng.Next(0, prefabList.Count);
            Debug.Log(roomData.Type);
            Debug.Log(randomNum);
            Debug.Log(prefabList.Count - 1);
            GameObject prefab = prefabList[randomNum];

            print(roomData.GridPosition.x +" "+ roomData.GridPosition.y);
            // Tính vị trí world từ grid position
            Vector3 worldPos = new Vector3(
                roomData.GridPosition.x * cellSize,
                roomData.GridPosition.y * cellSize,
                0
            );

            // Spawn
            Room room = Instantiate(prefab, worldPos, Quaternion.identity,mapTransform).GetComponent<Room>();
            _roomMap[room] = worldPos;
            room.gameObject.name = $"Room_{roomData.Type}_{roomData.GridPosition.x}_{roomData.GridPosition.y}";
            roomData.Instance = room.gameObject;
            room.data = roomData;


            //// Spawn cửa giữa các phòng
            //foreach (var connected in roomData.ConnectedRooms)
            //{
            //    // Chỉ spawn 1 lần (tránh trùng)
            //    if (connected.GridPosition.x > roomData.GridPosition.x ||
            //        connected.GridPosition.y > roomData.GridPosition.y)
            //    {
            //        SpawnDoor(roomData, connected);
            //    }
            //}
        }
    }

    private void SpawnDoor(RoomData roomA, RoomData roomB)
    {
        if (config.prefabDoor == null) return;

        float cellSize = config.GetActualRoomSize().x;

        // Cửa đặt ở giữa 2 phòng
        Vector3 posA = new Vector3(
            roomA.GridPosition.x * cellSize, 0,
            roomA.GridPosition.y * cellSize
        );
        Vector3 posB = new Vector3(
            roomB.GridPosition.x * cellSize, 0,
            roomB.GridPosition.y * cellSize
        );
        Vector3 doorPos = (posA + posB) / 2f;

        // Xoay cửa theo hướng
        Vector2Int dir = roomA.GetDirectionTo(roomB);
        float angle = 0f;
        if (dir == Vector2Int.up || dir == Vector2Int.down) angle = 90f;

        Instantiate(config.prefabDoor, doorPos, Quaternion.Euler(0, angle, 0),mapTransform);
    }

    // ================================================================
    // HELPER FUNCTIONS
    // ================================================================
    private bool IsInBounds(Vector2Int pos)
    {
        // Chia đôi để lấy bán kính (ví dụ grid 20x20 thì cho phép đi từ -10 đến 10)
        int halfW = config.gridWidth / 2;
        int halfH = config.gridHeight / 2;

        // Cho phép tọa độ âm
        return pos.x >= -halfW && pos.x < halfW &&
               pos.y >= -halfH && pos.y < halfH;
    }

    /// <summary>
    /// Trả về 2 hướng vuông góc với hướng hiện tại
    /// </summary>
    private Vector2Int[] GetPerpendicularDirs(Vector2Int dir)
    {
        if (dir == Vector2Int.up || dir == Vector2Int.down)
            return new[] { Vector2Int.left, Vector2Int.right };
        else
            return new[] { Vector2Int.up, Vector2Int.down };
    }

    // ================================================================
    // BƯỚC 4: LOG DỮ LIỆU CHO LUẬN VĂN
    // ================================================================
    private void LogDungeonData(List<RoomData> rooms, int seed)
    {
        if (dataLogger == null) return;

        // Tính khoảng cách từ Start bằng BFS
        var startRoom = rooms.FirstOrDefault(r => r.Type == RoomType.StartingRoom);
        if (startRoom != null)
            CalculateDistances(startRoom);

        // Log tổng quan
        dataLogger.LogEvent("DUNGEON_GENERATED",
            $"Seed={seed}, Rooms={rooms.Count}, " +
            $"Boss={rooms.Count(r => r.Type == RoomType.BossReadyRoom)}, " +
            $"Treasure={rooms.Count(r => r.Type == RoomType.TreasureRoom)}");

        // Log chi tiết từng phòng
        foreach (var room in rooms)
        {
            dataLogger.LogEvent("ROOM_DATA",
                $"Pos=({room.GridPosition.x},{room.GridPosition.y}), " +
                $"Type={room.Type}, " +
                $"Connections={room.ConnectedRooms.Count}, " +
                $"DistFromStart={room.DistanceFromStart}");
        }
    }

    /// <summary>
    /// BFS để tính khoảng cách từ phòng Start đến mọi phòng
    /// (Dùng để phân tích độ sâu của dungeon trong luận văn)
    /// </summary>
    private void CalculateDistances(RoomData startRoom)
    {
        Queue<RoomData> queue = new();
        startRoom.DistanceFromStart = 0;
        queue.Enqueue(startRoom);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var neighbor in current.ConnectedRooms)
            {
                if (neighbor.DistanceFromStart == -1)
                {
                    neighbor.DistanceFromStart = current.DistanceFromStart + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    // ================================================================
    // PUBLIC API: Dùng cho Gameplay
    // ================================================================

    /// <summary>
    /// Lấy phòng tại vị trí grid (dùng khi người chơi đi qua cửa)
    /// </summary>
    public RoomData GetRoomAt(Vector2Int gridPos)
    {
        _roomdataMap.TryGetValue(gridPos, out var room);
        return room;
    }

    /// <summary>
    /// Lấy tất cả phòng (dùng cho minimap)
    /// </summary>
    public List<RoomData> GetAllRooms() => _roomdataMap.Values.ToList();

    public void PrintMapToConsole()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        int halfW = config.gridWidth / 2;
        int halfH = config.gridHeight / 2;

        // In từ trên xuống dưới (y giảm dần)
        for (int y = halfH - 1; y >= -halfH; y--)
        {
            for (int x = -halfW; x < halfW; x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                if (_roomdataMap.ContainsKey(pos))
                {
                    sb.Append("█ ");
                }
                else
                {
                    sb.Append(". ");
                }
            }
            sb.AppendLine();
        }

        Debug.Log("=== MAP LAYOUT ===\n" + sb.ToString() + "==================");
    }

}
