; Script for the installation of Enigma Astrology Research 0.1.0 - beta.

#define MyAppName "Enigma Astrology Research"
#define MyAppVersion "0.1.0"
#define MyAppPublisher "Jan Kampherbeek"
#define MyAppURL "http://www.radixpro.com"
#define MyAppExeName "enigma.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F5C11EE4-ECFF-4FF4-89CB-ED992780C19B}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName=RadixPro
DisableDirPage=no
DisableProgramGroupPage=no
UninstallDisplayIcon={group}\{#MyAppName}
LicenseFile=d:\dev\proj\EnigmaSuite\Enigma\copyright.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=Enigma AR installation
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "dutch"; MessagesFile: "compiler:Languages\Dutch.isl"


[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]

Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\Enigma.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\*.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\logs\*"; DestDir: "{app}\logs"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\Images\*"; DestDir: "{app}\Images"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\res\*"; DestDir: "{app}\res"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\res\help\*"; DestDir: "{app}\res\help"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\Enigma.Frontend\bin\Release\net6.0-windows\res\help\css\*"; DestDir: "{app}\res\help\css"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\documentation\User Manual Enigma Research - 0.1.pdf"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\copyright.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\gpl-3.0.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "d:\dev\proj\Enigmasuite\Enigma\se-license.html"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

