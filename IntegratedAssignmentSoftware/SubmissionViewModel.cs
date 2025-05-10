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
        public string Name { get; }
        public IReadOnlyList<TestCaseResult> Results { get; }

        public int PassedCount => Results.Count(r => r.Passed);
        public int TotalCount => Results.Count;

        public Brush DotBrush
        {
            get
            {
                if (PassedCount == 0) return Brushes.Red;
                if (PassedCount == TotalCount) return Brushes.Green;
                return Brushes.Gold;
            }
        }

        public SubmissionViewModel(string name, IEnumerable<TestCaseResult> results)
        {
            Name = name;
            Results = results.ToList();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
