#ifndef VISUALIZATIONIMPL_H
#define VISUALIZATIONIMPL_H

//#include "stdafx.h"

#include "Visualization.h"

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

#include "Manager.h"

// exportierte funktionen

extern "C" DLL_API int APIENTRY init(const char* windowTitle, bool fullscreen, unsigned int windowWidth, unsigned int windowHeight);
DLL_API int APIENTRY init(const char* windowTitle, bool fullscreen, unsigned int windowWidth, unsigned int windowHeight) {
	return (int)visual::Manager::getInstance()->init(windowTitle, fullscreen, windowWidth, windowHeight);
}
DLL_API int APIENTRY isRunning() {
	return (int)visual::Manager::getInstance()->isRunning();
}
DLL_API void APIENTRY close() {
	visual::Manager::getInstance()->close();
}

DLL_API void APIENTRY doSomething(const char* text) {
	visual::Manager::getInstance()->doSomething(text);
}

DLL_API unsigned int APIENTRY addModel(const char* filename) {
	return visual::Manager::getInstance()->addModel(filename);
}
DLL_API unsigned int APIENTRY addPoint(const char* filename) {
	return visual::Manager::getInstance()->addPoint(filename);
}
DLL_API unsigned int APIENTRY addText(const char* filename) {
	return visual::Manager::getInstance()->addText(filename);
}
DLL_API unsigned int APIENTRY addButton(const char* fontname) {
	return visual::Manager::getInstance()->addButton(fontname);
}

DLL_API int APIENTRY isCreated(const unsigned int modelId) {
	return visual::Manager::getInstance()->isModelCreated(modelId);
}
DLL_API void APIENTRY dispose(const unsigned int modelId) {
	visual::Manager::getInstance()->dispose(modelId);
}

DLL_API int APIENTRY position(const unsigned int modelId, const float x, const float y, const float z) {
	return (int)visual::Manager::getInstance()->positionModel(modelId, glm::vec3(x, y, z));
}
DLL_API float APIENTRY positionX(const unsigned int modelId) {
	return visual::Manager::getInstance()->modelPosition(modelId).x;
}
DLL_API float APIENTRY positionY(const unsigned int modelId) {
	return visual::Manager::getInstance()->modelPosition(modelId).y;
}
DLL_API float APIENTRY positionZ(const unsigned int modelId) {
	return visual::Manager::getInstance()->modelPosition(modelId).z;
}
DLL_API int APIENTRY rotate(const unsigned int modelId, const float degrees, const float x, const float y, const float z) {
	return (int)visual::Manager::getInstance()->rotateModel(modelId, degrees, glm::vec3(x, y, z));
}
DLL_API int APIENTRY scale(const unsigned int modelId, const float x, const float y, const float z) {
	return (int)visual::Manager::getInstance()->scaleModel(modelId, glm::vec3(x, y, z));
}
DLL_API int APIENTRY scalingIsNormalized(const unsigned int modelId, bool choice) {
	return (int)visual::Manager::getInstance()->scalingIsNormalized(modelId, choice);
}
DLL_API int APIENTRY highlightColor(const unsigned int modelId, const float r, const float g, const float b, const float a) {
	return (int)visual::Manager::getInstance()->setModelHighlightColor(modelId, glm::vec4(r, g, b, a));
}
DLL_API int APIENTRY isHighlighted(const unsigned int modelId, const bool choice) {
	return (int)visual::Manager::getInstance()->isModelHighlighted(modelId, choice);
}
DLL_API int APIENTRY attachToCamera(const unsigned int modelId, const bool choice) {
	return (int)visual::Manager::getInstance()->attachModelToCamera(modelId, choice);
}
DLL_API int APIENTRY setModelVisibility(const unsigned int modelId, const bool choice) {
	return (int)visual::Manager::getInstance()->modelVisibility(modelId, choice);
}
DLL_API int APIENTRY getModelVisibility(const unsigned int modelId) {
	return (int)visual::Manager::getInstance()->modelVisibility(modelId);
}



DLL_API int APIENTRY text(const unsigned int textId, const char* text) {
	return (int)visual::Manager::getInstance()->setText(textId, text);
}
DLL_API int APIENTRY textSize(const unsigned int textId, const int points) {
	return (int)visual::Manager::getInstance()->setTextSize(textId, points);
}
DLL_API int APIENTRY textColor(const unsigned int textId, const float r, const float g, const float b, const float a) {
	return (int)visual::Manager::getInstance()->setTextColor(textId, glm::vec4(r, g, b, a));
}

DLL_API void APIENTRY positionCamera(float x, float y, float z) {
	visual::Manager::getInstance()->positionCamera(glm::vec3(x, y, z));
}
DLL_API void APIENTRY rotateCamera(float degrees) {
	visual::Manager::getInstance()->rotateCamera(degrees);
}
DLL_API void APIENTRY tiltCamera(float degrees) {
	visual::Manager::getInstance()->tiltCamera(degrees);
}
DLL_API void APIENTRY changeCameraSpeed(float speed) {
	visual::Manager::getInstance()->changeCameraSpeed(speed);
}

DLL_API int APIENTRY addCollisionModel(const unsigned int modelId, const char* filename) {
	//return visual::Manager::getInstance()->addCollisionModel(modelId, filename);
	visual::Manager::getInstance()->addCollisionModel(modelId, filename);
	return (int)true;
}
DLL_API int APIENTRY collisionGroup(const unsigned int modelId, const unsigned int collisionGroup) {
	return visual::Manager::getInstance()->collisionGroup(modelId, collisionGroup);
}
DLL_API unsigned int APIENTRY collisionsTextLength(void) {
	return visual::Manager::getInstance()->collisionsTextLength();
}
DLL_API int APIENTRY collisionsText(char* string, int length) {
	std::string collisions = visual::Manager::getInstance()->collisionsText().c_str();
	
	if ((int)collisions.length() > length) {
		return (int)false;
	}
	
	strcpy_s(string, length, collisions.c_str());
	return (int)true;
}

#endif