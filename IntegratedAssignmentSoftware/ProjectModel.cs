using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Collections.ObjectModel;

namespace IntegratedAssignmentSoftware
{
    public class ProjectModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("configuration")]
        public ConfigModel Configuration { get; set; }

        [JsonPropertyName("submissionsDirectory")]
        public string SubmissionsDirectory { get; set; }

        [JsonPropertyName("testCases")]
        public List<TestCaseModel> TestCases { get; set; } = new List<TestCaseModel>();
        [JsonPropertyName("savedSubmissionResults")]
        public ObservableCollection<SubmissionViewModel> SavedSubmissionResults { get; set; } = new ObservableCollection<SubmissionViewModel>();
    }
}