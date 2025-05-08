using IntegratedAssignmentSoftware.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Path = System.IO.Path;

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// ProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class ProjectWindow : Window
    {
        public ObservableCollection<SubmissionViewModel> Submissions { get; } = new ObservableCollection<SubmissionViewModel>();
        private ProjectModel Project { get; set; }
        public ProjectWindow(ProjectModel project)
        {
            this.Project = project;
            InitializeComponent();
            SubmissionsList.ItemsSource = Submissions;
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();

            if (dlg.ShowDialog() != true)
                return;
            var selectedPath = dlg.FolderName;
            LoadSubmissions(selectedPath);
        }
        private void LoadSubmissions(string rootFolder)
        {
            Submissions.Clear();

            var testCases = Project.TestCases;
            int totalTests = testCases.Count;

            foreach (var dir in Directory.GetDirectories(rootFolder))
            {
                string name = Path.GetFileName(dir);

                int passed = EvaluateSubmission(dir, testCases);

                Submissions.Add(new SubmissionViewModel(name, passed, totalTests));
            }
        }

        private int EvaluateSubmission(string submissionFolder, IList<TestCaseModel> tests)
        {
            // TODO: Compile & run each test, return how many passed.
            // For now, return a random number for demo:
            return new Random(submissionFolder.GetHashCode()).Next(0, tests.Count + 1);
        }

        private void SaveResults_Click(object sender, RoutedEventArgs e)
        {

        }
    } 
}
