#ifndef VISUALIZATIONIMPL_H
#define VISUALIZATIONIMPL_H

#include "stdafx.h"
/*#include "Visualization.h"

class VisualizationImpl {
public:
	static void hello();
};

#if !defined(_WIN64)
// This pragma is required only for 32-bit builds. In a 64-bit environment,
// C functions are not decorated.
#pragma comment(linker, "/export:hello=_hello@0")
#endif  // _WIN64


DLL_API void WINAPI hello(void) {
	VisualizationImpl::hello();
}*/



#include "Visualization.h"

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

#include "Manager.h"

namespace visual {
	class VisualizationImpl {
	public:
		static int doSomething(int n);
		static unsigned int addModel(std::string/*System::String^*/);

		static unsigned int addPoint(void); // default values mit managed code ist so eine sache ... http://stackoverflow.com/questions/15454394/why-c-cli-has-no-default-argument-on-managed-types
		static unsigned int addPoint(const std::string textureFilename);

		static bool isModelCreated(const unsigned int modelId);

		static bool positionModel(const unsigned int modelId, const float x, const float y, const float z);
		static bool rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z);
		static bool scaleModel(const unsigned int modelId, const float x, const float y, const float z);

	};
}


// exportierte funktionen
DLL_API void APIENTRY doSomething(int n) {
	visual::VisualizationImpl::doSomething(n);
}

#endif