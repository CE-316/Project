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
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("input")]
        public string Input { get; set; }

        [JsonPropertyName("output")]
        public string Output { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
}
