#include "stdafx.h"
#include "TextureSoil.h"
#include <SOIL.h>
#include <iostream>

using namespace visual::model;

TextureSoil::TextureSoil() {
}


TextureSoil::~TextureSoil() {
	glDeleteTextures(1, &textureId);
}

bool TextureSoil::load(const int width, const int height, const unsigned char* image, GLint internalFormat, GLenum format) {
	// Load texture
	glGenTextures(1, &textureId);
	this->bind();

	glGetError();
	glTexImage2D(GL_TEXTURE_2D, 0, internalFormat, width, height, 0, format, GL_UNSIGNED_BYTE, image);
	GLenum error = glGetError();
	if (error != GL_TRUE && error != 0 && error != 1280) { // http://www.opengl.org/wiki/OpenGL_Loading_Library - GL_INVALID_ENUM ignorieren
		std::cout << "OpenGL Error: Loading Texture: " << error << ": " << gluErrorString(error) << std::endl;
		
		return false;
	}

	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	//glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

	return true;
}

bool TextureSoil::loadFromFile(const std::string& filename) {
	bool result = false;

	int width, height;
	unsigned char* image = 0;

	//"E:/dev/opengl/ProjektarbeitTest/TextureTest/Debug/test.png"
	image = SOIL_load_image(filename.c_str(), &width, &height, NULL, SOIL_LOAD_RGB);
	if (image == 0) {
		std::cout << "Soil Error Message: " << SOIL_last_result() << std::endl;

		return false;
	}

	result = load(width, height, image);

	SOIL_free_image_data(image);

	return result;
}

void TextureSoil::bind(GLenum textureUnit) {
	//std::cout << "bind texture. textureId[" << textureId << "]" << std::endl;

	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureId);

	//std::cout << " Error" << glGetError() << std::endl;
}