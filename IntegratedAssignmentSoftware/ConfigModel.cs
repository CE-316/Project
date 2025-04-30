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
    public class ConfigModel
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("compile")]
        public string Compile { get; set; }

        [JsonPropertyName("run")]
        public string Run { get; set; }
    }
}
