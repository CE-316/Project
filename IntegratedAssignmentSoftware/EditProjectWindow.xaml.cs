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
        private readonly string _originalFilePath;
        private ConfigModel _config;


        public EditProjectWindow(string filePath)
        {
            InitializeComponent();
            _originalFilePath = filePath;
            LoadConfigFiles();
        }
        public void SaveProjectButton_Click(object sender, RoutedEventArgs e)
        {

        string newProjectName = NameTextBox.Text.Trim();


            TextRange textRange = new TextRange(DescriptionTextBox.Document.ContentStart, DescriptionTextBox.Document.ContentEnd);
            string newProjectDescription = textRange.Text.Trim();

            if (string.IsNullOrEmpty(newProjectName))
            {
                MessageBox.Show("Proje adı boş olamaz.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string configFolder = Path.GetDirectoryName(_originalFilePath);
            string newFileName = Path.Combine(configFolder, newProjectName + ".json");
            var newProject = new ProjectModel
            {
                Name = newProjectName,
                Description = newProjectDescription,
                SubmissionsDirectory = string.Empty,
                Configuration = null,
                TestCases = new List<TestCaseModel>()
            };


            try
            {
                JsonLoader.SaveToFile(newProject, newFileName);

                if (!string.Equals(_originalFilePath, newFileName, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(_originalFilePath);
                }

                MessageBox.Show("Project updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private ICollectionView configListView;
        private void LoadConfigFiles()
        {
            string configDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Configurations");
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            var dir = new DirectoryInfo(configDir);

            var configFiles = dir.GetFiles("*.json").ToList();

            configListView = CollectionViewSource.GetDefaultView(configFiles);
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
    }
}
