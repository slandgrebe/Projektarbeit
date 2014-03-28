#ifndef VISUALIZATIONIMPL_H
#define VISUALIZATIONIMPL_H

#include "stdafx.h"

#include "Visualization.h"

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

#include "Manager.h"

namespace visual {
	class VisualizationImpl {
	public:
		static void doSomething(std::string s);
		static unsigned int addModel(std::string/*System::String^*/);

		//static unsigned int addPoint(void); // default values mit managed code ist so eine sache ... http://stackoverflow.com/questions/15454394/why-c-cli-has-no-default-argument-on-managed-types
		static unsigned int addPoint(const std::string textureFilename = "data/textures/sample.png");

		static bool isModelCreated(const unsigned int modelId);

		static bool positionModel(const unsigned int modelId, const float x, const float y, const float z);
		static bool rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z);
		static bool scaleModel(const unsigned int modelId, const float x, const float y, const float z);

		static unsigned int addText(std::string text);
	};
}


// exportierte funktionen
DLL_API int APIENTRY isRunning() {
	// c++ bool in int umwandeln, damit c# bool damit umgehen kann
	return (int)visual::Manager::getInstance()->isRunning();
}

DLL_API void APIENTRY doSomething(const char* text) {
	visual::VisualizationImpl::doSomething(text);
}

DLL_API unsigned int APIENTRY addModel(const char* filename) {
	return visual::VisualizationImpl::addModel(filename);
}

DLL_API unsigned int APIENTRY addPoint(const char* filename) {
	return visual::VisualizationImpl::addPoint(filename);
}

DLL_API bool APIENTRY isModelCreated(const unsigned int modelId) {
	return visual::VisualizationImpl::isModelCreated(modelId);
}

DLL_API bool APIENTRY positionModel(const unsigned int modelId, const float x, const float y, const float z) {
	return visual::VisualizationImpl::positionModel(modelId, x, y, z);
}

DLL_API bool APIENTRY rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z) {
	return visual::VisualizationImpl::rotateModel(modelId, degrees, x, y, z);
}

DLL_API bool APIENTRY scaleModel(const unsigned int modelId, const float x, const float y, const float z) {
	return visual::VisualizationImpl::scaleModel(modelId, x, y, z);
}

DLL_API unsigned int APIENTRY addText(const char* text) {
	return visual::Manager::getInstance()->addText(text);
}



DLL_API void APIENTRY setText(const unsigned int textId, const char* text) {
	visual::Manager::getInstance()->setText(textId, text);
}
DLL_API void APIENTRY setTextPosition(const unsigned int textId, const int x, const int y) {
	visual::Manager::getInstance()->setTextPosition(textId, x, y);
}
DLL_API bool APIENTRY setTextSize(const unsigned int textId, const int points) {
	return visual::Manager::getInstance()->setTextSize(textId, points);
}
DLL_API void APIENTRY setTextColor(const unsigned int textId, const float r, const float g, const float b, const float a) {
	visual::Manager::getInstance()->setTextColor(textId, glm::vec4(r, g, b, a));
}
DLL_API bool APIENTRY setFontFamily(const unsigned int textId, const char* filename) {
	return visual::Manager::getInstance()->setFontFamily(textId, filename);
}

#endif