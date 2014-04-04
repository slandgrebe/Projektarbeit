#include "stdafx.h"
#include "Button.h"
#include <iostream>

using namespace visual::gui;

Button::Button() {
}

Button::~Button() {
}


bool Button::init(void) {
	std::cout << "button init " << std::endl;

	square = new model::Square;
	if (!square->load()) {
		std::cout << "Could not create background for button." << std::endl;
		return false;
	}
	square->position(glm::vec3(0, 0, -2));

	text = new Text;
	if (!text->init("data/fonts/MADAVE.TTF")) {
		std::cout << "Could not create text for button" << std::endl;
	}
	text->setText("Klick mich");


	return true;
}
void Button::setText(const std::string text) {
	this->text->setText(text);
}
void Button::setHighlightColor(glm::vec4 color) {
	square->setHighlightColor(color);
}
void Button::isHighlighted(bool choice) {
	square->isHighlighted(choice);
}


void Button::draw() {
	std::cout << "button draw 1 " << std::endl;
	square->draw();
	std::cout << "button draw 2 " << std::endl;
	text->draw();
	std::cout << "button draw 3" << std::endl;
}