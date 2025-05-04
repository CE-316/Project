using IntegratedAssignmentSoftware.Services;
using System.ComponentModel;
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

        private ICollectionView configListView;
        private ICollectionView projectListView;
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
        private void LoadProjectFiles()
        {
            string projectDir = System.IO.Path.Combine(AppContext.BaseDirectory, "Projects");
            if (!Directory.Exists(projectDir))
                Directory.CreateDirectory(projectDir);

            var dir = new DirectoryInfo(projectDir);

            var projectFiles = dir.GetFiles("*.json").ToList();
            
            projectListView = CollectionViewSource.GetDefaultView(projectFiles);
            ProjectListBox.ItemsSource = projectListView;
        }

        private void ProjectSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var term = ProjectSearchBox.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(term))
            {
                projectListView.Filter = null;
            }
            else
            {
                projectListView.Filter = o =>
                {
                    var fi = o as FileInfo;
                    return fi?.Name.ToLowerInvariant().Contains(term) == true;
                };
            }
            projectListView.Refresh();
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
            var term = ConfigurationSearchBox.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrEmpty(term))
            {
                configListView.Filter = null;
            }
            else
            {
                configListView.Filter = o =>
                {
                    var fi = o as FileInfo;
                    return fi?.Name.ToLowerInvariant().Contains(term) == true;
                };
            }
            configListView.Refresh();
        }
        private void EditConfigButton_Click(Object sender, RoutedEventArgs e)
        {
            var editConfigurationWindow = new EditConfigurationWindow();
            editConfigurationWindow.ShowDialog();
        }
        private void DeleteConfigButton_Click(Object sender, RoutedEventArgs e)
        {
            
            var selectedConfig = ConfigurationListBox.SelectedItem as FileInfo;

            if (selectedConfig != null)
            {
               
                    try
                    {
                        //delete
                        File.Delete(selectedConfig.FullName);

                        //reload list afterr deletion
                        LoadConfigFiles();

                        MessageBox.Show("File deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                       
                        MessageBox.Show($"error while deleting: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                
            }
            else
            {
                // If no item is selected, notify the user
                MessageBox.Show("Please select a configuration file to delete.", "No File Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void OpenProjectButton_Click(Object sender, RoutedEventArgs e)
        {
            
        }
        private void EditProjectButton_Click(Object sender, RoutedEventArgs e)
        {
            var editProjectWindow = new EditProjectWindow();
            editProjectWindow.ShowDialog();
        }
        private void DeleteProjectButton_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}