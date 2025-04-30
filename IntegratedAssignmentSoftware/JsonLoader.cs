using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.IO;

namespace IntegratedAssignmentSoftware
{
    public class JsonLoader
    {
        private static readonly JsonSerializerOptions _defaultOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };

        // Reads the JSON and deserializes it into the requested type.
        public static T LoadFromFile<T>(string filePath, JsonSerializerOptions? options = null)
        {
            var opts = options ?? _defaultOptions;
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(json, opts)
                   ?? throw new InvalidDataException($"Failed to deserialize {filePath} into {typeof(T)}");
        }

        // Serializes the object back to JSON and writes it to disk.
        public static void SaveToFile<T>(T data, string filePath, JsonSerializerOptions? options = null)
        {
            var opts = options ?? new JsonSerializerOptions(_defaultOptions) { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, opts);
            File.WriteAllText(filePath, json);
        }
    }
}
