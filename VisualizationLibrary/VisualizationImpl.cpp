#include "stdafx.h"
#include "VisualizationImpl.h"

namespace visual {
	void VisualizationImpl::doSomething(std::string s) {
		Manager::getInstance()->doSomething(s);
	}

	unsigned int VisualizationImpl::addModel(std::string/*System::String^*/ filename) {
		//std::string stdfilename = msclr::interop::marshal_as<::std::string>(filename);
		unsigned int result = Manager::getInstance()->addModel(filename);
		return result;
	}

	unsigned int VisualizationImpl::addPoint(void) {
		unsigned int result = Manager::getInstance()->addPoint();
		return result;
	}
	unsigned int VisualizationImpl::addPoint(const std::string textureFilename) {
		unsigned int result = Manager::getInstance()->addPoint(textureFilename);
		return result;
	}

	bool VisualizationImpl::isModelCreated(const unsigned int modelId) {
		GLboolean result = Manager::getInstance()->isModelCreated(modelId);
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool VisualizationImpl::positionModel(const unsigned int modelId, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->positionModel(modelId, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool VisualizationImpl::rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->rotateModel(modelId, degrees, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool VisualizationImpl::scaleModel(const unsigned int modelId, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->scaleModel(modelId, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}
}