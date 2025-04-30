using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadConfigFiles();
        }

        private void LoadConfigFiles()
        {
            var configDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Configurations");
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);
            var configFiles = Directory.GetFiles(configDir, "*.json").ToList();
            ConfigurationListBox.ItemsSource = configFiles;
        }

        private void ProjectSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddConfigurationButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConfigurationSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void EditConfigButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void DeleteConfigButton_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}