using System;
using System.Diagnostics;
using System.IO;

namespace IntegratedAssignmentSoftware.Services
{
    public static class CompilerService
    {

        public static bool CompileJava(string sourcePath, string javacPath, out string errors)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = javacPath,
                Arguments = $"\"{sourcePath}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = Process.Start(startInfo);
            errors = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(errors);
        }

        public static string RunJava(string classFolder, string mainClass)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "java",
                Arguments = $"-cp \"{classFolder}\" {mainClass}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(error) ? output : $"[Error]\n{error}";
        }

        public static string RunPython(string pythonExePath, string scriptPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = pythonExePath, // e.g., "python" or full path
                Arguments = $"\"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(error) ? output : $"[Error]\n{error}";
        }

        public static bool CompileCpp(string sourcePath, string outputExePath, string gppPath, out string errors)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = gppPath, // e.g., "g++"
                Arguments = $"\"{sourcePath}\" -o \"{outputExePath}\"",
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = Process.Start(startInfo);
            errors = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(errors);
        }

        public static string RunCpp(string exePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = exePath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            return string.IsNullOrWhiteSpace(error) ? output : $"[Error]\n{error}";
        }
    }
}
