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
	Log::ReportingLevel() = logINFO;
	m_collisions = "";

	/*clock_t begin = clock();
	while (true) {
		if (isRunning()) {
			return;
		}

		if (double(clock() - begin) / CLOCKS_PER_SEC > 5) {
			break;
		}
	}*/
}

bool Manager::init(std::string windowTitle, bool fullscreen, unsigned int windowWidth, unsigned int windowHeight) {	
	bool result = graphics::GraphicEngine::getInstance()->init(windowTitle, fullscreen, windowWidth, windowHeight);

	if (result) {
		clock_t begin = clock();
		while (true) {
			if (isRunning()) {
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 5) {
				break;
			}
		}
	}
	
	Log().fatal() << "Bibliothek konnte nicht initialisiert werden.";

	return false;
}
bool Manager::isRunning(void) {
	return graphics::GraphicEngine::getInstance()->isRunning();
}
void Manager::close() {
	graphics::GraphicEngine::getInstance()->close();

	clock_t begin = clock();
	while (isRunning()) {
		if (double(clock() - begin) / CLOCKS_PER_SEC > 10) {
			Log().error() << "Fenster konnte nicht innerhalb von 5s geschlossen werden.";
			break;
		}
	}
}

void Manager::doSomething(std::string s) {
	addText(s);
}


GLuint Manager::addModel(const std::string filename) {
	if (!isRunning()) {
		init();
	}
	if (isRunning()) {
		modelInstantiationCounter++;

		Log().debug() << " adding Model to Queue: " << modelInstantiationCounter ;

		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueModel(modelInstantiationCounter, filename);

		return modelInstantiationCounter;
	}

	return 0;
}


GLuint Manager::addPoint(const std::string textureFilename) {
	if (!isRunning()) {
		Log().debug() << "Window not open. Trying to open it";
		bool result = init();
		Log().debug() << "Result: " << result;
	}
	if (isRunning()) {
		modelInstantiationCounter++;

		Log().debug() << " adding Point to Queue: " << modelInstantiationCounter ;
		
		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueSquare(modelInstantiationCounter, textureFilename);

		return modelInstantiationCounter;
	}

	return 0;
}


bool Manager::isModelCreated(GLuint modelId) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		return true;
	}
	else if (textList.find(modelId) != textList.end()) {
		return true;
	}
	else if (buttonList.find(modelId) != buttonList.end()) {
		return true;
	}

	return false;
}


void Manager::dispose(GLuint modelId) {
	graphics::GraphicEngine::getInstance()->enqueueDispose(modelId);
}


void Manager::remove(GLuint modelId) {
	std::map<GLuint, visual::model::AssimpModel*>::iterator modelIterator = assimpModelList.find(modelId);
	std::map<GLuint, visual::model::Square*>::iterator squareIterator = squareList.find(modelId);
	std::map<GLuint, visual::gui::Text*>::iterator textIterator = textList.find(modelId);
	std::map<GLuint, visual::gui::Button*>::iterator buttonIterator = buttonList.find(modelId);

	if (modelIterator != assimpModelList.end()) {
		model::AssimpModel* model = modelIterator->second;

		// aus collisionGroup entfernen
		switch (model->collisionGroup()) {
			case collisionGroup::ambiente:
				// ignore
				break;
			case collisionGroup::player:
				collisionGroupPlayer.remove(modelId);
				break;
			case collisionGroup::obstacle:
				collisionGroupObstacle.remove(modelId);
				break;
			case collisionGroup::bonus:
				collisionGroupBonus.remove(modelId);
				break;
		}

		delete model;
		assimpModelList.erase(modelIterator);
	}
	else if (squareIterator != squareList.end()) {
		model::Square* model = squareIterator->second;
		delete model;
		squareList.erase(squareIterator);
	}
	else if (textIterator != textList.end()) {
		gui::Text* model = textIterator->second;
		delete model;
		textList.erase(textIterator);
	}
	else if (buttonIterator != buttonList.end()) {
		gui::Button* model = buttonIterator->second;
		delete model;
		buttonList.erase(buttonIterator);
	}
	else {
		Log().warning() << "Model mit der ID '" << modelId << "' konnte nicht entfernt werden";
	}
}


