// Visualization.h

#ifndef VISUALIZATIONWRAPPER_H
#define VISUALIZATIONWRAPPER_H

#include "msclr\marshal_cppstd.h"

namespace view {
	public ref class VisualizationWrapper {
	public:
		static int doSomething(int n);
		/*static unsigned int addModel(System::String^);

		static unsigned int addPoint(void); // default values mit managed code ist so eine sache ... http://stackoverflow.com/questions/15454394/why-c-cli-has-no-default-argument-on-managed-types
		static unsigned int addPoint(const std::string textureFilename);

		static bool isModelCreated(const unsigned int modelId);

		static bool positionModel(const unsigned int modelId, const float x, const float y, const float z);
		static bool rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z);
		static bool scaleModel(const unsigned int modelId, const float x, const float y, const float z);*/

	};
}

#endif