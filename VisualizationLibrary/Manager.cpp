#include "stdafx.h"
#include "Manager.h"
#include <iostream>
#include <time.h>

using namespace visual;

Manager* Manager::instance = 0;

Manager* Manager::getInstance(void) {
	if (instance == 0) {
		instance = new Manager;
	}

	return instance;
}

Manager::Manager() {
	isRunning();
}

GLboolean Manager::isRunning(void) {
	clock_t begin = clock();
	while (true) {
		if (graphics::GraphicEngine::getInstance()->isRunning()) {
			return true;
		}

		if (double(clock() - begin) / CLOCKS_PER_SEC > 5) {
			break;
		}
	}

	return false;
}

void Manager::doSomething(std::string s) {
	/*for (int i = 0; i < 100; i++) {
		std::cout << i;
	}

	std::cout << std::endl;*/

	std::cout << "doSomething: " << s << std::endl;
	addModel("data/models/shuttle/SpaceShuttleOrbiter.3ds");
}

GLuint Manager::addModel(const std::string filename) {
	if (isRunning()) {
		modelInstantiationCounter++;

		std::cout << " adding Model to Queue: " << modelInstantiationCounter << std::endl;

		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueModel(modelInstantiationCounter, filename);

		return modelInstantiationCounter;
	}

	return 0;
}

GLuint Manager::addPoint(const std::string textureFilename) {
	if (isRunning()) {
		modelInstantiationCounter++;

		std::cout << " adding Point to Queue: " << modelInstantiationCounter << std::endl;
		
		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueSquare(modelInstantiationCounter, textureFilename);

		return modelInstantiationCounter;
	}

	return 0;
}

GLboolean Manager::isModelCreated(GLuint modelId) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		return GL_TRUE;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		return GL_TRUE;
	}

	return GL_FALSE;
}

void Manager::addToModelList(GLuint modelId, model::AssimpModel* model) {
	std::cout << " adding Model to List: " << modelId << std::endl;
	assimpModelList.insert(std::make_pair(modelId, model));
}
void Manager::addToSquareList(GLuint modelId, model::Square* model) {
	std::cout << " adding Square to List: " << modelId << std::endl;
	squareList.insert(std::make_pair(modelId, model));
}

GLboolean Manager::positionModel(GLuint modelId, glm::vec3 position) {
	//processQueue();
	
	for (auto it = assimpModelList.cbegin(); it != assimpModelList.cend(); ++it)
	{
		//std::cout << it->first << " " << it->second << " " << "\n";
	}

	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->position(position);
		
		return GL_TRUE;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->position(position);

		return GL_TRUE;
	}
	else {
		//std::cout << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu positionieren nicht gefunden werden." << std::endl;
	}

	return GL_FALSE;
}

GLboolean Manager::rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->rotate(degrees, axis);

		return GL_TRUE;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->rotate(degrees, axis);

		return GL_TRUE;
	}
	else {
		std::cout << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu rotieren nicht gefunden werden." << std::endl;
	}

	return GL_FALSE;
}

GLboolean Manager::scaleModel(GLuint modelId, glm::vec3 scale) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->scale(scale);

		return GL_TRUE;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->scale(scale);

		return GL_TRUE;
	}
	else {
		std::cout << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu skalieren nicht gefunden werden." << std::endl;
	}

	return GL_FALSE;
}

void Manager::draw(void) {
	if (!isRunning()) {
		std::cout << "Manager: GraphicsEngine ist nicht in einem laufenden Zustand. Neuzeichnen abgebrochen." << std::endl;
		return;
	}
	
	// Modelle zeichnen
	std::map<GLuint, model::AssimpModel*>::iterator it;
	for (it = assimpModelList.begin(); it != assimpModelList.end(); it++) {
		model::AssimpModel* model = (*it).second;
		model->draw();
	}

	// Punkte zeichnen
	std::map<GLuint, model::Square*>::iterator it2;
	for (it2 = squareList.begin(); it2 != squareList.end(); it2++) {
		model::Square* model = (*it2).second;
		model->draw();
	}
	
	
	/*if (isRunning()) {
		if (!square) {
			square = new model::Square;
			if (square->loadModel()) {
				square->position(glm::vec3(0.25f, 0.25f, 0.5f));
				square->rotate(30.0f, glm::vec3(0.0f, 1.0f, 0.0f));
				square->scale(glm::vec3(0.5f, 0.5f, 0.5f));
			}
			else {
				delete square;
				square = 0;
			}
		}

		if (!assimpModel) {
			assimpModel = new model::AssimpModel;
			if (assimpModel->loadModel("earth.q3o")) {
				assimpModel->position(glm::vec3(-0.25f, -0.25f, -0.5f));
				assimpModel->rotate(20.0f, glm::vec3(0.0f, 1.0f, 0.0f));
				assimpModel->scale(glm::vec3(0.005f, 0.005f, 0.005f));
			}
			else {
				delete assimpModel;
				assimpModel = 0;
			}
		}
	}*/

	/*if (square) square->draw();
	if (assimpModel) assimpModel->draw();*/
}