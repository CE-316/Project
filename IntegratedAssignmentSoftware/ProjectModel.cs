using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;

namespace IntegratedAssignmentSoftware
{
    public class ProjectModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("submissionsDirectory")]
        public string SubmissionsDirectory { get; set; }
    }
}
