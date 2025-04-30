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
    public class TestCaseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("inputFile")]
        public string InputFile { get; set; }

        [JsonPropertyName("outputFile")]
        public string OutputFile { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
}
