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




DLL_API void APIENTRY text(const unsigned int textId, const char* text) {
	visual::Manager::getInstance()->setText(textId, text);
}
DLL_API int APIENTRY textSize(const unsigned int textId, const int points) {
	return (int)visual::Manager::getInstance()->setTextSize(textId, points);
}
DLL_API void APIENTRY textColor(const unsigned int textId, const float r, const float g, const float b, const float a) {
	visual::Manager::getInstance()->setTextColor(textId, glm::vec4(r, g, b, a));
}



DLL_API int APIENTRY cameraPosition(float x, float y, float z) {
	return (int)false;
}
/*DLL_API int APIENTRY cameraPosition(void) {
	return (int)false;
}*/
DLL_API int APIENTRY setCameraInMotion(float orientationX, float orientationY, float orientationZ, float speed) {
	return (int)visual::Manager::getInstance()->setCameraInMotion(glm::vec3(orientationX, orientationY, orientationZ), speed);
}

#endif