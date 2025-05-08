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
        public int Passed { get; }
        public int Total { get; }

        public Brush DotBrush
        {
            get
            {
                if (Passed == 0) return Brushes.Red;
                if (Passed == Total) return Brushes.Green;
                return Brushes.Gold;
            }
        }

        public SubmissionViewModel(string name, int passed, int total)
        {
            Name = name;
            Passed = passed;
            Total = total;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
