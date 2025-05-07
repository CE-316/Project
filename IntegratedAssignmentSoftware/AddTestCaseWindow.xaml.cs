using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// AddTestCaseWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class AddTestCaseWindow : Window
    {
        private static readonly Regex _nonDigit = new Regex(@"\D");
        private ProjectModel _projectModel;
        public AddTestCaseWindow(ProjectModel projectModel)
        {
            _projectModel = projectModel;
            InitializeComponent();
        }

        private void ChooseInputFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Select a text file",
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = AppContext.BaseDirectory
            };

            if (dlg.ShowDialog() == true)
            {
                string text = File.ReadAllText(dlg.FileName);

                InputTextBox.Text = text;
            }
        }

        private void ChooseOutputFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "Select a text file",
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = AppContext.BaseDirectory
            };

            if (dlg.ShowDialog() == true)
            {
                string text = File.ReadAllText(dlg.FileName);

                OutputTextBox.Text = text;
            }
        }

        private void SaveTestCaseButton_Click(object sender, RoutedEventArgs e)
        {
            int? nullableValue = PointsUpDown.Value;
            if (!string.IsNullOrEmpty(InputTextBox.Text) && !string.IsNullOrEmpty(OutputTextBox.Text) && !string.IsNullOrEmpty(TestCaseNameTextBox.Text) && nullableValue.HasValue)
            {
                TestCaseModel testCaseModel = new TestCaseModel();
                testCaseModel.Input = InputTextBox.Text;
                testCaseModel.Output = OutputTextBox.Text;
                testCaseModel.Points = nullableValue.Value;
                testCaseModel.Name = TestCaseNameTextBox.Text;
                _projectModel.TestCases.Add(testCaseModel);
            }
            else
            {
                MessageBox.Show("Check for empty fields.");
            }
        }

        private void PointsUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _nonDigit.IsMatch(e.Text);
        }

        private void PointsUpDown_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text)!;
                if (_nonDigit.IsMatch(text))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
