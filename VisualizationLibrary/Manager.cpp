#include "stdafx.h"
#include "Manager.h"
#include <iostream>
#include <time.h>

#include "Text.h"

using namespace visual;

Manager* Manager::instance = 0;

Manager* Manager::getInstance(void) {
	if (instance == 0) {
		instance = new Manager;
	}

	return instance;
}

Manager::Manager() {
	clock_t begin = clock();
	while (true) {
		if (isRunning()) {
			return;
		}

		if (double(clock() - begin) / CLOCKS_PER_SEC > 5) {
			break;
		}
	}
}

bool Manager::isRunning(void) {
	return graphics::GraphicEngine::getInstance()->isRunning();
}

void Manager::doSomething(std::string s) {
	addText(s);
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
	else if (textList.find(modelId) != textList.end()) {
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
void Manager::addToTextList(GLuint modelId, gui::Text* text) {
	std::cout << " adding Text to List: " << modelId << std::endl;
	textList.insert(std::make_pair(modelId, text));
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


bool Manager::setModelHighlightColor(GLuint modelId, glm::vec4 color) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->setHighlightColor(color);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->setHighlightColor(color);

		return true;
	}
	else {
		std::cout << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch dessen Highlight Farbe zu setzen nicht gefunden werden." << std::endl;
	}

	return false;
}
bool Manager::isModelHighlighted(GLuint modelId, bool choice) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->isHighlighted(choice);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->isHighlighted(choice);

		return true;
	}
	else {
		std::cout << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch den Highlight Modus zu aendern nicht gefunden werden." << std::endl;
	}

	return false;
}


GLuint Manager::addText(const std::string filename) {
	if (isRunning()) {
		modelInstantiationCounter++;

		std::cout << " adding Text to Queue: " << modelInstantiationCounter << std::endl;

		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueText(modelInstantiationCounter, filename);

		return modelInstantiationCounter;
	}

	return 0;
}

visual::gui::Text* Manager::getTextFromList(GLuint textId) {
	visual::gui::Text* text = NULL;

	if (textList.find(textId) != textList.end()) {
		text = textList.find(textId)->second;
	}

	return text;
}
void Manager::setText(const GLuint textId, const std::string text) {
	visual::gui::Text* textObj = getTextFromList(textId);
	textObj->setText(text);
}
void Manager::setTextPosition(const GLuint textId, const int x, const int y) {
	visual::gui::Text* textObj = getTextFromList(textId);
	textObj->setPosition(x, y);
}
bool Manager::setTextSize(const GLuint textId, const int points) {
	visual::gui::Text* textObj = getTextFromList(textId);
	return textObj->setSize(points);
}
void Manager::setTextColor(const GLuint textId, const glm::vec4 color) {
	visual::gui::Text* textObj = getTextFromList(textId);
	textObj->setColor(color);
}
/*bool Manager::setFontFamily(const GLuint textId, const std::string filename) {
	visual::gui::Text* textObj = getTextFromList(textId);
	return textObj->setFontFamily(filename);
}*/


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

	// Texte immer zuoberst
	glClear(GL_DEPTH_BUFFER_BIT);
	
	// Texte zeichnen
	std::map<GLuint, gui::Text*>::iterator it3;
	for (it3 = textList.begin(); it3 != textList.end(); it3++) {
		gui::Text* text = (*it3).second;
		text->draw();
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