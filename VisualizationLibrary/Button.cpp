#include "stdafx.h"
#include "Button.h"
#include <iostream>

using namespace visual::gui;

Button::Button() {
	zSquare = -2.0f;
}

Button::~Button() {
	//delete square;
	//delete text;
}


bool Button::init(const std::string fontname) {
	Log().debug() << "button init " ;

	//square = new model::ColoredSquare;
	if (!square.loadModel()) {
		Log().error() << "Could not create background for button." ;
		return false;
	}
	//square->position(glm::vec3(0.5f, 0.5f, zSquare));
	//square->scale(glm::vec3(0.5f, 0.5f, 1.0f));

	//text = new Text;
	if (!text.init(fontname/*"data/fonts/MADAVE.TTF"*/)) {
		Log().error() << "Could not create text for button";
	}
	text.setSize(20);
	//text->setText("Klick mich");
	//text->setPosition(0.5f, 0.5f);

	this->position(glm::vec2(-0.5f, 0.5f));
	this->scale(glm::vec2(0.5f));
	this->setText("Klick mich");

	return true;
}

void Button::setText(const std::string text) {
	this->text.setText(text);
}
bool Button::setTextSize(const int points) {
	return text.setSize(points);
}
void Button::setTextColor(const glm::vec4 color) {
	text.setColor(color);
}

void Button::setHighlightColor(glm::vec4 color) {
	Log().debug() << "highlightcolor: " << color.r << "/" << color.g << "/" << color.b << "/" << color.a ;
	square.setHighlightColor(color);
}
void Button::isHighlighted(bool choice) {
	square.isHighlighted(choice);
}
void Button::scale(glm::vec2 scale) {
	square.scale(glm::vec3(scale, 1.0f));
}
void Button::position(glm::vec2 position) {
	square.position(glm::vec3(position, zSquare));
	text.setPosition(position.x, position.y);
}

void Button::draw() {
	square.draw();
	text.draw();
}