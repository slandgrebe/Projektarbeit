#include "stdafx.h"
#include "Text.h"

#include <iostream>

using namespace visual::gui;

// http://nehe.gamedev.net/tutorial/freetype_fonts_in_opengl/24001/
Text::Text() {
	m_text = "Text";
	x = 0;
	y = 0;
	currentTextSize = 12;
	color = glm::vec4(0.0f, 0.0f, 0.0f, 1.0f);
	fontFamily = "data/fonts/arial.ttf";
}

Text::~Text() {
	FT_Done_Face(face);
	FT_Done_FreeType(library);
}

void Text::setText(const std::string text) {
	std::cout << "Wechsle Text von '" << m_text << "' zu '" << text << "'." << std::endl;
	m_text = text;
}
void Text::setPosition(const unsigned int x, const unsigned int y) {
	this->x = x;
	this->y = y;
}
bool Text::setSize(const int pixelSize) {
	// set the font's pixel size
	if (FT_Set_Pixel_Sizes(face, 0, pixelSize)) {
		std::cout << "Schriftgrösse konnte nicht auf " << pixelSize << " gesetzt werden." << std::endl;
		return false;
	}

	std::cout << "Schriftgrösse auf " << pixelSize << " gesetzt." << std::endl;

	currentTextSize = pixelSize; // set current font size equal to 'value' parameter
	return true;
}
void Text::setColor(const glm::vec4 color) {
	this->color = color;
}
/*bool Text::setFontFamily(const std::string filename) {
	FT_Done_Face(face);

	if (FT_New_Face(library, filename.c_str(), 0, &face)) {
		std::cout << "Schrift '" << filename << "' konnte nicht geladen werden." << std::endl;
		return false;
	}

	setSize(this->currentTextSize);

	glyphSlot = face->glyph;

	std::cout << "Schrift auf '" << filename << "' gesetzt." << std::endl;

	this->fontFamily = filename;
	return true;
}*/

bool Text::init(std::string filename) {
	std::cout << "init" << std::endl;

	if (FT_Init_FreeType(&library)) {
		std::cout << "FreeType Bibliothek konnte nicht initialisiert werden." << std::endl;
		library = NULL;
		return false;
	}

	if (FT_New_Face(library, filename.c_str(), 0, &face)) {
		std::cout << "Schrift '" << filename << "' konnte nicht geladen werden." << std::endl;

		if (FT_New_Face(library, fontFamily.c_str(), 0, &face)) {
			std::cout << "Es kann keine Schrift gefunden werden!" << std::endl;
			return false;
		}
		else {
			std::cout << "Verwende Fallback Schrift: '" << fontFamily << "'." << std::endl;
		}
	}
	else {
		fontFamily = filename;
	}

	if (!setSize(currentTextSize)) {
		return false;
	}

	glyphSlot = face->glyph;


	shaderProgram = new visual::graphics::ShaderProgram;
	shaderProgram->createShaderProgram("data/shader/text.vertexshader", "data/shader/text.fragmentshader");

	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);

	glGenBuffers(1, &vbo);
	//glBindBuffer(GL_ARRAY_BUFFER, vbo);

	glBindVertexArray(0);

	return true;
}

void Text::write(std::string text, float x, float y, int align) {
	glBindVertexArray(vao);
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	//glActiveTexture(GL_TEXTURE0);
	//glBindTexture(GL_TEXTURE_2D, textTexture);

	fontCoordinates = glGetAttribLocation(shaderProgram->getShaderProgramId(), "fontCoords");
	glEnableVertexAttribArray(fontCoordinates);
	glVertexAttribPointer(fontCoordinates, 4, GL_FLOAT, GL_FALSE, 0, 0);

	

	glActiveTexture(GL_TEXTURE0);
	glGenTextures(1, &textTexture);
	glBindTexture(GL_TEXTURE_2D, textTexture);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glPixelStorei(GL_UNPACK_ALIGNMENT, 1);

	glUseProgram(shaderProgram->getShaderProgramId());

	int windowWidth = visual::graphics::GraphicEngine::getInstance()->getWindowWidth();
	int windowHeight = visual::graphics::GraphicEngine::getInstance()->getWindowHeight();
	float screenx = 2.0f / windowWidth, screeny = 2.0f / windowHeight;
	float totalWidth = 0.0f;

	int index = 0;
	for (char c = text[index++]; c != 0; c = text[index++]) {
		getGlyph(c);
		totalWidth += (currentGlyph.advance.x >> 6) * screenx;
	}

	index = 0;
	for (char c = text[index++]; c != 0; c = text[index++]) {
		getGlyph(c);
		
		//glTexImage2D(GL_TEXTURE_2D, 0, GL_ALPHA, currentGlyph.bitmapWidth, currentGlyph.bitmapRows, 0, GL_ALPHA, GL_UNSIGNED_BYTE, currentGlyph.bitmapBuffer);
		// GL_ALPHA ist deprecated http://stackoverflow.com/questions/15618343/gl-alpha-gl-luminance
		// ... und hier steht die Lösung: http://en.wikibooks.org/wiki/Talk:OpenGL_Programming/Modern_OpenGL_Tutorial_Text_Rendering_01
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, currentGlyph.bitmapWidth, currentGlyph.bitmapRows, 0, GL_RED, GL_UNSIGNED_BYTE, currentGlyph.bitmapBuffer);

		// Get a handle for our "myTextureSampler" uniform
		/*GLuint texture = glGetUniformLocation(shaderProgram->getShaderProgramId(), "tex");
		glUniform1i(texture, 0);*/		

		//std::cout << "bitmapLeft: " << currentGlyph.bitmapLeft << " advance.x: " << currentGlyph.advance.x << std::endl;
		

		float x2 = x + currentGlyph.bitmapLeft * screenx;
		float y2 = -y - currentGlyph.bitmapTop * screeny;
		float w = currentGlyph.bitmapWidth * screenx;
		float h = currentGlyph.bitmapRows * screeny;

		/*std::cout << x2 << " " << y2 << " " << w << " " << h << std::endl;
		system("Pause");*/

		if (align == ALIGN_CENTER) {
			x2 -= totalWidth / 2;
		}
		else if (align == ALIGN_RIGHT) {
			x2 -= totalWidth;
		}

		/*GLfloat box[4][4] = {
			{x2,	-y2,	0, 0},
			{x2+w,	-y2,	1, 0},
			{x2,	-y2-h,	0, 1},
			{x2+w,	-y2-h,	1, 1}
		};*/
		GLfloat box[4][4] = {
			{ x2, -y2 - h, 0, 1 },
			{ x2 + w, -y2 - h, 1, 1 },
			{ x2, -y2, 0, 0 },
			{ x2 + w, -y2, 1, 0 }
		};

		glBufferData(GL_ARRAY_BUFFER, sizeof box, box, GL_DYNAMIC_DRAW);
		glDrawArrays(GL_TRIANGLE_STRIP, 0, 4);

		x += (currentGlyph.advance.x >> 6) * screenx;
		y += (currentGlyph.advance.y >> 6) * screeny;
	}

	glDeleteTextures(1, &textTexture);
	glDisableVertexAttribArray(fontCoordinates);
	glBindVertexArray(0);
}

