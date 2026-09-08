using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Tách riêng để dễ unit test và dễ thay thế thuật toán gán loại.
/// Sau này bạn có thể viết RoomTypeAssigner_v2 dùng thuật toán khác
/// mà không cần sửa DungeonGenerator.
/// </summary>
public static class RoomTypeAssigner
{
    public static void AssignTypes(List<RoomData> rooms,DungeonConfig dungeonConfig, System.Random rng, MapGenerationManager mapGenerator)
    {
        if (rooms.Count == 0) return;
        RoomData startRoom = rooms[0];
        int maxDistance = rooms.Max(r => r.DistanceFromStart);

        // 1. Phòng Start
        startRoom.Type = RoomType.StartingRoom;
        CalculateBFS(rooms[0]);

        // 2. Tìm phòng Boss (Phòng xa Start nhất)
        var bossRoom = rooms
            .Where(r => r.Type == RoomType.EnemyRoom)
            .OrderByDescending(r => r.DistanceFromStart)
            .FirstOrDefault();

        if (bossRoom == null) return; // Map quá nhỏ, không đủ phòng
        bossRoom.Type = RoomType.BossReadyRoom;
        int bossDistance = bossRoom.DistanceFromStart;

        // Tạo danh sách các phòng EnemyRoom còn lại để "chia bài"
        var remaining = rooms.Where(r => r.Type == RoomType.EnemyRoom).ToList();


        // ==========================================
        // 3. PHÒNG TREASURE (KHO BÁU) - Logic cũ nhưng thông minh hơn
        // ==========================================
        //for (int i = 0; i < dungeonConfig.numberOfTreasureRooms; i++)
        //{
            if (remaining.Count > 0)
            {
                remaining.Sort((a, b) => a.DistanceFromStart.CompareTo(b.DistanceFromStart));

                // Kho báu 1: Nằm ở chính giữa quãng đường còn lại
                int midIndex = remaining.Count / 2;
                remaining[midIndex].Type = RoomType.TreasureRoom;
                remaining.RemoveAt(midIndex); // Xóa khỏi list để tránh chọn trùng

                // Kho báu 2 (nếu map đủ lớn): Ưu tiên đặt ở Ngõ cụt
                if (remaining.Count > 3)
                {
                    var secondTreasure = remaining.FirstOrDefault(r => r.ConnectedRooms.Count == 1);
                    if (secondTreasure == null)
                    {
                        // Nếu không còn ngõ cụt, lấy phòng ở vị trí 1/4
                        secondTreasure = remaining[remaining.Count / 4];
                    }
                    secondTreasure.Type = RoomType.TreasureRoom;
                }


            }
        //}
        // ==========================================
        // 4. CẤY CỤM ELITE + KHO BÁU (Chuỗi thử thách)
        // ==========================================
        // Mục tiêu: Tìm phòng có khoảng cách gần bằng 70% quãng đường tới Boss

        for (int i = 0; i < dungeonConfig.numberOfEliteRooms; i++)
        {
            BuildSingleSpecialRoomWithAnotherRommConnected(rooms, 0.6f, RoomType.EliteRoom, RoomType.TreasureRoom, mapGenerator, rng);
        }
        // ==========================================
        // 5. Cấy PHÒNG SHOP (CỬA HÀNG) - Chuẩn bị trước Boss
        // ==========================================
        // Mục tiêu: Lấy phòng xa nhất trong các phòng còn lại (tầm 70-80% quãng đường)

        for (int i = 0; i < dungeonConfig.numberOfShopRooms; i++)
        {
            BuildSingleSpecialRoom(rooms, 0.75f, RoomType.ShopRoom, mapGenerator, rng);
        }

        // ==========================================
        // 6. CẤY PHÒNG GAMBLING (Nhánh cụt rủi ro giữa map)
        // ==========================================
        // Tìm một phòng bất kỳ ở khoảng 40% quãng đường
        for (int i = 0; i < dungeonConfig.numberOfGamblingRooms; i++)
        {
            BuildSingleSpecialRoom(rooms, 0.4f, RoomType.GamblingRoom, mapGenerator, rng);
        }

        // Reset distance
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

    private static void ResetDistances(List<RoomData> rooms)
    {
        foreach (var r in rooms) r.DistanceFromStart = -1;
    }

    private static RoomData BuildSingleSpecialRoom(List<RoomData> rooms,float percentDistanceOfSpecialRoomTakePlace, RoomType specialRoomType , MapGenerationManager mapGenerator, System.Random rng)
    {
        RoomData startRoom = rooms[0];
        var remaining = rooms.Where(r => r.Type == RoomType.EnemyRoom).ToList();
        var deadEnds = remaining.Where(r => r.ConnectedRooms.Count == 1).ToList();
        int maxDistance = rooms.Max(r => r.DistanceFromStart);


        RoomData specialRoom = null;


        if (deadEnds.Count > 0)
        {
            // Ưu tiên chọn ngõ cụt nằm ở khoảng 40% quãng đường (giữa map)
            int specialRoomTargetDist = (int)(maxDistance * percentDistanceOfSpecialRoomTakePlace);
            specialRoom = deadEnds.OrderBy(r => Math.Abs(r.DistanceFromStart - specialRoomTargetDist)).FirstOrDefault();
        }
        else
        {
            // chọn ra các phòng có nhỏ hơn 4 kết nối và nằm ở khoảng phần trăm quãng đường
            var preSpacialRoom = rooms
                .Where(r => r.Type == RoomType.EnemyRoom && r.DistanceFromStart > maxDistance * percentDistanceOfSpecialRoomTakePlace&&r.ConnectedRooms.Count <4)
                .OrderByDescending(r => r.DistanceFromStart)
                .FirstOrDefault();

            if (preSpacialRoom != null)
            {
                // Xây phòng Shop như một nhánh cụt rẽ ra từ phòng này
                specialRoom = mapGenerator.TryInjectRoom(preSpacialRoom, specialRoomType, rng);
            }
            else
            {
                
                int gambleTargetDist = rng.Next((int)(maxDistance * percentDistanceOfSpecialRoomTakePlace-0.1), (int)(maxDistance * percentDistanceOfSpecialRoomTakePlace+0.1) + 1);
                specialRoom = remaining.OrderBy(r => Math.Abs(r.DistanceFromStart - gambleTargetDist)).FirstOrDefault();
            }
        }

        if (specialRoom != null)
        {
            specialRoom.Type = specialRoomType;
            remaining.Remove(specialRoom);
        }


        // Tính lại BFS vì ta vừa xây thêm phòng, khoảng cách có thể thay đổi
        ResetDistances(rooms);
        CalculateBFS(startRoom);
        maxDistance = rooms.Max(r => r.DistanceFromStart);

        return specialRoom;
    }


    private static RoomData BuildSingleSpecialRoomWithAnotherRommConnected(List<RoomData> rooms, float percentDistanceOfSpecialRoomTakePlace, RoomType specialRoomType, RoomType connectRoomType, MapGenerationManager mapGenerator, System.Random rng)
    {
        RoomData startRoom = rooms[0];
        var remaining = rooms.Where(r => r.Type == RoomType.EnemyRoom).ToList();
        var deadEnds = remaining.Where(r => r.ConnectedRooms.Count == 1).ToList();
        int maxDistance = rooms.Max(r => r.DistanceFromStart);

        RoomData eliteRoom= BuildSingleSpecialRoom(rooms, percentDistanceOfSpecialRoomTakePlace, specialRoomType, mapGenerator, rng);
        Debug.Log(eliteRoom.GridPosition + " - " + eliteRoom.DistanceFromStart);

        // Xây tiếp phòng Kho báu nối tiếp phòng Elite (Chỉ qua được Elite mới lấy được đồ)
        RoomData treasureRoom = mapGenerator.TryInjectRoom(eliteRoom, connectRoomType, rng);
        // Tính lại BFS vì ta vừa xây thêm phòng, khoảng cách có thể thay đổi
        ResetDistances(rooms);
        CalculateBFS(startRoom);
        maxDistance = rooms.Max(r => r.DistanceFromStart);

        return eliteRoom;
    }
}