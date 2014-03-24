#include "Label.h"
#include <iostream>

using namespace visual::gui;

Label::Label() {
	position = glm::vec3(0, 0, 0);
	width = 100;
	height = 100;
	text = "Label";

	loadFont("FreeSans.ttf", 48);
}

GLboolean Label::loadFont(std::string filename, GLuint fontSize) {
	if (FT_Init_FreeType(&ftLib)) {
		std::cout << "Could not init freetype library" << std::endl;
		return GL_FALSE;
	}
	if (FT_New_Face(ftLib, filename.c_str(), 0, &ftFace)) {
		std::cout << "Could not open font " << filename << std::endl;
		return GL_FALSE;
	}
	if (FT_Set_Pixel_Sizes(ftFace, 0, fontSize)) {
		std::cout << "Could not set font size to " << fontSize << std::endl;
		return GL_FALSE;
	}

	if (FT_Load_Char(ftFace, 'X', FT_LOAD_RENDER)) {
		std::cout << "Could not load character" << std::endl;
		return GL_FALSE;
	}

	

	return GL_TRUE;
}

void Label::draw() {
	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);



	glDisable(GL_BLEND);
}


void Label::createCharacter(GLuint index) {
	/*FT_Load_Glyph(ftFace, FT_Get_Char_Index(ftFace, index), FT_LOAD_DEFAULT);
	
	FT_Render_Glyph(ftFace->glyph, FT_RENDER_MODE_NORMAL);
	FT_Bitmap* bitmap = &ftFace->glyph->bitmap;

	int width = bitmap->width;
	int height = bitmap->rows;
	int textureWidth = nextPowerOf2(width);
	int textureHeight = nextPowerOf2(height);

	GLubyte* data = new GLubyte[textureWidth * textureHeight];

	for (int h = 0; h < textureHeight; h++) {
		for (int w = 0; w < textureWidth; w++) {
			data[h * textureWidth + w] = (h >= height || w >= width) ? 0 : bitmap->buffer[(height - h - 1) * width + w];
		}
	}



	GLfloat colors[] = {
		1.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f,
		0.0f, 0.0f, 1.0f,

		0.0f, 1.0f, 1.0f,
		1.0f, 1.0f, 0.0f,
		1.0f, 0.0f, 1.0f,
	};
	
	glGenBuffers(1, &vertexBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex)* vertices.size(), &vertices[0], GL_STATIC_DRAW);
	*/
}

void Label::setPosition(glm::vec3 position) {

}

void Label::setSize(GLuint width, GLuint height) {

}

void Label::setText(std::string text) {

}