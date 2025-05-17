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
        private string configsDir;
        private string fileName;
        private string fullPath;
        private string originalPath;
        private ConfigModel Config;
        public EditConfigurationWindow(ConfigModel config)
        {
            InitializeComponent();

            configsDir = Path.Combine(AppContext.BaseDirectory, "Configurations");
            Directory.CreateDirectory(configsDir);

            originalPath = Path.Combine(configsDir, $"{config.Language}.json");

            Config = config;
            LanguageTextBox.Text = Config.Language;
            CompileTextBox.Text = Config.Compile;
            RunTextBox.Text = Config.Run;
        }
        private void SaveConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LanguageTextBox.Text) || string.IsNullOrEmpty(RunTextBox.Text))
            {
                MessageBox.Show("Name or Run Command can't be empty");
                return;
            }

            Config.Language = LanguageTextBox.Text;
            Config.Compile = CompileTextBox.Text;
            Config.Run = RunTextBox.Text;

            fileName = $"{Config.Language}.json";
            fullPath = Path.Combine(configsDir, fileName);

            JsonLoader.SaveToFile(Config, fullPath);

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
    }
}
