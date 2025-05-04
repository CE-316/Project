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

        private void AddConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            string language = LanguageTextBox.Text.Trim();
            string compileCommand = CompileTextBox.Text.Trim();
            string runCommand = RunTextBox.Text.Trim();

            if (string.IsNullOrEmpty(language))
            {
                MessageBox.Show("Language/configuration name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newConfig = new ConfigModel
            {
                Language = language,
                Compile = compileCommand,
                Run = runCommand
            };

            string configDir = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Configurations");
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
