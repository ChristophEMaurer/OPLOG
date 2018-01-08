#define LOGBUCH_VERSION "1.29.18" // CHANGE_FOR_NEW_VERSION
#define PROGRAM_NAME "OP-LOG"

//#define DEBUG_NO_FRAMEWORK

// Modify the following defines if you have to target a platform prior to the ones specified below.
// Refer to MSDN for the latest info on corresponding values for different platforms.
#ifndef WINVER				// Allow use of features specific to Windows XP or later.
#define WINVER 0x0501		// Change this to the appropriate value to target other versions of Windows.
#endif

#ifndef _WIN32_WINNT		// Allow use of features specific to Windows XP or later.                   
#define _WIN32_WINNT 0x0501	// Change this to the appropriate value to target other versions of Windows.
#endif						

#ifndef _WIN32_WINDOWS		// Allow use of features specific to Windows 98 or later.
#define _WIN32_WINDOWS 0x0410 // Change this to the appropriate value to target Windows Me or later.
#endif

#ifndef _WIN32_IE			// Allow use of features specific to IE 6.0 or later.
#define _WIN32_IE 0x0600	// Change this to the appropriate value to target other versions of IE.
#endif

#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>

// C RunTime Header Files
#include <stdlib.h>
#include <stdio.h>
#include <malloc.h>
#include <memory.h>
#include <tchar.h>

#include <shellapi.h>

#include "test.h"
#include "resource.h"

//#undef UNICODE
//#undef _UNICODE

// In case the machine this is compiled on does not have the most recent platform SDK
// with these values defined, define them here
#ifndef SM_TABLETPC
	#define SM_TABLETPC		86
#endif

#ifndef SM_MEDIACENTER
	#define SM_MEDIACENTER	87
#endif

// Constants that represent registry key names and value names
// to use for detection
const TCHAR *g_szNetfx10RegKeyName = _T("Software\\Microsoft\\.NETFramework\\Policy\\v1.0");
const TCHAR *g_szNetfx10RegKeyValue = _T("3705");
const TCHAR *g_szNetfx10SPxMSIRegKeyName = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{78705f0d-e8db-4b2d-8193-982bdda15ecd}");
const TCHAR *g_szNetfx10SPxOCMRegKeyName = _T("Software\\Microsoft\\Active Setup\\Installed Components\\{FDC11A6F-17D1-48f9-9EA3-9051954BAA24}");
const TCHAR *g_szNetfx10SPxRegValueName = _T("Version");
const TCHAR *g_szNetfx11RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v1.1.4322");
const TCHAR *g_szNetfx20RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v2.0.50727");
const TCHAR *g_szNetfx11and20RegValueName = _T("Install");
const TCHAR *g_szNetfx11and20SPxRegValueName = _T("SP");
const TCHAR *g_szNetfx30RegKeyName = _T("Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup");
const TCHAR *g_szNetfx30RegValueName = _T("InstallSuccess");
const TCHAR *g_szNetfx30VersionRegValueName = _T("Version");
const int g_iNetfx30VersionMajor = 3;
const int g_iNetfx30VersionMinor = 0;
const int g_iNetfx30BuildNumber = 4506;
const int g_iNetfx30RevisionNumber = 26;

#define DownloadDOTNET20Url "http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en"

// Function prototypes
bool CheckNetfx30BuildNumber();
int GetNetfx10SPLevel();
int GetNetfx11SPLevel();
int GetNetfx20SPLevel();
bool IsCurrentOSTabletMedCenter();
bool IsNetfx10Installed();
bool IsNetfx11Installed();
bool IsNetfx20Installed();
bool IsNetfx30Installed();
bool RegistryGetValue(HKEY, const TCHAR*, const TCHAR*, DWORD, LPBYTE, DWORD);

/******************************************************************
Function Name:	IsNetfx10Installed
Description:	Uses the detection method recommended at
                http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnnetdep/html/dotnetfxref.asp
                to determine whether the .NET Framework 1.0 is
                installed on the machine
Inputs:	        NONE
Results:        true if the .NET Framework 1.0 is installed
                false otherwise
******************************************************************/
bool IsNetfx10Installed()
{
	TCHAR szRegValue[MAX_PATH];
	return (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx10RegKeyName, g_szNetfx10RegKeyValue, NULL, (LPBYTE)szRegValue, MAX_PATH));
}


