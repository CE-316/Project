# 📘 Integrated Assignment Environment (IAE)

**IAE** is a Windows desktop application designed to automate the compilation, execution, and evaluation of student programming assignments. Built with **.NET** and **WPF (C#)**, it provides instructors with an intuitive interface for managing assignments, running test cases, and viewing results efficiently.

![IAE Screenshot](https://github.com/user-attachments/assets/3c4fe596-0ae1-4dbc-ac33-5aaa3d72d4f7)

---

## 🚀 Features

### 📂 Project-Based Workflow
- Create individual projects for each assignment  
- Manage submissions, configurations, and evaluation results together

### ⚙️ Configuration Management
- Define how code should be compiled or interpreted for different languages (C, Java, Python, etc.)  
- Supports custom compile and run commands

### 📦 ZIP Submission Processing
- Automatically extracts `.zip` files submitted by students  
- Organizes extracted folders based on student IDs

### 🧪 Test Case Definition
- Add multiple test cases with custom input and expected output  
- Supports manual entry and file-based testing

### ▶️ Compilation & Execution
- Executes or compiles student code using system-installed tools (e.g., `gcc`, `python`)  
- Based on selected configuration

### 🔍 Output Comparison
- Compares student output with expected results automatically  
- Evaluates success or failure per test case

### 📊 Result Reporting
- Displays each student’s result in a clear list  
- Includes status (compiled, passed, failed), outputs, and error messages

### 💾 Local Storage
- Uses **SQLite** and `.json` files to save configuration, project metadata, and results  
- Fully offline functionality

### 🧭 Simple, Visual Interface
- Built with WPF  
- Clean, responsive, and easy-to-navigate UI

---

## 🖥️ Installation and Running

### ✅ Run via Installer (Recommended)

1. **Download the installer:**  
   _IAE Installer Release_ *(Replace this with your actual GitHub Releases link)*

2. **Run** `IAE_Setup.exe` and follow the installation steps.

3. A **desktop shortcut** will be created automatically.

4. **Double-click** the shortcut to launch the app.

---

## 🧪 Usage Guide

### 1️⃣ Create a New Project
- Click **+ Add Project**
- Enter:
  - Project name
  - Assignment description
  - Submissions folder (ZIPs)
  - Configuration file
  - Test inputs and expected outputs

The application will:
- Extract all `.zip` files  
- Compile or interpret each submission  
- Run with input  
- Compare output  
- Save and display results

![Project Creation](https://github.com/user-attachments/assets/d347325e-f888-47a5-a527-669070f70f0e)

---

### 2️⃣ Create or Edit a Configuration
- Go to **Configurations**
- Click **+ New** or **Edit**
- Enter:
  - Language name (e.g., Java, Python)
  - Compile command (e.g., `javac`, `gcc`)
  - Run command (e.g., `java Main`, `python main.py`)
- Save for future use

![Configuration Edit](https://github.com/user-attachments/assets/aed86c00-e2b8-4a48-83b6-f59e9995be74)

---

### 3️⃣ View Results
- Go to the **Submissions** tab to see:
  - Student IDs
  - Compilation status
  - Test results
  - Output and error logs

---

### 4️⃣ File Viewer
- Explore extracted submission folders and files manually using the built-in file browser

---

### 5️⃣ Help Menu
- Click the **Help** button in the top-right to view the in-app user manual

![Help Menu](https://github.com/user-attachments/assets/02a5f74f-5b12-4cf0-81de-b4fc570a492b)

---

## 🛠️ Reporting Issues

If you encounter a bug or want to suggest a new feature, please [open an issue](https://github.com/your-repo/issues) on the GitHub repository.

---

_Developed with 💻 by Team 5 - CE316 Project_
