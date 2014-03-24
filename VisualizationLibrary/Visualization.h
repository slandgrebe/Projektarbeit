#pragma once

#include "stdafx.h"

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport) 
#else
#define DLL_API __declspec(dllimport) 
#endif


// exportierte funktionen
extern "C" DLL_API VOID APIENTRY doSomething(const char* text);