/******************************************************************
Function Name:	IsNetfx11Installed
Description:	Uses the detection method recommended at
                http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnnetdep/html/redistdeploy1_1.asp
                to determine whether the .NET Framework 1.1 is
                installed on the machine
Inputs:	        NONE
Results:        true if the .NET Framework 1.1 is installed
                false otherwise
******************************************************************/
bool IsNetfx11Installed()
{
	bool bRetValue = false;
	DWORD dwRegValue=0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx11RegKeyName, g_szNetfx11and20RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = true;
	}

	return bRetValue;
}


/******************************************************************
Function Name:	IsNetfx20Installed
Description:	Uses the detection method recommended at
                http://msdn2.microsoft.com/en-us/library/aa480243.aspx
                to determine whether the .NET Framework 2.0 is
                installed on the machine
Inputs:	        NONE
Results:        true if the .NET Framework 2.0 is installed
                false otherwise
******************************************************************/
bool IsNetfx20Installed()
{
	bool bRetValue = false;
	DWORD dwRegValue=0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx20RegKeyName, g_szNetfx11and20RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = true;
	}

	return bRetValue;
}


/******************************************************************
Function Name:	IsNetfx30Installed
Description:	Uses the detection method recommended at
                http://msdn2.microsoft.com/en-us/library/aa480173.aspx
                to determine whether the .NET Framework 3.0 is
                installed on the machine
Inputs:	        NONE
Results:        true if the .NET Framework 3.0 is installed
                false otherwise
******************************************************************/
bool IsNetfx30Installed()
{
	bool bRetValue = false;
	DWORD dwRegValue=0;

	// Check that the InstallSuccess registry value exists and equals 1
	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx30RegKeyName, g_szNetfx30RegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		if (1 == dwRegValue)
			bRetValue = true;
	}

	// A system with a pre-release version of the .NET Framework 3.0 can
	// have the InstallSuccess value.  As an added verification, check the
	// version number listed in the registry
	return (bRetValue && CheckNetfx30BuildNumber());
}


/******************************************************************
Function Name:	GetNetfx10SPLevel
Description:	Uses the detection method recommended at
                http://blogs.msdn.com/astebner/archive/2004/09/14/229802.aspx
                to determine what service pack for the 
                .NET Framework 1.0 is installed on the machine
Inputs:	        NONE
Results:        integer representing SP level for .NET Framework 1.0
******************************************************************/
int GetNetfx10SPLevel()
{
	TCHAR szRegValue[MAX_PATH];
	TCHAR *pszSPLevel = NULL;
	int iRetValue = -1;
	bool bRegistryRetVal = false;

	// Need to detect what OS we are running on so we know what
	// registry key to use to look up the SP level
	if (IsCurrentOSTabletMedCenter())
		bRegistryRetVal = RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx10SPxOCMRegKeyName, g_szNetfx10SPxRegValueName, NULL, (LPBYTE)szRegValue, MAX_PATH);
	else
		bRegistryRetVal = RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx10SPxMSIRegKeyName, g_szNetfx10SPxRegValueName, NULL, (LPBYTE)szRegValue, MAX_PATH);

	if (bRegistryRetVal)
	{
		// This registry value should be of the format
		// #,#,#####,# where the last # is the SP level
		// Try to parse off the last # here
		pszSPLevel = _tcsrchr(szRegValue, _T(','));
		if (NULL != pszSPLevel)
		{
			// Increment the pointer to skip the comma
			pszSPLevel++;

			// Convert the remaining value to an integer
			iRetValue = _tstoi(pszSPLevel);
		}
	}

	return iRetValue;
}


/******************************************************************
Function Name:	GetNetfx11SPLevel
Description:	Uses the detection method recommended at
                http://blogs.msdn.com/astebner/archive/2004/09/14/229574.aspx
                to determine what service pack for the 
                .NET Framework 1.1 is installed on the machine
Inputs:	        NONE
Results:        integer representing SP level for .NET Framework 1.1
******************************************************************/
int GetNetfx11SPLevel()
{
	DWORD dwRegValue=0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx11RegKeyName, g_szNetfx11and20SPxRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		return (int)dwRegValue;
	}

	// We can only get here if the .NET Framework 1.1 is not
	// installed or there was some kind of error retrieving
	// the data from the registry
	return -1;
}


