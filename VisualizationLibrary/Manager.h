#ifndef MANAGER_H
#define MANAGER_H

#include "Square.h"
#include "AssimpModel.h"
#include "Text.h"
#include "Button.h"
#include "GraphicEngine.h"
#include <map>

namespace visual {
	class Manager {
	public:
		static Manager* getInstance(void);

		bool isRunning(void);

		void addToModelList(GLuint modelId, model::AssimpModel* model);
		void addToSquareList(GLuint modelId, model::Square* model);
		void addToTextList(GLuint textId, gui::Text* text);
		void addToButtonList(GLuint buttonId, gui::Button* button);

		void doSomething(std::string s);
		GLuint addModel(const ::std::string filename);
		GLuint addPoint(const ::std::string textureFilename);
		GLuint addText(const std::string text);
		GLuint addButton(const std::string fontname);

		GLboolean isModelCreated(GLuint modelId);

		bool dispose(GLuint modelId);
		
		GLboolean positionModel(GLuint modelId, glm::vec3 position);
		GLboolean rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis);
		GLboolean scaleModel(GLuint modelId, glm::vec3 scale);
		bool setModelHighlightColor(GLuint modelId, glm::vec4 color);
		bool isModelHighlighted(GLuint modelId, bool choice);

		bool setText(const GLuint textId, const std::string filename);
		bool setTextSize(const GLuint textId, const int points);
		bool setTextColor(const GLuint textId, const glm::vec4 color);

		bool setCameraInMotion(glm::vec3 orientation, float speed);

		void draw(void);

	private:
		static Manager* instance;

		GLuint modelInstantiationCounter = 0;
		std::map<GLuint, model::AssimpModel*> assimpModelList;
		std::map<GLuint, model::Square*> squareList;
		std::map<GLuint, gui::Text*> textList;
		std::map<GLuint, gui::Button*> buttonList;

		model::Square* square = 0;
		model::AssimpModel* assimpModel = 0;

		Manager(void);

		gui::Text* getTextFromList(GLuint textId);
		gui::Button* getButtonFromList(GLuint buttonId);
	};
}

#endif