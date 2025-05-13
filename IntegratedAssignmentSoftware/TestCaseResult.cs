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
        public TestCaseModel TestCase { get; set; }

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
                    OnPropertyChanged(nameof(DotColorHex));
                }
            }
        }
        public string DotColorHex => Passed ? "#008000" : "#FF0000";

        public TestCaseResult() { }
        public TestCaseResult(TestCaseModel testCase, bool passed)
        {
            TestCase = testCase;
            Passed = passed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? n = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
    }
}