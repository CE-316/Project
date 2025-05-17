using IntegratedAssignmentSoftware.Services;
using System;
using System.IO;
using System.Windows;

namespace IntegratedAssignmentSoftware
{

    public partial class AddConfigurationWindow : Window
    {
        public AddConfigurationWindow()
        {
            InitializeComponent();
        }

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

        public string javacPath;

        bool isjava = false;
        bool iscpp = false;
        bool ispython = false;
            private void GetJDK(object sender, RoutedEventArgs e)
        {
            isjava = true;
          
            javacPath = SelectFile("Select javac.exe", "Java Compiler (javac.exe)|javac.exe", @"C:\Program Files\Java");
            if (javacPath != null)
            {
                MessageBox.Show("Selected path: " + javacPath);
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }
            private void GetPython(object sender, RoutedEventArgs e)
        {
            ispython = true;
            //TODO: PYTHON PATHİ SEÇİLECEK

        }

            private void GetCpp(object sender, RoutedEventArgs e)
        {
            iscpp = true;
            //TODO: CPP PATHİ SEÇİLECEK
        }

        
        

        private void AddConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            string language = LanguageTextBox.Text.Trim();
            string compileCommand = CompileTextBox.Text.Trim();
            string runCommand = RunTextBox.Text.Trim();
            string compilerPath = javacPath;

            if (string.IsNullOrEmpty(language))
            {
                MessageBox.Show("Language/configuration name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newConfig = new ConfigModel
            {
                Language = language,
                Compile = compileCommand,
                Run = runCommand,
                //TODO: Path seçilecekh hangisini seçmek istediğini isjava ispython veya iscpp kullanarak anlayabilirsin 
            };

            string configDir = Path.Combine(AppContext.BaseDirectory, "Configurations");

            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            string configFilePath = Path.Combine(configDir, $"{language}.json");
            if (File.Exists(configFilePath))
            {
                MessageBox.Show("A configuration with this name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                JsonLoader.SaveToFile(newConfig, configFilePath);
                MessageBox.Show("The configuration was added successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
