using IntegratedAssignmentSoftware.Services;
using System;
using System.IO;
using System.Windows;

namespace IntegratedAssignmentSoftware
{
    /// 
    /// Interaction logic for EditConfigurationWindow.xaml
    /// 
    public partial class EditConfigurationWindow : Window
    {
        private readonly string _originalFilePath;
        private ConfigModel _config;
        public EditConfigurationWindow(string filePath)
        {
            InitializeComponent();
            _originalFilePath = filePath;
            LoadConfiguration();
        }

        // Load JSON into the UI
        private void LoadConfiguration()
        {
            try
            {
                _config = JsonLoader.LoadFromFile<ConfigModel>(_originalFilePath);
                LanguageTextBox.Text = _config.Language;
                CompileTextBox.Text = _config.Compile;
                RunTextBox.Text = _config.Run;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        // Save changes back to the same file (or rename on language change)
        private void SaveConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            string newLanguage = LanguageTextBox.Text.Trim();
            string newCompile = CompileTextBox.Text.Trim();
            string newRun = RunTextBox.Text.Trim();

            if (string.IsNullOrEmpty(newLanguage))
            {
                MessageBox.Show("Configuration name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string configFolder = Path.GetDirectoryName(_originalFilePath);
            string newFileName = Path.Combine(configFolder, newLanguage + ".json");

            var newConfig = new ConfigModel
            {
                Language = newLanguage,
                Compile = newCompile,
                Run = newRun
            };

            try
            {
                JsonLoader.SaveToFile(newConfig, newFileName);

                if (!string.Equals(_originalFilePath, newFileName, StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(_originalFilePath);
                }

                MessageBox.Show("Configuration updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Static helper to launch editor from MainWindow with selected FileInfo
        public static bool? LaunchFor(FileInfo selectedFile)
        {
            if (selectedFile == null)
            {
                MessageBox.Show("Please select a configuration file to edit.", "No File Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            var editWin = new EditConfigurationWindow(selectedFile.FullName);
            return editWin.ShowDialog();
        }
    }
}