/******************************************************************
Function Name:	GetNetfx20SPLevel
Description:	Uses the detection method recommended at
                http://blogs.msdn.com/astebner/archive/2004/09/14/229574.aspx
                to determine what service pack for the 
                .NET Framework 2.0 is installed on the machine
Inputs:         NONE
Results:        integer representing SP level for .NET Framework 2.0
******************************************************************/
int GetNetfx20SPLevel()
{
	DWORD dwRegValue=0;

	if (RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx20RegKeyName, g_szNetfx11and20SPxRegValueName, NULL, (LPBYTE)&dwRegValue, sizeof(DWORD)))
	{
		return (int)dwRegValue;
	}

	// We can only get here if the .NET Framework 2.0 is not
	// installed or there was some kind of error retrieving
	// the data from the registry
	return -1;
}


/******************************************************************
Function Name:	CheckNetfx30BuildNumber
Description:	Retrieves the .NET Framework 3.0 build number from
                the registry and validates that it is not a pre-release
                version number
Inputs:         NONE
Results:        true if the build number is greater than or equal
                to 3.0.04506.25; false otherwise
******************************************************************/
bool CheckNetfx30BuildNumber()
{
	TCHAR szRegValue[MAX_PATH];
	TCHAR *pszToken = NULL;
	int iVersionPartCounter = 0;
	int iVersionMajor = 0;
	int iVersionMinor = 0;
	int iBuildNumber = 0;
	int iRevisionNumber = 0;
	bool bRegistryRetVal = false;
    TCHAR *pContext;

	// Attempt to retrieve the build number registry value
	bRegistryRetVal = RegistryGetValue(HKEY_LOCAL_MACHINE, g_szNetfx30RegKeyName, g_szNetfx30VersionRegValueName, NULL, (LPBYTE)szRegValue, MAX_PATH);

	if (bRegistryRetVal)
	{
		// This registry value should be of the format
		// #.#.#####.##.  Try to parse the 4 parts of
		// the version here
		pszToken = _tcstok_s(szRegValue, _T("."), &pContext);
		while (NULL != pszToken)
		{
			iVersionPartCounter++;

			switch (iVersionPartCounter)
			{
			case 1:
				// Convert the major version value to an integer
				iVersionMajor = _tstoi(pszToken);
				break;
			case 2:
				// Convert the major version value to an integer
				iVersionMinor = _tstoi(pszToken);
				break;
			case 3:
				// Convert the major version value to an integer
				iBuildNumber = _tstoi(pszToken);
				break;
			case 4:
				// Convert the major version value to an integer
				iRevisionNumber = _tstoi(pszToken);
				break;
			default:
				break;

			}

			// Get the next part of the version number
			pszToken = _tcstok_s(NULL, _T("."), &pContext);
		}
	}

	// Compare the version number retrieved from the registry with
	// the version number of the final release of the .NET Framework 3.0
	if (iVersionMajor > g_iNetfx30VersionMajor)
	{
		return true;
	}
	else if (iVersionMajor == g_iNetfx30VersionMajor)
	{
		if (iVersionMinor > g_iNetfx30VersionMinor)
		{
			return true;
		}
		else if (iVersionMinor == g_iNetfx30VersionMinor)
		{
			if (iBuildNumber > g_iNetfx30BuildNumber)
			{
				return true;
			}
			else if (iBuildNumber == g_iNetfx30BuildNumber)
			{
				if (iRevisionNumber >= g_iNetfx30RevisionNumber)
				{
					return true;
				}
			}
		}
	}

	// If we get here, the version in the registry must be less than the
	// version of the final release of the .NET Framework 3.0,
	// so return false
	return false;
}


bool IsCurrentOSTabletMedCenter()
{
	// Use GetSystemMetrics to detect if we are on a Tablet PC or Media Center OS  
	return ( (GetSystemMetrics(SM_TABLETPC) != 0) || (GetSystemMetrics(SM_MEDIACENTER) != 0) );
}


