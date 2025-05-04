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

        public static string RunJava(string workingDirectory, string className, string arguments = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "java",
                Arguments = $"{className} {arguments}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return string.IsNullOrWhiteSpace(errors) ? output : errors;
            }
        }


        public static string RunPython(string pythonPath, string scriptPath, string arguments = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"\"{scriptPath}\" {arguments}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return string.IsNullOrWhiteSpace(errors) ? output : errors;
            }
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

        public static string RunCpp(string exePath, string arguments = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string errors = process.StandardError.ReadToEnd();
                process.WaitForExit();

                return string.IsNullOrWhiteSpace(errors) ? output : errors;
            }
        }

    }
}
