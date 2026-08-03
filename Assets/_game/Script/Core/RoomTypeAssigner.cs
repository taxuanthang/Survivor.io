using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Tách riêng để dễ unit test và dễ thay thế thuật toán gán loại.
/// Sau này bạn có thể viết RoomTypeAssigner_v2 dùng thuật toán khác
/// mà không cần sửa DungeonGenerator.
/// </summary>
public static class RoomTypeAssigner
{
    public static void AssignTypes(List<RoomData> rooms, System.Random rng)
    {
        if (rooms.Count == 0) return;

        // --- Phòng Start: chọn phòng có vị trí gần trung tâm nhất ---
        // (hoặc bạn có thể fix phòng đầu tiên trong Random Walk là Start)
        rooms[0].Type = RoomType.StartingRoom;

        // Tính khoảng cách BFS từ Start
        CalculateBFS(rooms[0]);

        // --- Phòng Boss: phòng XA Start nhất ---
        var farthestRoom = rooms
            .Where(r => r.Type != RoomType.StartingRoom)
            .OrderByDescending(r => r.DistanceFromStart)
            .FirstOrDefault();

        if (farthestRoom != null)
            farthestRoom.Type = RoomType.BossReadyRoom;

        // --- Phòng Treasure: phòng ở khoảng cách trung bình ---
        var remaining = rooms
            .Where(r => r.Type == RoomType.EnemyRoom)
            .ToList();

        if (remaining.Count > 0)
        {
            // Chọn phòng ở giữa quãng đường (khoảng cách median)
            remaining.Sort((a, b) => a.DistanceFromStart.CompareTo(b.DistanceFromStart));
            int midIndex = remaining.Count / 2;
            remaining[midIndex].Type = RoomType.TreasureRoom;

            // Nếu map đủ lớn, thêm phòng treasure thứ 2
            if (remaining.Count > 4)
            {
                remaining[remaining.Count / 4].Type = RoomType.TreasureRoom;
            }
        }

        // Reset distance về -1 (vì đã dùng xong, tránh nhầm lẫn sau này)
        foreach (var r in rooms) r.DistanceFromStart = -1;
    }

    private static void CalculateBFS(RoomData start)
    {
        Queue<RoomData> queue = new();
        start.DistanceFromStart = 0;
        queue.Enqueue(start);

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
}