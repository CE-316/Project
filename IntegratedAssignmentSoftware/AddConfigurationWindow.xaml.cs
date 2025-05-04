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
    /// AddConfigurationWindow.xaml etkileşim mantığı
    /// </summary>
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

    
    string configDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Configurations");
    if (!Directory.Exists(configDir))
        Directory.CreateDirectory(configDir);

    string configFilePath = System.IO.Path.Combine(configDir, $"{language}.json");

    if (File.Exists(configFilePath))
    {
        MessageBox.Show("A configuration with this name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return;
    }

    try
    {
        JsonLoader.SaveToFile(newConfig, configFilePath);
        MessageBox.Show("The configuration was added successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        this.Close(); 
    }
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred while saving the configuration: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
    }
}
