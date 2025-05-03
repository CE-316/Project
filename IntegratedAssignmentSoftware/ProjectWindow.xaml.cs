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
        //java
        private void TestCompileJava_Click(object sender, RoutedEventArgs e)
        {
            string javaFilePath = "C:\\Users\\msı\\OneDrive\\Masaüstü\\hellow.java"; //random java file
            string javacPath = "C:\\Program Files\\Java\\jdk-19\\bin\\javac.exe"; //path, 

            bool success = CompilerService.CompileJava(javaFilePath, javacPath, out string errors);

            if (success)
            {
                string output = CompilerService.RunJava("C:\\Users\\msı\\OneDrive\\Masaüstü", "hellow");
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
            string scriptPath = "C:\\Users\\msı\\OneDrive\\Masaüstü\\test.py"; //random python file

            string output = CompilerService.RunPython(pythonPath, scriptPath);
            MessageBox.Show("Python Output:\n" + output);
        }
        //c++
        /* 
         * <Button Content="Test Compile C++" Click="TestCompileCpp_Click" Margin="5" Height="220"/>
        private void TestCompileCpp_Click(object sender, RoutedEventArgs e)
        {
            string cppSource = "C:\\Users\\msı\\OneDrive\\Masaüstü\\main.cpp";
            string exeOutput = "C:\\Users\\msı\\OneDrive\\Masaüstü\\main.exe";
            string gppPath = "g++"; // path

            bool success = CompilerService.CompileCpp(cppSource, exeOutput, gppPath, out string errors);

            if (success)
            {
                string output = CompilerService.RunCpp(exeOutput);
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
