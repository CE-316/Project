using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Path = System.IO.Path;
using System.IO.Compression;
using System.Diagnostics;

namespace IntegratedAssignmentSoftware
{
    /// <summary>
    /// ProjectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private string fileName;
        private string fullPath;
        private string projectsDir;
        private string originalPath;
        private ObservableCollection<ConfigModel> _configs = new ObservableCollection<ConfigModel>();
        public ObservableCollection<SubmissionViewModel> Submissions { get; } = new ObservableCollection<SubmissionViewModel>();
        public ProjectModel Project { get; set; }

        private (bool Success, string StdOut, string StdErr)
        RunShell(string command, string workingDirectory, int timeoutMs = 5000)
        {
            var psi = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi)!;
            if (!proc.WaitForExit(timeoutMs))
            {
                // timed out
                try { proc.Kill(true); } catch { /* ignore */ }
                return (false, string.Empty, "Timeout");
            }

            // process has exited
            string outText = proc.StandardOutput.ReadToEnd();
            string errText = proc.StandardError.ReadToEnd();
            bool ok = proc.ExitCode == 0;
            return (ok, outText, errText);
        }

        private (bool Success, string StdOut, string StdErr)
        RunWithInput(string command, string workingDirectory, string input, int timeoutMs = 5000)
        {
            var psi = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                WorkingDirectory = workingDirectory,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var proc = Process.Start(psi)!;

            // write the input and close the stream so the process can proceed
            proc.StandardInput.Write(input);
            proc.StandardInput.Close();

            if (!proc.WaitForExit(timeoutMs))
            {
                // timed out
                try { proc.Kill(true); } catch { /* ignore */ }
                return (false, string.Empty, "Timeout");
            }

            string outText = proc.StandardOutput.ReadToEnd();
            string errText = proc.StandardError.ReadToEnd();
            bool ok = proc.ExitCode == 0;
            return (ok, outText, errText);
        }
        public ProjectWindow(ProjectModel project)
        {
            InitializeComponent();
            Project = project;
            DataContext = this;
            NameTextBox.Text = project.Name;
            SetRichText(DescriptionTextBox, project.Description);
            /*if (!string.IsNullOrEmpty(project.SubmissionsDirectory))
            {
                LoadSubmissions(Path.Combine(project.SubmissionsDirectory, "Extracted"));
            }*/

            foreach (SubmissionData submissionData in Project.SavedSubmissionResults)
            {
                SubmissionViewModel submissionViewModel = new SubmissionViewModel(submissionData.Name, submissionData.Results, submissionData.Code);
                Submissions.Add(submissionViewModel);
            }

            projectsDir = Path.Combine(AppContext.BaseDirectory, "Projects");
            Directory.CreateDirectory(projectsDir);
            originalPath = Path.Combine(projectsDir, $"{project.Name}.json");

            ConfigurationComboBox.ItemsSource = _configs;
            LoadConfigurations();
            if (Project.Configuration == null)
            {
                Project.Configuration = new ConfigModel { Language = null };
            }
            if (Project.Configuration.Language != null)
            {
                var match = _configs.FirstOrDefault(c => c.Language == Project.Configuration.Language);
                if (match != null)
                    ConfigurationComboBox.SelectedItem = match;
            }
            SubmissionsList.ItemsSource = Submissions;
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFolderDialog();

            if (dlg.ShowDialog() != true)
            {
                return;
            }
            var selectedPath = dlg.FolderName;
            Project.SubmissionsDirectory = selectedPath;
            string extractFolder = ExtractSubmissions(Project.SubmissionsDirectory);
            LoadSubmissions(extractFolder);
        }
        private string ExtractSubmissions(string submissionsDirectory)
        {
            string zipFolder = submissionsDirectory;
            string extractFolder = Path.Combine(zipFolder, "Extracted");

            WipeExtractedFolder(extractFolder);
            
            Directory.CreateDirectory(extractFolder);

            
            foreach (var zipPath in Directory.GetFiles(zipFolder, "*.zip"))
            {
                string folderName = Path.GetFileNameWithoutExtension(zipPath);
                string outDir = Path.Combine(extractFolder, folderName);
                /*
                if (Directory.Exists(outDir))
                {
                    Directory.Delete(outDir, true);
                }*/
                Directory.CreateDirectory(outDir);
                ZipFile.ExtractToDirectory(zipPath, outDir);
            }
            return extractFolder;
        }
        private void LoadSubmissions(string rootFolder)
        {
            Submissions.Clear();

            var testCases = Project.TestCases;
            int totalTests = testCases.Count;

            Directory.CreateDirectory(rootFolder);
            foreach (var dir in Directory.GetDirectories(rootFolder))
            {
                string name = Path.GetFileName(dir);

                var results = EvaluateSubmission(dir, Project.TestCases);
                string code = "";
                var files = Directory.GetFiles(dir, "Main.*");
                var tempCode = files.FirstOrDefault();
                if (tempCode != null)
                {
                    code = File.ReadAllText(tempCode);
                }
                Submissions.Add(new SubmissionViewModel(name, results, code));

            }
        }

        private List<TestCaseResult> EvaluateSubmission(string submissionFolder, IList<TestCaseModel> tests)
        {
            var cfg = Project.Configuration;
            Debug.WriteLine("Config compile: " + cfg.Compile);
            Debug.WriteLine("Config run: " + cfg.Run);
            //skip if cfg.Compile is empty
            Debug.WriteLine("Checking compile command.");
            
            if (!string.IsNullOrWhiteSpace(cfg.Compile))
            {
                var (okC, _, errC) = RunShell(cfg.Compile, submissionFolder);

                if (!okC)
                {
                    // mark all as failed if compile fails
                    return tests
                        .Select(tc => new TestCaseResult(tc, false, errC))
                        .ToList();
                }
            }
            else
            {
                Debug.WriteLine("No compile command found");
            }

                // Run each test by piping the Input string into stdin
                var results = new List<TestCaseResult>(tests.Count);
            foreach (var tc in tests)
            {
                Debug.WriteLine("Running Test Case: " + tc.Name);
                // RunWithInput feeds tc.Input into stdin, captures stdout
                var (okR, stdout, stderr) = RunWithInput(
                    cfg.Run,
                    submissionFolder,
                    tc.Input
                );
                Debug.WriteLine($"─── RUN `{cfg.Run}` on `{submissionFolder}` ───");
                Debug.WriteLine($"Success: {okR}");
                Debug.WriteLine($"STDOUT:\n{stdout}");
                Debug.WriteLine($"STDERR:\n{stderr}");
                bool passed = false;
                if (okR)
                {
                    Debug.WriteLine("Checking Test Case Result: " + tc.Name + " for " + submissionFolder);
                    // Compare trimmed output text to expected
                    passed = stdout.TrimEnd() == tc.Output.TrimEnd();
                    Debug.WriteLine("Test case passed: " + passed);
                }
                else
                {
                    Debug.WriteLine("Run failed.");
                }

                    results.Add(new TestCaseResult(tc, passed, stdout));
            }

            return results;
        }

        private void SaveResults_Click(object sender, RoutedEventArgs e)
        {
            Project.SavedSubmissionResults.Clear();
            foreach (SubmissionViewModel submissionViewModel in Submissions)
            {
                SubmissionData submissionData = new SubmissionData();
                submissionData.Name = submissionViewModel.Name;
                submissionData.Results = submissionViewModel.Results;
                submissionData.Code = submissionViewModel.Code;
                Project.SavedSubmissionResults.Add(submissionData);
            }
            projectsDir = Path.Combine(AppContext.BaseDirectory, "Saves");
            Directory.CreateDirectory(projectsDir);
            originalPath = Path.Combine(projectsDir, "submission_results.json"); 
            JsonLoader.SaveToFile(Project.SavedSubmissionResults, originalPath);

        }
        string GetRichText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
        void SetRichText(RichTextBox rtb, string rtfText)
        {
            var range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfText));
            range.Load(stream, DataFormats.Text);
        }
        private void ConfigurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ConfigurationComboBox.SelectedItem is ConfigModel selected)
            {
                Project.Configuration = selected;
            }
        }
        private void LoadConfigurations()
        {
            string configDir = Path.Combine(AppContext.BaseDirectory, "Configurations");
            Directory.CreateDirectory(configDir);

            _configs.Clear();
            foreach (var fi in new DirectoryInfo(configDir).GetFiles("*.json"))
            {
                try
                {
                    var cfg = JsonLoader.LoadFromFile<ConfigModel>(fi.FullName);
                    _configs.Add(cfg);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to load {fi.Name}: {ex.Message}");
                }
            }
        }
        private void WipeExtractedFolder(string extractFolder)
        {
            if (!Directory.Exists(extractFolder))
                return;

            foreach (var dir in Directory.GetDirectories(extractFolder))
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine(dir);
                    Directory.Delete(dir, recursive: true);
                    System.Diagnostics.Debug.WriteLine("Wiped folder.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Couldn’t delete {dir}: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            foreach (var file in Directory.GetFiles(extractFolder))
            {
                try 
                { 
                    File.Delete(file);
                    System.Diagnostics.Debug.WriteLine("Wiped file.");
                }
                catch { System.Diagnostics.Debug.WriteLine("Could not wipe file."); }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(GetRichText(DescriptionTextBox)))
            {
                MessageBox.Show("Make sure the project has a name and description.");
                return;
            }
            Project.Name = NameTextBox.Text;
            Project.Description = GetRichText(DescriptionTextBox);

            fileName = $"{Project.Name}.json";
            fullPath = Path.Combine(projectsDir, fileName);

            JsonLoader.SaveToFile(Project, fullPath);

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
            MessageBox.Show("Project saved successfully.");
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Project.Configuration == null)
                {
                    MessageBox.Show("No configuration selected.");
                    return;
                }

                string submissionRoot = Path.Combine(Project.SubmissionsDirectory, "Extracted");
                if (!Directory.Exists(submissionRoot))
                {
                    MessageBox.Show("No extracted submissions found. Did you extract the ZIPs?");
                    return;
                }

                // Clear existing results
                Submissions.Clear();

                var testCases = Project.TestCases;
                int totalTests = testCases.Count;

                var resultsSummary = new StringBuilder();

                foreach (var submissionFolder in Directory.GetDirectories(submissionRoot))
                {
                    string studentName = Path.GetFileName(submissionFolder);
                    string code = "";
                    var files = Directory.GetFiles(submissionFolder, "Main.*");
                    var tempCode = files.FirstOrDefault();
                    if (tempCode != null)
                    {
                        code = File.ReadAllText(tempCode);
                    }

                    var results = EvaluateSubmission(submissionFolder, testCases);

                    // Add to Submissions (for UI update)
                    var viewModel = new SubmissionViewModel(studentName, results, code);
                    Submissions.Add(viewModel);

                    // Summary for message box
                    resultsSummary.AppendLine($"{studentName}:");
                    foreach (var res in results)
                    {
                        resultsSummary.AppendLine($"  {res.TestCase.Name}: {(res.Passed ? "Passed" : "Failed")}");
                        resultsSummary.AppendLine($"  Output: {res.Output.Trim()}");
                    }
                    resultsSummary.AppendLine(new string('-', 40));
                }

                MessageBox.Show(resultsSummary.ToString(), "Execution Summary", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error:\n{ex.Message}", "Error");
            }
        }






        private void SubmissionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is SubmissionViewModel submissionViewModel)
            {
                CodeViewer.Text = submissionViewModel.Code;
            }
        }
    } 
}
