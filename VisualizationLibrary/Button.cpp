#include "stdafx.h"
#include "Button.h"
#include <iostream>

using namespace visual::gui;

Button::Button() {
}

Button::~Button() {
}


bool Button::init(const std::string filename) {
	std::cout << "button init " << std::endl;

	square = new model::Square;
	if (!square->loadFromFile(filename)) {
		std::cout << "Could not create background for button." << std::endl;
		return false;
	}

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
	square->draw();
	text->draw();
}