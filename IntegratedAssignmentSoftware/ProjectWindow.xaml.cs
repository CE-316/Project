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
        private string fileName;
        private string fullPath;
        private string projectsDir;
        private string originalPath;
        private ObservableCollection<ConfigModel> _configs = new ObservableCollection<ConfigModel>();
        public ObservableCollection<SubmissionViewModel> Submissions { get; } = new ObservableCollection<SubmissionViewModel>();
        private ProjectModel Project { get; set; }
        public ProjectWindow(ProjectModel project)
        {
            Project = project;
            InitializeComponent();
            NameTextBox.Text = project.Name;
            SetRichText(DescriptionTextBox, project.Description);
            if (!string.IsNullOrEmpty(project.SubmissionsDirectory))
            {
                LoadSubmissions(project.SubmissionsDirectory);
            }
            projectsDir = Path.Combine(AppContext.BaseDirectory, "Projects");
            Directory.CreateDirectory(projectsDir);
            originalPath = Path.Combine(projectsDir, $"{project.Name}.json");

            ConfigurationComboBox.ItemsSource = _configs;
            LoadConfigurations();
            if (Project.Configuration == null)
            {
                Project.Configuration = new ConfigModel { Language = null };
            }
            if (Project.Configuration.Language != null)
            {
                var match = _configs.FirstOrDefault(c => c.Language == Project.Configuration.Language);
                if (match != null)
                    ConfigurationComboBox.SelectedItem = match;
            }
            SubmissionsList.ItemsSource = Submissions;
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();

            if (dlg.ShowDialog() != true)
                return;
            var selectedPath = dlg.FolderName;
            LoadSubmissions(selectedPath);
            Project.SubmissionsDirectory = selectedPath;
        }
        private void LoadSubmissions(string rootFolder)
        {
            Submissions.Clear();

            var testCases = Project.TestCases;
            int totalTests = testCases.Count;

            Directory.CreateDirectory(rootFolder);
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
        string GetRichText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
        void SetRichText(RichTextBox rtb, string rtfText)
        {
            var range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfText));
            range.Load(stream, DataFormats.Text);
        }
        private void ConfigurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfigurationComboBox.SelectedItem is ConfigModel selected)
            {
                Project.Configuration = selected;
            }
        }
        private void LoadConfigurations()
        {
            string configDir = Path.Combine(AppContext.BaseDirectory, "Configurations");
            Directory.CreateDirectory(configDir);

            _configs.Clear();
            foreach (var fi in new DirectoryInfo(configDir).GetFiles("*.json"))
            {
                try
                {
                    var cfg = JsonLoader.LoadFromFile<ConfigModel>(fi.FullName);
                    _configs.Add(cfg);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to load {fi.Name}: {ex.Message}");
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(GetRichText(DescriptionTextBox)))
            {
                MessageBox.Show("Make sure the project has a name and description.");
                return;
            }
            Project.Name = NameTextBox.Text;
            Project.Description = GetRichText(DescriptionTextBox);

            fileName = $"{Project.Name}.json";
            fullPath = Path.Combine(projectsDir, fileName);

            JsonLoader.SaveToFile(Project, fullPath);

            if (!string.Equals(originalPath, fullPath, StringComparison.OrdinalIgnoreCase) && File.Exists(originalPath))
            {
                try
                {
                    File.Delete(originalPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Warning: could not delete old file:\n{ex.Message}",
                                    "Cleanup Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            MessageBox.Show("Project saved successfully.");
        }
    } 
}
