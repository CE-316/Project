using IntegratedAssignmentSoftware.Services;
using System.ComponentModel;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace IntegratedAssignmentSoftware
{
    public partial class MainWindow : Window
    {
        private ICollectionView configListView;
        private ICollectionView projectListView;
        public MainWindow()
        {
            InitializeComponent();
            LoadConfigFiles();
            LoadProjectFiles();
        }

        private void LoadConfigFiles()
        {
            string configDir = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Configurations");
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            var configFiles = new DirectoryInfo(configDir)
                              .GetFiles("*.json").ToList();

            configListView = CollectionViewSource.GetDefaultView(configFiles);
            ConfigurationListBox.ItemsSource = configListView;
        }

        private void LoadProjectFiles()
        {
            string projectDir = Path.Combine(AppContext.BaseDirectory, "Projects");
            if (!Directory.Exists(projectDir))
                Directory.CreateDirectory(projectDir);

            var projectFiles = new DirectoryInfo(projectDir)
                               .GetFiles("*.json").ToList();

            projectListView = CollectionViewSource.GetDefaultView(projectFiles);
            ProjectListBox.ItemsSource = projectListView;
        }

        private void ProjectSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var term = ProjectSearchBox.Text.Trim().ToLowerInvariant();
            projectListView.Filter = string.IsNullOrEmpty(term)
                ? null
                : new Predicate<object>(o => ((FileInfo)o).Name.ToLowerInvariant().Contains(term));
            projectListView.Refresh();
        }

        private void ConfigurationSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var term = ConfigurationSearchBox.Text.Trim().ToLowerInvariant();
            configListView.Filter = string.IsNullOrEmpty(term)
                ? null
                : new Predicate<object>(o => ((FileInfo)o).Name.ToLowerInvariant().Contains(term));
            configListView.Refresh();
        }

        private void AddConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var addConfigWin = new AddConfigurationWindow();
            addConfigWin.ShowDialog();
            LoadConfigFiles();
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var addProjWin = new AddProjectWindow();
            addProjWin.ShowDialog();
            LoadProjectFiles();
        }

        private void EditConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedConfig = ConfigurationListBox.SelectedItem as FileInfo;
            bool? result = EditConfigurationWindow.LaunchFor(selectedConfig);
            if (result == true)
            {
                LoadConfigFiles();
            }
        }

        private void DeleteConfigButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedConfig = ConfigurationListBox.SelectedItem as FileInfo;
            if (selectedConfig == null)
            {
                MessageBox.Show("Please select a configuration file to delete.", "No File Selected",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                File.Delete(selectedConfig.FullName);
                LoadConfigFiles();
                MessageBox.Show("Configuration deleted successfully.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting configuration: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProj = ProjectListBox.SelectedItem as FileInfo;
            if (selectedProj == null)
            {
                MessageBox.Show("Please select a project to open.", "No Project Selected",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
            string chosen = "Java";
            if (chosen.Equals("Java"))
            {
                TestRunJava_Click();
            }
            if (chosen.Equals("Python"))
            {
                TestRunPython_Click();
            }
            if (chosen.Equals("C++"))
            {

            }
        }
    
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open project: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var editProjWin = new EditProjectWindow();
            editProjWin.ShowDialog();
            LoadProjectFiles();
        }

        private void DeleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedProj = ProjectListBox.SelectedItem as FileInfo;
            if (selectedProj == null)
            {
                MessageBox.Show("Please select a project to delete.", "No Project Selected",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                File.Delete(selectedProj.FullName);
                LoadProjectFiles();
                MessageBox.Show("Project deleted successfully.", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting project: {ex.Message}", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TestRunJava_Click()
        {
            string javacPath = SelectFile("Select javac.exe", "Java Compiler (javac.exe)|javac.exe", @"C:\Program Files\Java");
            if (javacPath != null)
            {
                MessageBox.Show("Selected path: " + javacPath);
                string javaFilePath = SelectFile("select file to be compiled", "Java Files (*.java)|*.java", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                if (javaFilePath != null)
                {
                    MessageBox.Show("Selected path: " + javaFilePath);
                    string className = System.IO.Path.GetFileNameWithoutExtension(javaFilePath);
                    string workingDirectory = System.IO.Path.GetDirectoryName(javaFilePath);

                    bool success = CompilerService.CompileJava(javaFilePath, javacPath, out string errors);

                    if (success)
                    {
                        string javaArgs = "hello dunya";
                        string output = CompilerService.RunJava(workingDirectory, className, javaArgs);
                        MessageBox.Show("Java Output:\n" + output);
                    }
                    else
                    {
                        MessageBox.Show("Java Compilation Fail, Perhaps check the configuration:\n" + errors);
                    }
                }
                else
                {
                    MessageBox.Show("No file selected.");
                }
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }
        //phyton
        private void TestRunPython_Click()
        {
            string pythonPath = "python"; //path
            string scriptPath = SelectFile("select file to be compiled", "Python Files (*.py)|*.py", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            string pythonArgs = "merhabadunya";

            string output = CompilerService.RunPython(pythonPath, scriptPath, pythonArgs);
            MessageBox.Show("Python Output:\n" + output);
        }
        //c++
        /* 
        private void TestCompileCpp_Click()
{
    string cppSource = SelectFile("Select C++ Source File", "C++ Files (*.cpp)|*.cpp", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
    if (cppSource == null) { MessageBox.Show("No file selected."); return; }

    string exeOutput = System.IO.Path.ChangeExtension(cppSource, ".exe");
    string gppPath = "g++"; // or full path to g++

    bool success = CompilerService.CompileCpp(cppSource, exeOutput, gppPath, out string errors);

    if (success)
    {
        string cppArgs = Microsoft.VisualBasic.Interaction.InputBox("Enter arguments for C++ executable:", "C++ Arguments");
        string output = CompilerService.RunCpp(exeOutput, cppArgs);
        MessageBox.Show("C++ Output:\n" + output);
    }
    else
    {
        MessageBox.Show("C++ Compilation Failed:\n" + errors);
    }
}

        */
        private string SelectFile(string title, string filter, string initialDirectory = "")
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = title,
                Filter = filter,
                InitialDirectory = string.IsNullOrWhiteSpace(initialDirectory)
                                    ? Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
                                    : initialDirectory
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }
    }
}