[Setup]
AppName=Integrated Assignment Software
AppVersion=1.0.0
DefaultDirName={pf}\IntegratedAssignmentSoftware
DefaultGroupName=IntegratedAssignmentSoftware
OutputDir=.
OutputBaseFilename=Installer
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin

[Files]
; Include everything in Release folder
Source: "bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
; Start menu shortcut
Name: "{group}\Integrated Assignment Software"; Filename: "{app}\net9.0-windows\win-x64\IntegratedAssignmentSoftware.exe"; WorkingDir: "{app}\net9.0-windows\win-x64"; IconFilename: "{app}\net9.0-windows\win-x64\IntegratedAssignmentSoftware.exe"

; Optional desktop shortcut
Name: "{commondesktop}\Integrated Assignment Software"; Filename: "{app}\net9.0-windows\win-x64\IntegratedAssignmentSoftware.exe"; Tasks: desktopicon; WorkingDir: "{app}\net9.0-windows\win-x64"; IconFilename: "{app}\net9.0-windows\win-x64\IntegratedAssignmentSoftware.exe"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"
