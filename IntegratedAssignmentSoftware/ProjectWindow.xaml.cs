using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// ProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private string projectName;
        private string assignmentOverview;
        private string configuration;
        private string submissionsDirectory;
        private List<TestCaseModel> testCases;
        public ProjectWindow()
        {
            projectName = "test";
            this.Name = projectName;
            InitializeComponent();
        }
    }
}