/******************************************************************
Function Name:	RegistryGetValue
Description:	Get the value of a reg key
Inputs:			HKEY hk - The hk of the key to retrieve
				TCHAR *pszKey - Name of the key to retrieve
				TCHAR *pszValue - The value that will be retrieved
				DWORD dwType - The type of the value that will be retrieved
				LPBYTE data - A buffer to save the retrieved data
				DWORD dwSize - The size of the data retrieved
Results:		true if successful, false otherwise
******************************************************************/
bool RegistryGetValue(HKEY hk, const TCHAR * pszKey, const TCHAR * pszValue, DWORD dwType, LPBYTE data, DWORD dwSize)
{
	HKEY hkOpened;

	// Try to open the key
	if (RegOpenKeyEx(hk, pszKey, 0, KEY_READ, &hkOpened) != ERROR_SUCCESS)
	{
		return false;
	}

	// If the key was opened, try to retrieve the value
	if (RegQueryValueEx(hkOpened, pszValue, 0, &dwType, (LPBYTE)data, &dwSize) != ERROR_SUCCESS)
	{
		RegCloseKey(hkOpened);
		return false;
	}
	
	// Clean up
	RegCloseKey(hkOpened);

	return true;
}

int StartExeCreateProcess();

// Forward declarations of functions included in this code module:
INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
    int nRet = 1;

    UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	bool bNetfx20Installed = IsNetfx20Installed();
    
#ifdef DEBUG_NO_FRAMEWORK
    bNetfx20Installed = false;
#endif

    if (bNetfx20Installed)
    {
        nRet = 0;
    }
    else
    {
        DialogBox(hInstance, MAKEINTRESOURCE(IDD_ABOUTBOX), NULL, About);
    }

    if (nRet == 0)
    {
        nRet = StartExeCreateProcess(); 
    }
}


int StartExeCreateProcess()
{
    char program[200];
    int nRet = 0;
    STARTUPINFO si;
    PROCESS_INFORMATION pi;

    ZeroMemory( &si, sizeof(si) );
    si.cb = sizeof(si);
    ZeroMemory( &pi, sizeof(pi) );

    ZeroMemory(&program, sizeof(program));
	sprintf_s(program, sizeof(program),  "%s", ".\\OP-LOG-V" LOGBUCH_VERSION "\\OPSetup.exe");

    // Start the child process. 
    if( !CreateProcess(
        program,
        //".\\OP-LOG-V" LOGBUCH_VERSION "\\OPSetup.exe",
        NULL,        // Command line
        NULL,           // Process handle not inheritable
        NULL,           // Thread handle not inheritable
        FALSE,          // Set handle inheritance to FALSE
        0,              // No creation flags
        NULL,           // Use parent's environment block
        NULL,           // Use parent's starting directory 
        &si,            // Pointer to STARTUPINFO structure
        &pi )           // Pointer to PROCESS_INFORMATION structure
    ) 
    {
		char buf[250];

        nRet = GetLastError();

		sprintf_s(buf, sizeof(buf), "Das Setup konnte nicht gestartet werden (Fehler: %d, %s)!", nRet, program);
        MessageBox(NULL, buf, PROGRAM_NAME, MB_OK);
    }
    else
    {
        // Wait until child process exits.
        WaitForSingleObject( pi.hProcess, INFINITE );

        // Close process and thread handles. 
        CloseHandle( pi.hProcess );
        CloseHandle( pi.hThread );
    }

    return nRet;
}

void StartExplorer()
{
    ShellExecute(NULL, "open", "iexplore.exe",
             DownloadDOTNET20Url, NULL,
             SW_SHOWNORMAL);
}

// Message handler for about box.
INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	UNREFERENCED_PARAMETER(lParam);
	switch (message)
	{
	case WM_INITDIALOG:
        ::SetDlgItemText(hDlg, IDC_EDIT_URL, _T(DownloadDOTNET20Url));
        ::SetDlgItemText(hDlg, IDC_STATIC4, _T("Das .NET Framework 2.0 , welches von dem Programm " 
            PROGRAM_NAME " benötigt wird, ist nicht installiert.\r\rSie müssen das .NET Framework 2.0 "   
            "von der Microsoft Internetseite herunterladen und installieren, und dann dieses Setup erneut ausführen.\r\r"
            "Klicken Sie auf die Schaltfläche 'Das .NET Framework 2.0 herunterladen', um hierfür einen Internet Browser zu starten."
            "\r\rSie können auch die Internetadresse aus dem Eingabefeld in die oberste Zeile Ihres Internet-Browsers kopieren."));
		return (INT_PTR)TRUE;

	case WM_COMMAND:
		if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
		{
			EndDialog(hDlg, LOWORD(wParam));
			return (INT_PTR)TRUE;
		}
		else if (LOWORD(wParam) == IDC_BUTTON_DOWNLOAD)
		{
            StartExplorer();
			return (INT_PTR)TRUE;
		}
		break;
	}
	return (INT_PTR)FALSE;
}


