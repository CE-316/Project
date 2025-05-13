using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedAssignmentSoftware
{
    public class SubmissionData
    {
        public String Name {  get; set; }
        public String Code { get; set; }
        public List<TestCaseResult> Results { get; set; }
    }
}
