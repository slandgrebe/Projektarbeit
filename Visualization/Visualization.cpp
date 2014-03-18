#include "Visualization.h"

namespace visual {
	int Visualization::doSomething(int n) { 
		unsigned int result = Manager::getInstance()->doSomething(n);
		return result;
	}

	unsigned int Visualization::addModel(System::String^ filename) {
		std::string stdfilename = msclr::interop::marshal_as<::std::string>(filename);
		unsigned int result = Manager::getInstance()->addModel(stdfilename);
		return result;
	}

	unsigned int Visualization::addPoint(void) {
		unsigned int result = Manager::getInstance()->addPoint();
		return result;
	}
	unsigned int Visualization::addPoint(const std::string textureFilename) {
		unsigned int result = Manager::getInstance()->addPoint(textureFilename);
		return result;
	}

	bool Visualization::isModelCreated(const unsigned int modelId) {
		GLboolean result = Manager::getInstance()->isModelCreated(modelId);
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool Visualization::positionModel(const unsigned int modelId, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->positionModel(modelId, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool Visualization::rotateModel(const unsigned int modelId, const float degrees, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->rotateModel(modelId, degrees, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}

	bool Visualization::scaleModel(const unsigned int modelId, const float x, const float y, const float z) {
		GLboolean result = Manager::getInstance()->scaleModel(modelId, glm::vec3(x, y, z));
		if (result == GL_TRUE) {
			return true;
		}
		return false;
	}
}