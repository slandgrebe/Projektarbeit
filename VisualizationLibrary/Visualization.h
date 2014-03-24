#pragma once

#include "stdafx.h"

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport) 
#else
#define DLL_API __declspec(dllimport) 
#endif


// exportierte funktionen
extern "C" DLL_API VOID APIENTRY doSomething(const char* text);

extern "C" DLL_API unsigned int APIENTRY addModel(const char* filename);

extern "C" DLL_API unsigned int APIENTRY addPoint(const char* filename = "sample.png");

extern "C" DLL_API bool APIENTRY isModelCreated(const unsigned int modelId);

extern "C" DLL_API bool APIENTRY positionModel(const unsigned int modelId, const float x, const float y, const float z);

extern "C" DLL_API bool APIENTRY rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z);

extern "C" DLL_API bool APIENTRY scaleModel(const unsigned int modelId, const float x, const float y, const float z);