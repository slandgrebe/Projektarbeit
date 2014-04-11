#pragma once

//#include "stdafx.h"

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
extern "C" DLL_API unsigned int APIENTRY addText(const char* filename);
extern "C" DLL_API unsigned int APIENTRY addButton(const char* fontname);

extern "C" DLL_API int APIENTRY isCreated(const unsigned int modelId);

extern "C" DLL_API void APIENTRY dispose(const unsigned int modelId);

extern "C" DLL_API int APIENTRY position(const unsigned int modelId, const float x, const float y, const float z);
extern "C" DLL_API int APIENTRY rotate(const unsigned int modelId, const float degrees, const float x, const float y, const float z);
extern "C" DLL_API int APIENTRY scale(const unsigned int modelId, const float x, const float y, const float z);
extern "C" DLL_API int APIENTRY highlightColor(const unsigned int modelId, const float r, const float g, const float b, const float a);
extern "C" DLL_API int APIENTRY isHighlighted(const unsigned int modelId, const bool choice);

extern "C" DLL_API void APIENTRY text(const unsigned int textId, const char* text);
extern "C" DLL_API int APIENTRY textSize(const unsigned int textId, const int points);
extern "C" DLL_API void APIENTRY textColor(const unsigned int textId, const float r, const float g, const float b, const float a);

extern "C" DLL_API void APIENTRY positionCamera(float x, float y, float z);
extern "C" DLL_API void APIENTRY rotateCamera(float degrees);
extern "C" DLL_API void APIENTRY tiltCamera(float degrees);
extern "C" DLL_API void APIENTRY changeCameraSpeed(float speed);