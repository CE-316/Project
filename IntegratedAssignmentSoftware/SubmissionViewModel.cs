using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IntegratedAssignmentSoftware
{
    public class SubmissionViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<TestCaseResult> Results { get; set; }
        public string Code { get; set; }
        public int PassedCount => Results.Count(r => r.Passed);
        public int TotalCount => Results.Count;

        public int Points
        {
            get
            {
                int sum = 0;
                foreach (var r in Results)
                {
                    if (r.Passed)
                    {
                        sum += r.TestCase.Points;
                    }
                }
                return sum;
            }
        }
        public Brush DotBrush
        {
            get
            {
                if (PassedCount == 0) return Brushes.Red;
                if (PassedCount == TotalCount) return Brushes.Green;
                return Brushes.Gold;
            }
        }

        public SubmissionViewModel(string name, IEnumerable<TestCaseResult> results, string code)
        {
            Name = name;
            Code = code;
            Results = results.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
