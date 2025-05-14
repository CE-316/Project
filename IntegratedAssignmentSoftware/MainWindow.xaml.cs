using IntegratedAssignmentSoftware.Services;
using System.ComponentModel;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;
using Microsoft.Win32;

namespace IntegratedAssignmentSoftware
{
    public partial class MainWindow : Window
    {
        private ICollectionView configListView;
        private ICollectionView projectListView;
        private string projectDir;
        private string configDir;
        public MainWindow()
        {
            InitializeComponent();
            projectDir = Path.Combine(AppContext.BaseDirectory, "Projects");
            configDir = Path.Combine(AppContext.BaseDirectory, "Configurations");
            LoadConfigFiles();
            LoadProjectFiles();
        }

        private void LoadConfigFiles()
        {
            if (!Directory.Exists(configDir))
                Directory.CreateDirectory(configDir);

            var configFiles = new DirectoryInfo(configDir).GetFiles("*.json");

            var configModels = configFiles
                .Select(fi =>
                {
                    try
                    {
                        return JsonLoader.LoadFromFile<ConfigModel>(fi.FullName);
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(p => p != null)
                .ToList();

            configListView = CollectionViewSource.GetDefaultView(configModels);
            ConfigurationListBox.ItemsSource = configListView;
        }

        private void LoadProjectFiles()
        {
            if (!Directory.Exists(projectDir))
                Directory.CreateDirectory(projectDir);

            var projectFiles = new DirectoryInfo(projectDir).GetFiles("*.json");

            var projectModels = projectFiles
                .Select(fi =>
                {
                    try
                    {
                        return JsonLoader.LoadFromFile<ProjectModel>(fi.FullName);
                    }
                    catch
                    {
                        Debug.WriteLine("Error while loading from file");
                        throw;
                    }
                })
                .Where(p => p != null)
                .ToList();

            projectListView = CollectionViewSource.GetDefaultView(projectModels);
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
                : new Predicate<object>(o => ((ConfigModel)o).Language.ToLowerInvariant().Contains(term));

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
        private void OpenProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProjectModel project)
            {
                ProjectWindow projectWindow = new ProjectWindow(project);
                projectWindow.Title = project.Name;
                projectWindow.Show();
                this.Close();
            }
        }
        private void EditProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProjectModel project)
            {
                EditProjectWindow editProjectWindow = new EditProjectWindow(project);
                bool? isSaved = editProjectWindow.ShowDialog();

                if (isSaved == true)
                {
                    LoadProjectFiles();
                }
            }
        }
        private void DeleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProjectModel project)
            {
                string deletePath = Path.Combine(projectDir, $"{project.Name}.json");
                try
                {
                    File.Delete(deletePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Warning: could not delete old file:\n{ex.Message}",
                                    "Cleanup Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            LoadProjectFiles();
        }
        private void EditConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ConfigModel config)
            {
                EditConfigurationWindow editConfigurationWindow = new EditConfigurationWindow(config);
                bool? isSaved = editConfigurationWindow.ShowDialog();

                if (isSaved == true)
                {
                    LoadConfigFiles();
                }
            }
        }

        private void DeleteConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ConfigModel config)
            {
                string deletePath = Path.Combine(configDir, $"{config.Language}.json");
                try
                {
                    File.Delete(deletePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Warning: could not delete old file:\n{ex.Message}",
                                    "Cleanup Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            LoadConfigFiles();
        }

        private void ImportConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();

            if (dlg.ShowDialog() != true)
            {
                return;
            }
            var selectedPath = dlg.FileName;
            var fileName = Path.GetFileNameWithoutExtension(selectedPath);

            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }
            try
            {
                ConfigModel config = JsonLoader.LoadFromFile<ConfigModel>(selectedPath);
                if (config.Language != fileName)
                {
                    MessageBox.Show("Configuration name and file name must be the same");
                    return;
                }
                File.Copy(selectedPath, Path.Combine(configDir, $"{fileName}.json"));
                LoadConfigFiles();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                MessageBox.Show("Could not import configuration from file");
            }
        }
    }
}