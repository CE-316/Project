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
Name: "{group}\Integrated Assignment Software"; Filename: "{app}\net9.0-windows\IntegratedAssignmentSoftware.exe"; WorkingDir: "{app}\net9.0-windows"; IconFilename: "{app}\net9.0-windows\IntegratedAssignmentSoftware.exe"

; Optional desktop shortcut
Name: "{commondesktop}\Integrated Assignment Software"; Filename: "{app}\net9.0-windows\IntegratedAssignmentSoftware.exe"; Tasks: desktopicon; WorkingDir: "{app}\net9.0-windows"; IconFilename: "{app}\net9.0-windows\IntegratedAssignmentSoftware.exe"

[Tasks]
Name: "desktopicon"; Description: "Create a &desktop icon"; GroupDescription: "Additional icons:"

[UninstallDelete]
Type: filesandordirs; Name: "{app}"


;!!!RUN "dotnet build -c Release" ON CMD FIRST!!!