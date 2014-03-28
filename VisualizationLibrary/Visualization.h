#pragma once

#include "stdafx.h"

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport) 
#else
#define DLL_API __declspec(dllimport) 
#endif


// exportierte funktionen
extern "C" DLL_API int APIENTRY isRunning(void);

extern "C" DLL_API void APIENTRY doSomething(const char* text);

extern "C" DLL_API unsigned int APIENTRY addModel(const char* filename);
extern "C" DLL_API unsigned int APIENTRY addPoint(const char* filename);
extern "C" DLL_API int APIENTRY isModelCreated(const unsigned int modelId);

extern "C" DLL_API int APIENTRY positionModel(const unsigned int modelId, const float x, const float y, const float z);
extern "C" DLL_API int APIENTRY rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z);
extern "C" DLL_API int APIENTRY scaleModel(const unsigned int modelId, const float x, const float y, const float z);


extern "C" DLL_API unsigned int APIENTRY addText(const char* filename);
extern "C" DLL_API void APIENTRY setText(const unsigned int textId, const char* text);
extern "C" DLL_API void APIENTRY setTextPosition(const unsigned int textId, const int x, const int y);
extern "C" DLL_API int APIENTRY setTextSize(const unsigned int textId, const int points);
extern "C" DLL_API void APIENTRY setTextColor(const unsigned int textId, const float r, const float g, const float b, const float a);
//extern "C" DLL_API bool APIENTRY setFontFamily(const unsigned int textId, const char* filename);