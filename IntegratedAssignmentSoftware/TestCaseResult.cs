using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAssignmentSoftware
{
    public class TestCaseResult
    {
        public TestCaseModel Case { get; }
        public bool Passed { get; }

        public TestCaseResult(TestCaseModel c, bool passed)
        {
            Case = c;
            Passed = passed;
        }
    }
}