void Manager::addToModelList(GLuint modelId, model::AssimpModel* model) {
	Log().debug() << " adding Model to List: " << modelId ;
	assimpModelList.insert(std::make_pair(modelId, model));
}

void Manager::addToSquareList(GLuint modelId, model::Square* model) {
	Log().debug() << " adding Square to List: " << modelId ;
	squareList.insert(std::make_pair(modelId, model));
}

void Manager::addToTextList(GLuint modelId, gui::Text* text) {
	Log().debug() << " adding Text to List: " << modelId ;
	textList.insert(std::make_pair(modelId, text));
}

void Manager::addToButtonList(GLuint buttonId, gui::Button* button) {
	Log().debug() << " adding Button to List: " << buttonId ;
	buttonList.insert(std::make_pair(buttonId, button));
}


bool Manager::positionModel(GLuint modelId, glm::vec3 position) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->position(position);
		
		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->position(position);

		return true;
	}
	else if (textList.find(modelId) != textList.end()) {
		gui::Text* model = textList.find(modelId)->second;
		model->setPosition(position.x, position.y);

		return true;
	}
	else if (buttonList.find(modelId) != buttonList.end()) {
		gui::Button* model = buttonList.find(modelId)->second;
		model->position(glm::vec2(position));

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu positionieren nicht gefunden werden." ;
	}

	return false;
}

glm::vec3 Manager::modelPosition(GLuint modelId) {
	glm::vec3 position = glm::vec3(0, 0, 0);

	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		position = model->position();
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		position = model->position();
	}
	else if (textList.find(modelId) != textList.end()) {
		gui::Text* model = textList.find(modelId)->second;
		position = glm::vec3(model->getPosition(), 0);
	}
	else if (buttonList.find(modelId) != buttonList.end()) {
		gui::Button* model = buttonList.find(modelId)->second;
		position = model->position();
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch dessen Position zu ermitteln nicht gefunden werden.";
	}

	return position;
}

bool Manager::rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->rotate(degrees, axis);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->rotate(degrees, axis);

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu rotieren nicht gefunden werden." ;
	}

	return false;
}


bool Manager::scaleModel(GLuint modelId, glm::vec3 scale) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->scale(scale);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->scale(scale);

		return true;
	}
	else if (buttonList.find(modelId) != buttonList.end()) {
		gui::Button* model = buttonList.find(modelId)->second;
		model->scale(glm::vec2(scale));

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es neu zu skalieren nicht gefunden werden." ;
	}

	return false;
}
bool Manager::scalingIsNormalized(GLuint modelId, bool choice) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->scalingIsNormalized(choice);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->scalingIsNormalized(choice);

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch den Zustand der Skalierungsnormalisierung zu ändern nicht gefunden werden.";
	}

	return false;
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
	else if (buttonList.find(modelId) != buttonList.end()) {
		gui::Button* model = buttonList.find(modelId)->second;
		model->setHighlightColor(color);

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch dessen Highlight Farbe zu setzen nicht gefunden werden." ;
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
	else if (buttonList.find(modelId) != buttonList.end()) {
		gui::Button* model = buttonList.find(modelId)->second;
		model->isHighlighted(choice);

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch den Highlight Modus zu aendern nicht gefunden werden." ;
	}

	return false;
}


bool Manager::attachModelToCamera(GLuint modelId, bool choice) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;
		model->attachedToCamera(choice);

		return true;
	}
	else if (squareList.find(modelId) != squareList.end()) {
		model::Square* model = squareList.find(modelId)->second;
		model->attachedToCamera(choice);

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch es an die Kamera anzubinden nicht gefunden werden.";
	}

	return false;
}


