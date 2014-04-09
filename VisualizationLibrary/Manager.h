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
		GLuint addPoint(const ::std::string textureFilename = "sample.png");

		GLboolean isModelCreated(GLuint modelId);
		GLboolean positionModel(GLuint modelId, glm::vec3 position);
		GLboolean rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis);
		GLboolean scaleModel(GLuint modelId, glm::vec3 scale);
		bool setModelHighlightColor(GLuint modelId, glm::vec4 color);
		bool isModelHighlighted(GLuint modelId, bool choice);

		GLuint addText(const std::string text);
		bool setText(const GLuint textId, const std::string filename);
		//bool setTextPosition(const GLuint textId, const float x, const float y);
		bool setTextSize(const GLuint textId, const int points);
		bool setTextColor(const GLuint textId, const glm::vec4 color);
		//bool setFontFamily(const GLuint textId, const std::string filename);

		GLuint addButton(const std::string fontname);
		//void setButtonText(const GLuint buttonId, const std::string text);
		//void setButtonHighlightColor(GLuint buttonId, glm::vec4 color);
		//void isButtonHighlighted(GLuint buttonId, bool choice);

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

		visual::gui::Text* getTextFromList(GLuint textId);
		gui::Button* getButtonFromList(GLuint buttonId);
	};
}

#endif