void Text::getGlyph(char c) {
	std::vector<GlyphData>::iterator glyphIterator = glyphs.begin();

	while (glyphIterator != glyphs.end()) {
		if (glyphIterator->c == c && glyphIterator->size == currentTextSize) {
			currentGlyph = *glyphIterator;
			return;
		}

		glyphIterator++;
	}

	if (FT_Load_Char(face, c, FT_LOAD_RENDER)) {
		std::cout << "FreeType kann das Zeichen " << c << " nicht laden." << std::endl;
	}

	GlyphData glyphData;
	//glyphData.bitmapBuffer = new unsigned char[glyphSlot->bitmap.rows * glyphSlot->bitmap.width * 4];
	glyphData.bitmapBuffer = new unsigned char[glyphSlot->bitmap.rows * glyphSlot->bitmap.width];

	//std::memcpy(glyphData.bitmapBuffer, glyphSlot->bitmap.buffer, glyphSlot->bitmap.rows * glyphSlot->bitmap.width * 4);
	std::memcpy(glyphData.bitmapBuffer, glyphSlot->bitmap.buffer, glyphSlot->bitmap.rows * glyphSlot->bitmap.width);

	glyphData.bitmapWidth = glyphSlot->bitmap.width;
	glyphData.bitmapRows = glyphSlot->bitmap.rows;
	glyphData.bitmapLeft = glyphSlot->bitmap_left;
	glyphData.bitmapTop = glyphSlot->bitmap_top;
	glyphData.advance = glyphSlot->advance;
	glyphData.c = c;
	glyphData.size = currentTextSize;
	currentGlyph = glyphData;

	glyphs.push_back(glyphData);

	/*std::cout << "*********************************" << std::endl;
	for (int j = 0; j < glyphData.bitmapRows; j++) {
		for (int i = 0; i < glyphData.bitmapWidth; i++) {
			std::cout << glyphData.bitmapBuffer[(i + j * glyphData.bitmapWidth)];
		}
		std::cout << std::endl;
	}
	std::cout << "*********************************" << std::endl;*/

}

void Text::draw(void) {
	

	glUseProgram(shaderProgram->getShaderProgramId());

	GLint colorAttribute = glGetUniformLocation(shaderProgram->getShaderProgramId(), "color");
	
	GLfloat r = color.r;
	GLfloat g = color.g;
	GLfloat b = color.b;
	GLfloat a = color.a;
	glUniform4f(colorAttribute, r, g, b, a);
	//std::cout << "color: " << color << std::endl;

	// Get a handle for our "myTextureSampler" uniform
	GLuint texture = glGetUniformLocation(shaderProgram->getShaderProgramId(), "tex");
	//std::cout << "texture: " << texture << std::endl;

	/*
	// Bind our texture in Texture Unit 0
	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, Texture);
	// Set our "myTextureSampler" sampler to user Texture Unit 0
	glUniform1i(TextureID, 0);
	*/
	int windowWidth = visual::graphics::GraphicEngine::getInstance()->getWindowWidth();
	int windowHeight = visual::graphics::GraphicEngine::getInstance()->getWindowHeight();
	float screenx = 2.0f / windowWidth, screeny = 2.0f / windowHeight;

	float relativeX = x * screenx * 2.0f - 1;
	float relativeY = y * screeny * 2.0f - 1;

	write(m_text, relativeX, relativeY, ALIGN_CENTER);

	//glUseProgram(0);
}

int Text::nextPowerOf2(int n) {
	int result = 1;

	// rval<<=1 Is A Prettier Way Of Writing rval*=2;
	while (result < n) {
		result <<= 1;
	}

	return result;
}