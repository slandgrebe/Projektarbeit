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

/** Hier sind alle Funktionen der Bibliothek definiert.
* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.
*/


DLL_API int APIENTRY isRunning() {
	// c++ bool in int umwandeln, damit c# bool damit umgehen kann
	return (int)visual::Manager::getInstance()->isRunning();
}

DLL_API void APIENTRY doSomething(const char* text) {
	//visual::VisualizationImpl::doSomething(text);
	visual::Manager::getInstance()->doSomething(text);
}

DLL_API unsigned int APIENTRY addModel(const char* filename) {
	//return visual::VisualizationImpl::addModel(filename);
	return visual::Manager::getInstance()->addModel(filename);
}
DLL_API unsigned int APIENTRY addPoint(const char* filename) {
	//return visual::VisualizationImpl::addPoint(filename);
	return visual::Manager::getInstance()->addPoint(filename);
}
DLL_API unsigned int APIENTRY addText(const char* filename) {
	return visual::Manager::getInstance()->addText(filename);
}
DLL_API unsigned int APIENTRY addButton(const char* fontname) {
	return visual::Manager::getInstance()->addButton(fontname);
}

DLL_API int APIENTRY isCreated(const unsigned int modelId) {
	//return (int)visual::VisualizationImpl::isModelCreated(modelId);
	return visual::Manager::getInstance()->isModelCreated(modelId);
}

DLL_API void APIENTRY dispose(const unsigned int modelId) {
	visual::Manager::getInstance()->dispose(modelId);
}

DLL_API int APIENTRY position(const unsigned int modelId, const float x, const float y, const float z) {
	//return (int)visual::VisualizationImpl::positionModel(modelId, x, y, z);
	return (int)visual::Manager::getInstance()->positionModel(modelId, glm::vec3(x, y, z));
}

DLL_API int APIENTRY rotate(const unsigned int modelId, const float degrees, const float x, const float y, const float z) {
	//return (int)visual::VisualizationImpl::rotateModel(modelId, degrees, x, y, z);
	return (int)visual::Manager::getInstance()->rotateModel(modelId, degrees, glm::vec3(x, y, z));
}

DLL_API int APIENTRY scale(const unsigned int modelId, const float x, const float y, const float z) {
	//return (int)visual::VisualizationImpl::scaleModel(modelId, x, y, z);
	return (int)visual::Manager::getInstance()->scaleModel(modelId, glm::vec3(x, y, z));
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



DLL_API void APIENTRY text(const unsigned int textId, const char* text) {
	visual::Manager::getInstance()->setText(textId, text);
}
DLL_API int APIENTRY textSize(const unsigned int textId, const int points) {
	return (int)visual::Manager::getInstance()->setTextSize(textId, points);
}
DLL_API void APIENTRY textColor(const unsigned int textId, const float r, const float g, const float b, const float a) {
	visual::Manager::getInstance()->setTextColor(textId, glm::vec4(r, g, b, a));
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

DLL_API unsigned int APIENTRY collisionsTextLength(void) {
	return visual::Manager::getInstance()->collisionsTextLength();
}
DLL_API void APIENTRY collisionsText(char* string, int length) {
	std::string collisions = visual::Manager::getInstance()->collisionsText().c_str();
	strcpy_s(string, length, collisions.c_str());
	//return visual::Manager::getInstance()->collisions().c_str();
}

#endif