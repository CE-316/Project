using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// EditProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        public ProjectModel Project { get; }
        private string projectsDir;
        private string fileName;
        private string fullPath;
        private string originalPath;

        public EditProjectWindow(ProjectModel project)
        {
            InitializeComponent();

            projectsDir = Path.Combine(AppContext.BaseDirectory, "Projects");
            Directory.CreateDirectory(projectsDir);

            originalPath = Path.Combine(projectsDir, $"{project.Name}.json");

            Project = project;
            NameTextBox.Text = project.Name;
            SetRichText(DescriptionTextBox, project.Description);
            TestCaseListBox.ItemsSource = project.TestCases;
            LoadConfigFiles();
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

        public void SaveProjectButton_Click(object sender, RoutedEventArgs e)
        {
            Project.Name = NameTextBox.Text;
            Project.Description = GetRichText(DescriptionTextBox);
            if (ConfigurationListBox.SelectedItem != null)
            {
                Project.Configuration = (ConfigModel)ConfigurationListBox.SelectedItem;
            }

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
            this.DialogResult = true;
        }


        private ICollectionView configListView;
        private void LoadConfigFiles()
        {
            string configDir = Path.Combine(AppContext.BaseDirectory, "Configurations");
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            var configFiles = new DirectoryInfo(configDir).GetFiles("*.json");

            var configModels = configFiles
                .Select(fi =>
                {
                    try
                    {
                        return JsonLoader.LoadFromFile<ConfigModel>(fi.FullName);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(p => p != null)
                .ToList();

            configListView = CollectionViewSource.GetDefaultView(configModels);
            ConfigurationListBox.ItemsSource = configListView;
        }

        private void EditTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TestCaseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TestCaseListBox.SelectedItem is TestCaseModel selectedTest)
            {
                TestCasePreviewTextBlock.Text = selectedTest.Name + "\n Input: " + selectedTest.Input + "\n Output: " + selectedTest.Output;
            }
        }
    }
}
