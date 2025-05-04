using System;
using System.Collections.Generic;
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

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// AddProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class AddProjectWindow : Window
    {
        public AddProjectWindow()
        {
            InitializeComponent();
        }

       private void AddProjectButton_Click(object sender, RoutedEventArgs e)
{
    
    string projectName = NameTextBox.Text.Trim();

    
    TextRange textRange = new TextRange(DescriptionTextBox.Document.ContentStart, DescriptionTextBox.Document.ContentEnd);
    string projectDescription = textRange.Text.Trim();

    if (string.IsNullOrEmpty(projectName))
    {
        MessageBox.Show("Proje adı boş olamaz.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    
    var newProject = new ProjectModel
    {
        Name = projectName,
        Description = projectDescription,
        SubmissionsDirectory = string.Empty, 
        Configuration = null, 
        TestCases = new List<TestCaseModel>() 
    };

    
    string projectDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Projects");
    if (!Directory.Exists(projectDir))
        Directory.CreateDirectory(projectDir);

    string projectFilePath = System.IO.Path.Combine(projectDir, $"{projectName}.json");

    if (File.Exists(projectFilePath))
    {
        MessageBox.Show("A project with this name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    try
    {
        JsonLoader.SaveToFile(newProject, projectFilePath);
        MessageBox.Show("Project added successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close(); 
    }
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred while saving the project: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}
