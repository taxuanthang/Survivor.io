using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


public class DataLogger : MonoBehaviour
{
    [SerializeField] private string outputFolder = "Logs";

    private List<string> _logEntries = new();
    private string _sessionID;

    private void Awake()
    {
        _sessionID = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "..", outputFolder));
    }

    public void LogEvent(string eventType, string data)
    {
        float time = Time.timeSinceLevelLoad;
        string entry = $"{time:F3},{_sessionID},{eventType},{data}";
        _logEntries.Add(entry);

        Debug.Log($"[LOG] {eventType}: {data}");
    }

    /// <summary>
    /// Ghi toàn bộ log ra file CSV. Gọi khi kết thúc dungeon / người chơi chết.
    /// </summary>
    public void SaveToFile()
    {
        string path = Path.Combine(
            Application.dataPath, "..", outputFolder,
            $"dungeon_log_{_sessionID}.csv"
        );

        StringBuilder sb = new();
        sb.AppendLine("Timestamp,SessionID,EventType,Data");
        foreach (var entry in _logEntries)
        {
            sb.AppendLine(entry);
        }

        File.WriteAllText(path, sb.ToString());
        Debug.Log($"[DataLogger] Saved to: {path}");
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }
}