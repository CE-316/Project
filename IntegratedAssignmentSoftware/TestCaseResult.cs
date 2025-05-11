using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace IntegratedAssignmentSoftware
{
    public class TestCaseResult : INotifyPropertyChanged
    {
        public TestCaseModel TestCase { get; }
        public bool _passed;
        public bool Passed

        {
            get => _passed;
            set
            {
                if (_passed != value)
                {
                    _passed = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DotBrush));
                }
            }
        }
        public Brush DotBrush => Passed ? Brushes.Green : Brushes.Red;

        public TestCaseResult(TestCaseModel tc, bool passed)
        {
            TestCase = tc;
            _passed = passed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}
