using IntegratedAssignmentSoftware.Services;
using System;
using System.Collections.Generic;
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
    /// ProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class ProjectWindow : Window
    {

        private string projectName;
        private string assignmentOverview;
        private string configuration;
        private string submissionsDirectory;
        private List<TestCaseModel> testCases;
        public ProjectWindow()
        {
            projectName = "test";
            this.Name = projectName;
            InitializeComponent();
        }
        private string SelectFile(string title, string filter, string initialDirectory = "")
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = title,
                Filter = filter,
                InitialDirectory = string.IsNullOrWhiteSpace(initialDirectory)
                                    ? Environment.GetFolderPath(Environment.SpecialFolder.MyComputer)
                                    : initialDirectory
            };

            bool? result = openFileDialog.ShowDialog();
            return result == true ? openFileDialog.FileName : null;
        }

        //java
        private void TestCompileJava_Click(object sender, RoutedEventArgs e)
        {
            string javacPath = SelectFile("Select javac.exe", "Java Compiler (javac.exe)|javac.exe", @"C:\Program Files\Java");
            if (javacPath != null)
            {
                MessageBox.Show("Selected path: " + javacPath);
            }
            else
            {
                MessageBox.Show("No file selected.");
            }

            string javaFilePath = SelectFile("select file to be compiled", "Java Files (*.java)|*.java", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            if (javaFilePath != null)
            {
                MessageBox.Show("Selected path: " + javaFilePath);
            }
            else
            {
                MessageBox.Show("No file selected.");
            }

            string className = System.IO.Path.GetFileNameWithoutExtension(javaFilePath);
            string workingDirectory = System.IO.Path.GetDirectoryName(javaFilePath);

            bool success = CompilerService.CompileJava(javaFilePath, javacPath, out string errors);

            if (success)
            {
                string javaArgs = "hello dunya";
                string output = CompilerService.RunJava(workingDirectory, className, javaArgs);
                MessageBox.Show("Java Output:\n" + output);
            }
            else
            {
                MessageBox.Show("Java Compilation Fail:\n" + errors);
            }
        }
        //phyton
        private void TestRunPython_Click(object sender, RoutedEventArgs e)
        {
            string pythonPath = "python"; //path
            string scriptPath = SelectFile("select file to be compiled", "Python Files (*.py)|*.py", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

            string pythonArgs = "merhabadunya";

            string output = CompilerService.RunPython(pythonPath, scriptPath, pythonArgs);
            MessageBox.Show("Python Output:\n" + output);
        }
        //c++
        /* 
         * <Button Content="Test Compile C++" Click="TestCompileCpp_Click" Margin="5" Height="220"/>
        private void TestCompileCpp_Click(object sender, RoutedEventArgs e)
{
    string cppSource = SelectFile("Select C++ Source File", "C++ Files (*.cpp)|*.cpp", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
    if (cppSource == null) { MessageBox.Show("No file selected."); return; }

    string exeOutput = System.IO.Path.ChangeExtension(cppSource, ".exe");
    string gppPath = "g++"; // or full path to g++

    bool success = CompilerService.CompileCpp(cppSource, exeOutput, gppPath, out string errors);

    if (success)
    {
        string cppArgs = Microsoft.VisualBasic.Interaction.InputBox("Enter arguments for C++ executable:", "C++ Arguments");
        string output = CompilerService.RunCpp(exeOutput, cppArgs);
        MessageBox.Show("C++ Output:\n" + output);
    }
    else
    {
        MessageBox.Show("C++ Compilation Failed:\n" + errors);
    }
}

        */
        //
    } 
}
