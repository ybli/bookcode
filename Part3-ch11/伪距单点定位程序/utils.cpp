#include "stdafx.h"
#include "utils.h"

#include <list>

int string_split(const string& str,string sep,vector<string>& ret)
{
    if (str.empty())    
        return 0;    

    string tmp;
    string::size_type pos_begin = str.find_first_not_of(sep);
    string::size_type comma_pos = 0;

	ret.clear();
    while (pos_begin != string::npos)
    {
        comma_pos = str.find(sep, pos_begin);
        if (comma_pos != string::npos)
        {
            tmp = str.substr(pos_begin, comma_pos - pos_begin);
            pos_begin = comma_pos + sep.length();
        }
        else
        {
            tmp = str.substr(pos_begin);
            pos_begin = comma_pos;
        }

        if (!tmp.empty())
        {
            ret.push_back(tmp);
            tmp.clear();
        }
    }
    return 0;
}

void string_trimmed(string &str)
{
	int s = 0,e = str.length();
	int str_len = str.length();
	for(int i = 0; i < str_len; i++)
	{
		if(str[i] == '\t' || str[i] == '\n' || str[i] == '\v'
		|| str[i] == '\f' || str[i] == '\r' || str[i] == ' ')  
			s = i+1;
		else
			break;
	}

	if(s == str_len)
	{
		str = "";
	}
	else
	{
		for(int i = str_len - 1; i >= s; i--)
		{
			if(str[i] == '\t' || str[i] == '\n' || str[i] == '\v'
			|| str[i] == '\f' || str[i] == '\r' || str[i] == ' ')  
				e = i-1;
			else
				break;
		}	
		str = str.substr(s,e - s + 1);
	}	
}	

void string_replace(string &str, const  string &old_value, const string &new_value)     
{     
    for(string::size_type   pos(0);   pos!=string::npos;   pos+=new_value.length())  
    {     
        if((pos=str.find(old_value,pos)) != string::npos)     
        { 
            str.replace(pos,old_value.length(),new_value);  
        }   
        else  
        { 
            break; 
        }
    }         
}  


string CStr_to_str(CString cstr)
{
#ifdef _UNICODE
	if(cstr.IsEmpty())
		return string();		
	int len = cstr.GetLength() * 2 + 2;
	char *buffer = new char[len];
	WideCharToMultiByte(CP_OEMCP, NULL, cstr, -1, buffer, len, NULL, FALSE);

	string str = buffer;
	delete []buffer;
	return str;
#else
	return (const char*)cstr;
#endif
}

CString str_to_CStr(string str)
{
#ifdef _UNICODE
	if(str.empty())
		return CString();
	
	int len = str.length() + 1;
	wchar_t *buffer = new wchar_t[len];
	// Convert headers from ASCII to Unicode.
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, buffer, len);

	CString cstr = buffer;
	delete []buffer;
	return cstr;
#else
	return str.c_str();
#endif
}

string GetAppExePathName()
{
	TCHAR lszPath[MAX_PATH] = _T("");
	GetModuleFileName(NULL, lszPath, MAX_PATH);
	return CStr_to_str(lszPath);
}

string GetAppExePath()
{
	string lstrPathName = GetAppExePathName();
	string::size_type liPos = lstrPathName.rfind("\\");
	if(liPos != -1)
	{
		return lstrPathName.substr(0,liPos+1);
	}
	else
	{
		return lstrPathName;
	}
}

string ExtractFileName(const string astrFilePathName)
{
	char drive[_MAX_DRIVE];   
	char dir[_MAX_DIR];   
	char fname[_MAX_FNAME];   
	char ext[_MAX_EXT];   
	_splitpath_s(astrFilePathName.c_str(),drive,dir,fname,ext);   
	return string(fname) + ext;
}

string ExtractFilePath(const string astrFilePathName)
{
	char drive[_MAX_DRIVE];   
	char dir[_MAX_DIR];   
	char fname[_MAX_FNAME];   
	char ext[_MAX_EXT];   
	_splitpath_s(astrFilePathName.c_str(),drive,dir,fname,ext);   
	return string(drive) + dir;
}

BOOL FileExists( LPCTSTR szFile)
{
	OFSTRUCT of;   
	if (OpenFile(CStr_to_str(szFile).c_str(), &of, OF_EXIST) == HFILE_ERROR)
	{
		int liError = GetLastError();
		return   FALSE;
	}
	else
	{
		return   TRUE;
	}
}

BOOL FileExists(string astrFile)
{
	return FileExists(astrFile.c_str());
}

BOOL CreateDirectory(string astrDir)
{
	HANDLE lhFile;					// File Handle
	WIN32_FIND_DATA	fileinfo;		// File Information Structure
	list<string> loDirList;			// CString Array to hold Directory Structures
	BOOL lbResult;					// BOOL used to test if Create Directory was successful
	string::size_type lsizePos = 0;	// Counter
	string lstrTemp = "";			// lstrTempporary CString Object

	lhFile = FindFirstFile(str_to_CStr(astrDir),&fileinfo);

	// if the file exists and it is a directory
	if(fileinfo.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY)
	{
		//  Directory Exists close file and return
		FindClose(lhFile);
		return FALSE;
	}
	loDirList.clear();

	for(lsizePos = 0; lsizePos < astrDir.length(); lsizePos++ )		// Parse the supplied CString Directory String
	{									
		if(astrDir.at(lsizePos) != '\\')		// if the Charachter is not a \ 
		{
			lstrTemp += astrDir.at(lsizePos);	// aastrDir the character to the lstrTempp String
		}
		else
		{
			loDirList.push_back(lstrTemp);		// if the Character is a \ 
			lstrTemp += "\\";					// Now aastrDir the \ to the lstrTempp string
		}
		if(lsizePos == astrDir.length()-1)		// If we reached the end of the String
			loDirList.push_back(lstrTemp);
	}

	// Close the file
	FindClose(lhFile);

	// Now lets cycle through the String Array and create each directory in turn
	for(list<string>::iterator lsizePos=loDirList.begin();lsizePos != loDirList.end();lsizePos++)//lsizePos = 1; lsizePos < loDirList.size(); lsizePos++)
	{
		lstrTemp = *lsizePos;//loDirList.at(lsizePos);
		lbResult = CreateDirectory(str_to_CStr(lstrTemp),NULL);

		// If the Directory exists it will return a false
		if(lbResult)
			SetFileAttributes(str_to_CStr(lstrTemp),FILE_ATTRIBUTE_NORMAL);
		// If we were successful we set the attributes to normal
	}
	//  Now lets see if the directory was successfully created
	lhFile = FindFirstFile(str_to_CStr(astrDir),&fileinfo);

	loDirList.clear();
	if(fileinfo.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY)
	{
		//  Directory Exists close file and return
		FindClose(lhFile);
		return TRUE;
	}
	else
	{
		// For Some reason the Function Failed  Return FALSE
		FindClose(lhFile);
		return FALSE;
	}
}

bool DirectoryExists(string astrDir)
{
	return PathFileExists(str_to_CStr(astrDir)) == TRUE;
}