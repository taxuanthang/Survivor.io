using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Đại diện cho MỘT phòng trong dungeon.
/// Tách biệt hoàn toàn với Unity GameObject (thuần data).
/// </summary>
public class RoomData
{
    public Vector2Int GridPosition { get; set; }  // Vị trí trên grid
    public RoomType Type { get; set; }             // Loại phòng
    public GameObject Instance { get; set; }       // GameObject trong scene

    // Các phòng kề nhau (có cửa thông)
    public List<RoomData> ConnectedRooms { get; private set; } = new();

    // Dữ liệu cho luận văn
    public int DistanceFromStart { get; set; } = -1;
    public int EnemyCount { get; set; }

    public RoomData(Vector2Int gridPos)
    {
        GridPosition = gridPos;
        Type = RoomType.EnemyRoom;
    }

    public void Connect(RoomData other)
    {
        // Thêm vào list của chính mình
        if (!ConnectedRooms.Contains(other))
            ConnectedRooms.Add(other);

        // Thêm ngược lại vào list của phòng kia (Tạo kết nối 2 chiều)
        if (!other.ConnectedRooms.Contains(this))
            other.ConnectedRooms.Add(this);
    }

    /// <summary>
    /// Lấy hướng từ phòng này sang phòng kia (để biết đặt cửa ở đâu)
    /// </summary>
    public Vector2Int GetDirectionTo(RoomData other)
    {
        return other.GridPosition - GridPosition;
    }
}
