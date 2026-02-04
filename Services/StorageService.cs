using System.Text.Json;
using System.Text.Json.Serialization;

public static class StorageService
{
    // Directories
    private static readonly string DataDir = Path.Combine(AppContext.BaseDirectory, "data");
    private static readonly string LogDir = Path.Combine(AppContext.BaseDirectory, "log");

    // File paths
    private static readonly string ExpensesPath = Path.Combine(DataDir, "expenses.json");
    private static readonly string LogPath = Path.Combine(LogDir, "log.json");

    static StorageService()
    {
        Directory.CreateDirectory(DataDir);
        Directory.CreateDirectory(LogDir);
    }

    public static List<Expense> LoadExpenses()
    {
        if (!File.Exists(ExpensesPath))
            return new List<Expense>();

        var json = File.ReadAllText(ExpensesPath);
        return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
    }

    public static void SaveExpenses(List<Expense> expenses)
    {
        Directory.CreateDirectory(DataDir);
        var json = JsonSerializer.Serialize(
            expenses,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() },
            }
        );

        File.WriteAllText(ExpensesPath, json);
    }

    public static List<Log> LoadLog()
    {
        if (!File.Exists(LogPath))
            return new List<Log>();

        var json = File.ReadAllText(LogPath);
        return JsonSerializer.Deserialize<List<Log>>(json) ?? new List<Log>();
    }

    public static void SaveLog(List<Log> logs)
    {
        Directory.CreateDirectory(LogDir);
        var json = JsonSerializer.Serialize(
            logs,
            new JsonSerializerOptions { WriteIndented = true }
        );

        File.WriteAllText(LogPath, json);
    }

    public static void AddLog(string message)
    {
        var logs = LoadLog();
        logs.Add(
            new Log
            {
                Id = Guid.NewGuid(),
                Message = message,
                Date = DateTime.Now,
            }
        );
        SaveLog(logs);
    }
}
