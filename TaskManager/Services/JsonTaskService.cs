using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TaskManager1.Models;

namespace TaskManager1.Services
{
    public static class JsonTaskService
    {
        private static readonly string FilePath =
            Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "tasks.json");

        public static void Save(IEnumerable<TaskItem> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(tasks, options));
        }

        public static List<TaskItem> Load()
        {
            if (!File.Exists(FilePath)) return new List<TaskItem>();
            try
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch
            {
                return new List<TaskItem>();
            }
        }
    }
}
