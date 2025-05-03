using IntegratedAssignmentSoftware.Services;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
            LoadProjectFiles();
        }
        

        private void LoadConfigFiles()
        {
            string configDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Configurations");
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            var dir = new DirectoryInfo(configDir);

            var configFiles = dir.GetFiles("*.json").ToList();
            ConfigurationListBox.ItemsSource = configFiles;
        }
        private void LoadProjectFiles()
        {
            string projectDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Projects");
            if (!Directory.Exists(projectDir))
                Directory.CreateDirectory(projectDir);

            var dir = new DirectoryInfo(projectDir);

            var projectFiles = dir.GetFiles("*.json").ToList();
            ProjectListBox.ItemsSource = projectFiles;
        }

        private void ProjectSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var addConfigurationWindow = new AddConfigurationWindow();
            addConfigurationWindow.ShowDialog();
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var addProjectWindow = new AddProjectWindow();
            addProjectWindow.ShowDialog();
        }

        // test için sadece
        //
        private void TestProjectWindow(object sender, RoutedEventArgs e)
        {
            var projectWindow = new ProjectWindow();
            projectWindow.ShowDialog();
        }
        //
        //

        private void ConfigurationSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void EditConfigButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void DeleteConfigButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void OpenProjectButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void EditProjectButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void DeleteProjectButton_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}