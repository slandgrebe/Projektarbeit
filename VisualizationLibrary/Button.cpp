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

	square = new model::ColoredSquare;
	if (!square->loadModel()) {
		std::cout << "Could not create background for button." << std::endl;
		return false;
	}
	square->position(glm::vec3(0, 0, -2));

	text = new Text;
	if (!text->init("data/fonts/MADAVE.TTF")) {
		std::cout << "Could not create text for button" << std::endl;
	}
	text->setText("Klick mich");
	text->setPosition(100, 100);

	return true;
}
void Button::setText(const std::string text) {
	this->text->setText(text);
}
void Button::setHighlightColor(glm::vec4 color) {
	std::cout << "highlightcolor: " << color.r << "/" << color.g << "/" << color.b << "/" << color.a << std::endl;
	square->setHighlightColor(color);
}
void Button::isHighlighted(bool choice) {
	square->isHighlighted(choice);
}


void Button::draw() {
	square->draw();
	text->draw();
}