GLuint Manager::addText(const std::string fontname) {
	if (!isRunning()) {
		init();
	}
	if (isRunning()) {
		modelInstantiationCounter++;

		Log().debug() << " adding Text to Queue: " << modelInstantiationCounter ;

		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueText(modelInstantiationCounter, fontname);

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

bool Manager::setText(const GLuint textId, const std::string text) {
	visual::gui::Text* textObj = getTextFromList(textId);
	if (textObj) {
		textObj->setText(text);
		return true;
	}
	
	gui::Button* button = getButtonFromList(textId);
	if (button) {
		button->setText(text);
		return true;
	}

	return false;
}

bool Manager::setTextSize(const GLuint textId, const int points) {
	visual::gui::Text* textObj = getTextFromList(textId);
	if (textObj) {
		return textObj->setSize(points);
	}
	
	gui::Button* button = getButtonFromList(textId);
	if (button) {
		return button->setTextSize(points);
	}

	return false;
}

bool Manager::setTextColor(const GLuint textId, const glm::vec4 color) {
	visual::gui::Text* textObj = getTextFromList(textId);
	if (textObj) {
		textObj->setColor(color);
		return true;
	}

	gui::Button* button = getButtonFromList(textId);
	if (button) {
		button->setTextColor(color);
		return true;
	}

	return false;
}


GLuint Manager::addButton(const std::string filename) {
	if (!isRunning()) {
		init();
	}
	if (isRunning()) {
		modelInstantiationCounter++;

		Log().debug() << " adding Button to Queue: " << modelInstantiationCounter ;

		// Queue für Thread Sicherheit
		graphics::GraphicEngine::getInstance()->enqueueButton(modelInstantiationCounter, filename);

		return modelInstantiationCounter;
	}

	return 0;
}
gui::Button* Manager::getButtonFromList(GLuint buttonId) {
	gui::Button* button = NULL;

	if (buttonList.find(buttonId) != buttonList.end()) {
		button = buttonList.find(buttonId)->second;
	}

	return button;
}

void Manager::positionCamera(glm::vec3 position) {
	graphics::GraphicEngine::getInstance()->camera()->position(position);
}
void Manager::rotateCamera(float degrees) {
	graphics::GraphicEngine::getInstance()->camera()->rotate(degrees);
}
void Manager::tiltCamera(float degrees) {
	graphics::GraphicEngine::getInstance()->camera()->tilt(degrees);
}
void Manager::changeCameraSpeed(float speed) {
	graphics::GraphicEngine::getInstance()->camera()->changeSpeed(speed);
}

bool Manager::setCollisionGroup(GLuint modelId, unsigned int newCollisionGroup) {
	if (assimpModelList.find(modelId) != assimpModelList.end()) {
		model::AssimpModel* model = assimpModelList.find(modelId)->second;

		if (model->collisionGroup() != newCollisionGroup) {
			
			// aus der alten gruppe entfernen
			switch (model->collisionGroup()) {
				case collisionGroup::ambiente:
					// ignore
					break;
				case collisionGroup::player: 
					collisionGroupPlayer.remove(modelId);
					break;
				case collisionGroup::obstacle:
					collisionGroupObstacle.remove(modelId);
					break;
				case collisionGroup::bonus:
					collisionGroupBonus.remove(modelId);
					break;
			}

			// der neuen gruppe hinzufügen
			switch (newCollisionGroup) {
				case collisionGroup::ambiente:
					// ignore
					break;
				case collisionGroup::player:
					collisionGroupPlayer.push_back(modelId);
					break;
				case collisionGroup::obstacle:
					collisionGroupObstacle.push_back(modelId);
					break;
				case collisionGroup::bonus:
					collisionGroupBonus.push_back(modelId);
					break;
			}

			model->collisionGroup(newCollisionGroup);
		}

		return true;
	}
	else {
		Log().error() << "Das Model mit der modelId '" << modelId << "' konnte waehrend dem Versuch dessen Kollisionsgruppe zu setzen nicht gefunden werden oder es handelt sich dabei nicht um ein 3D Modell.";
	}

	return false;
}

void Manager::doCollisionDetection(long unsigned int frame) {
	clock_t begin = clock();
	
	// 21;35
	std::stringstream collisions;
	
	GLuint collisionWithObstacle = 0;
	GLuint collisionWithBonus = 0;

	if (collisionGroupPlayer.size() > 0) {
		Log().info() << "pos z: " << assimpModelList[collisionGroupPlayer.front()]->position().z;
	}

	// collison Model aller Models der Collision group collisionGroupPlayer neu berechnen
	clock_t begin2 = clock();
	for (std::list<GLuint>::const_iterator itPlayer = collisionGroupPlayer.begin(), end = collisionGroupPlayer.end(); itPlayer != end; ++itPlayer) {
		model::AssimpModel* player = assimpModelList[*itPlayer];
		player->updateCollisionModel(frame);
	}
	Log().info() << "update all player models(" << collisionGroupPlayer.size() << ") " << float(clock() - begin2) << "ms. " << this->m_collisions;

	begin2 = clock();
	for (std::map<GLuint, model::AssimpModel*>::const_iterator itModel = assimpModelList.begin(), end = assimpModelList.end(); itModel != end; ++itModel) {
		model::AssimpModel* model = itModel->second;
		model->updateCollisionModel(frame);
	}
	Log().info() << "update all models(" << assimpModelList.size() << ") " << float(clock() - begin2) << "ms. " << this->m_collisions;
	Log().info() << "1 Collision Detection in " << float(clock() - begin) << "ms. " << this->m_collisions;
	// kollision mit hindernis suchen
	for (std::list<GLuint>::const_iterator itPlayer = collisionGroupPlayer.begin(), endPlayer = collisionGroupPlayer.end(); itPlayer != endPlayer; ++itPlayer) {
		model::AssimpModel* player = assimpModelList[*itPlayer];

		for (std::list<GLuint>::const_iterator itObstacles = collisionGroupObstacle.begin(), endObstacle = collisionGroupObstacle.end(); itObstacles != endObstacle; ++itObstacles) {
			model::AssimpModel* obstacle = assimpModelList[*itObstacles];

			if (player->doesIntersect(obstacle, frame)) { // exaktere prüfung
				collisionWithObstacle = *itObstacles;
				Log().info() << *itPlayer << " " << *itObstacles;
				break;
			}
		}

		if (collisionWithObstacle != 0) {
			break;
		}
	}
	Log().info() << "2 Collision Detection in " << float(clock() - begin) << "ms. " << this->m_collisions;

	// kollision mit bonus suchen
	for (std::list<GLuint>::const_iterator itPlayer = collisionGroupPlayer.begin(), endPlayer = collisionGroupPlayer.end(); itPlayer != endPlayer; ++itPlayer) {
		model::AssimpModel* player = assimpModelList[*itPlayer];

		for (std::list<GLuint>::const_iterator itBonus = collisionGroupBonus.begin(), endBonus = collisionGroupBonus.end(); itBonus != endBonus; ++itBonus) {
			model::AssimpModel* bonus = assimpModelList[*itBonus];

			if (player->doesIntersect(bonus, frame)) { // exaktere prüfung
				collisionWithBonus = *itBonus;
				Log().info() << *itPlayer << " " << *itBonus;
				break;
			}
		}

		if (collisionWithBonus != 0) {
			break;
		}
	}
	Log().info() << "3 Collision Detection in " << float(clock() - begin) << "ms. " << this->m_collisions;

	// collision text generieren
	if (collisionWithObstacle != 0) {
		collisions << collisionWithObstacle;
	}
	if (collisionWithBonus != 0) {
		if (collisionWithObstacle != 0) {
			collisions << ";";
		}
		collisions << collisionWithBonus;
	}

	this->m_collisions = collisions.str();
	Log().info() << "Collision Detection in " << float(clock() - begin) << "ms. " << this->m_collisions;
}
unsigned int Manager::collisionsTextLength(void) {
	m_collisionsCache = m_collisions;
	return m_collisionsCache.length();
}
std::string Manager::collisionsText(void) {
	return m_collisionsCache;
}

void Manager::draw(void) {
	if (!isRunning()) {
		Log().error() << "Manager: GraphicEngine ist nicht in einem laufenden Zustand. Neuzeichnen abgebrochen." ;
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

	// GUI immer zuoberst
	glClear(GL_DEPTH_BUFFER_BIT);
	
	// Texte zeichnen
	std::map<GLuint, gui::Text*>::iterator it3;
	for (it3 = textList.begin(); it3 != textList.end(); it3++) {
		gui::Text* text = (*it3).second;
		text->draw();
	}

	// Buttons zeichnen
	std::map<GLuint, gui::Button*>::iterator it4;
	for (it4 = buttonList.begin(); it4 != buttonList.end(); it4++) {
		gui::Button* button = (*it4).second;
		button->draw();
	}
}