#pragma once




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


// http://www.codeproject.com/Articles/28969/HowTo-Export-C-classes-from-a-DLL#CppMatureApproach

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the VISUALIZATION_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// VISUALIZATION_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef VISUALIZATION_EXPORTS
#define VISUALIZATION_API __declspec(dllexport)
#else
#define VISUALIZATION_API __declspec(dllimport)
#endif

////////////////////////////////////////////////////////////////////////////////
// 

#include <iostream>

namespace test {

	class TestLibrary {
	public:

		static int doSomething(void) { std::cout << "hallo" << std::endl; }
	};
}


#define EXTERN_C extern "C"

EXTERN_C VISUALIZATION_API INT WINAPI doSomething(VOID);


#if !defined(_WIN64)
// This pragma is required only for 32-bit builds. In a 64-bit environment,
// C functions are not decorated.
#pragma comment(linker, "/export:close=_close@0")

#endif  // _WIN64

VISUALIZATION_API int WINAPI doSomething(void) {
	return test::TestLibrary::doSomething();
}