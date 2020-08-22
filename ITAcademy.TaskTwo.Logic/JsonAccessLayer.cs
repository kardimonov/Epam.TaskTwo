using System.IO;
using System.Text.Json;
using ITAcademy.TaskTwo.Data.Models;

namespace ITAcademy.TaskTwo.Logic
{
    public static class JsonAccessLayer
    {
        public static Settings ReadDataFromJson()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var jsonString = File.ReadAllText("settings.json");
            return JsonSerializer.Deserialize<Settings>(jsonString, options);
        }
    }
}