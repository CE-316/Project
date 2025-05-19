📘 Integrated Assignment Environment (IAE)
IAE is a Windows desktop application designed to automate the compilation, execution, and evaluation of student programming assignments. Built with .NET and WPF (C#), it offers instructors an easy-to-use environment for managing programming projects, running test cases, and analyzing results in a structured way.

![111](https://github.com/user-attachments/assets/3c4fe596-0ae1-4dbc-ac33-5aaa3d72d4f7)



🚀 Features
📂 Project-Based Workflow
Create individual projects for each assignment and track submissions, configurations, and results together.

⚙️ Configuration Management
Define how code should be compiled or interpreted for each language (C, Java, Python, etc.).
Supports custom compile/run commands.

📦 ZIP Submission Processing
Automatically extracts .zip files submitted by students and organizes them by student ID.

🧪 Test Case Definition
Add multiple test cases with input and expected output values. Supports text input and file-based testing.

▶️ Compilation & Execution
Compiles or runs student code based on the selected configuration using system-installed tools (e.g., gcc, python).

🔍 Output Comparison
Automatically compares the program’s output with expected results and evaluates success or failure.

📊 Result Reporting
Shows student results in a clear list with status indicators (compiled, passed, failed, etc.). Includes error messages and logs.

💾 Local Storage
Uses SQLite and local .json files to store configuration, project metadata, and results. Works offline.

🧭 Simple, Visual Interface
User-friendly interface developed with WPF, supports configuration and test management with ease.

🖥️ Installation and Running
 Run via Installer (Recommended)
1-Download the installer:
IAE Installer Release (Replace with your GitHub release link)

2-Run IAE_Setup.exe and follow the installation wizard.

3-A desktop shortcut will be created.

4-Double-click the shortcut to launch the app.

Usage
1. Create a New Project
Click “+ New Project”

Enter project name and assignment description.

Select a folder containing .zip student submissions.

Choose a previously saved configuration.

Add test cases with input and expected output.

The application will:

Extract all .zip files

Compile or interpret the code

Run the program with the test input

Compare the output and save results

![222](https://github.com/user-attachments/assets/d347325e-f888-47a5-a527-669070f70f0e)


2. Create or Edit Configuration
Go to “Configurations”

Click “+ New” or “Edit” to define:

Language name (e.g., Java, Python)

Compile command (e.g., javac, gcc)

Run command (e.g., java Main, python main.py)

Save to use it in future projects.

![333](https://github.com/user-attachments/assets/aed86c00-e2b8-4a48-83b6-f59e9995be74)


3. View Results
Go to “Submissions” tab to see:

Student ID

Compilation status

Test case results

Output and error logs


4. File Viewer
Browse extracted files manually via the integrated file explorer panel.


5. Help Menu
Go to Help buttton to view detailed guidance on using the app.


![help](https://github.com/user-attachments/assets/02a5f74f-5b12-4cf0-81de-b4fc570a492b)
