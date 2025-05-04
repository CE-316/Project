using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// EditProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class EditProjectWindow : Window
    {
        public EditProjectWindow()
        {
            InitializeComponent();
            LoadConfigFiles();
        }
        public void SaveProjectButton_Click()
        {

        }

        private ICollectionView configListView;
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

        private void EditTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddTestCaseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
