#include "stdafx.h"
#include "Text.h"

#include <iostream>

using namespace visual::gui;

// http://nehe.gamedev.net/tutorial/freetype_fonts_in_opengl/24001/
Text::Text() {
	std::cout << "Consturctor" << std::endl;
	/*FT_Error error = FT_Init_FreeType(&ftLibrary);
	if (error) {
		std::cout << "FreeType Bibliothek konnte nicht initialisiert werden." << std::endl;
	}

	error = FT_New_Face(ftLibrary,	// handle to library
		"data/fonts/arial.ttf",		// filename
		0,							// face_index (0 geht immer)
		&ftFace);					// handel to face
	if (error == FT_Err_Unknown_File_Format) {
		std::cout << "Die Schrift wird nicht unterstützt." << std::endl;
	}
	else if (error) {
		std::cout << "Fehler beim Öffnen der Schrift." << std::endl;
	}

	error = FT_Set_Pixel_Sizes(
		ftFace,   // handle to face object
		0,      // pixel_width          
		12);   // pixel_height        

	error = FT_Select_Charmap(
		ftFace,               // target face object 
		FT_ENCODING_UNICODE); // encoding          
	if (error) {
		std::cout << "Charactermap konnte nicht geladen werden." << std::endl;
	}


	//FT_ULong charcode = 0x00000021;
	char * text = "!";
	FT_UInt glyph_index = FT_Get_Char_Index(ftFace, 077);

	std::cout << "glyph index: " << glyph_index << std::endl;

	error = FT_Load_Glyph(
		ftFace,          // handle to face object 
		glyph_index,   // glyph index           
		FT_LOAD_DEFAULT);  // load flags, see below 

	error = FT_Render_Glyph(ftFace->glyph,   // glyph slot  
		FT_RENDER_MODE_NORMAL); // render mode 

	FT_Bitmap& bitmap = ftFace->glyph->bitmap;

	int width = nextPowerOf2(bitmap.width);
	int height = nextPowerOf2(bitmap.rows);

	std::cout << "width: " << width << " bitmap.width: " << bitmap.width << std::endl;

	GLubyte* expandedData = new GLubyte[width * height];

	std::cout << "*********************************" << std::endl;
	for (int j = 0; j < height; j++) {
		for (int i = 0; i < width; i++) {
			expandedData[(i + j * width)] =
				(i >= bitmap.width || j >= bitmap.rows) ? 0 : bitmap.buffer[i + bitmap.width * j];

			std::cout << expandedData[(i + j * width)];
		}
		std::cout << std::endl;
	}
	std::cout << "*********************************" << std::endl; 

	square = new visual::model::Square;
	square->loadImage(width, height, expandedData);

	delete[] expandedData;

	// We Don't Need The Face Information Now That The Display
	// Lists Have Been Created, So We Free The Assosiated Resources.
	FT_Done_Face(ftFace);

	// Ditto For The Font Library.
	FT_Done_FreeType(ftLibrary);*/
}

Text::~Text() {
	FT_Done_Face(face);
	FT_Done_FreeType(library);
}

bool Text::init(int pixelSize) {
	std::cout << "init" << std::endl;

	if (FT_Init_FreeType(&library)) {
		std::cout << "FreeType Bibliothek konnte nicht initialisiert werden." << std::endl;
		return false;
	}

	if (FT_New_Face(library, "data/fonts/arial.ttf", 0, &face)) {
		std::cout << "Schrift konnte nicht geladen werden." << std::endl;
		return false;
	}

	FT_Set_Pixel_Sizes(face, 0, pixelSize);

	glyphSlot = face->glyph;

	shaderProgram = new visual::graphics::ShaderProgram;
	shaderProgram->createShaderProgram("data/shader/text.vertexshader", "data/shader/text.fragmentshader");

	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);

	glGenBuffers(1, &vbo);
	glBindBuffer(GL_ARRAY_BUFFER, vbo);

	fontCoordinates = glGetAttribLocation(shaderProgram->getShaderProgramId(), "fontCoords");
	glEnableVertexAttribArray(fontCoordinates);
	glVertexAttribPointer(fontCoordinates, 4, GL_FLOAT, GL_FALSE, 0, 0);

	return true;
}

void Text::write(std::string text, float x, float y, int align) {
	glBindVertexArray(vao);
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textTexture);

	glUseProgram(shaderProgram->getShaderProgramId());

	float screenx = 2.0f / 640, screeny = 2.0f / 480;
	float totalWidth = 0.0f;

	int index = 0;
	for (char c = text[index++]; c != 0; c = text[index++]) {
		getGlyph(c);
		totalWidth += (currentGlyph.advance.x >> 6) * screenx;
	}

	index = 0;
	for (char c = text[index++]; c != 0; c = text[index++]) {
		getGlyph(c);
		
		glActiveTexture(GL_TEXTURE0);
		glGenTextures(1, &textTexture);
		glBindTexture(GL_TEXTURE_2D, textTexture);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
		glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
		glPixelStorei(GL_UNPACK_ALIGNMENT, 1);
		
		GLenum error = glGetError();

		//glTexImage2D(GL_TEXTURE_2D, 0, GL_ALPHA, currentGlyph.bitmapWidth, currentGlyph.bitmapRows, 0, GL_ALPHA, GL_UNSIGNED_BYTE, currentGlyph.bitmapBuffer);
		// GL_ALPHA ist deprecated http://stackoverflow.com/questions/15618343/gl-alpha-gl-luminance
		glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, currentGlyph.bitmapWidth, currentGlyph.bitmapRows, 0, GL_RGBA, GL_UNSIGNED_BYTE, currentGlyph.bitmapBuffer);

		error = glGetError();
		if (error != GL_NO_ERROR) {
			std::cout << "---------------------------------------------------------" << std::endl;
			std::cout << "OpenGL Error: " << error << ": " << gluErrorString(error) << std::endl;
			std::cout << "---------------------------------------------------------" << std::endl;
		}

		// Get a handle for our "myTextureSampler" uniform
		/*GLuint texture = glGetUniformLocation(shaderProgram->getShaderProgramId(), "tex");
		glUniform1i(texture, 0);*/

		float x2 = x + currentGlyph.bitmapLeft * screenx;
		float y2 = -y - currentGlyph.bitmapTop * screeny;
		float w = currentGlyph.bitmapWidth * screenx;
		float h = currentGlyph.bitmapRows * screeny;

		if (align == ALIGN_CENTER) {
			x2 -= totalWidth / 2;
		}
		else if (align == ALIGN_RIGHT) {
			x2 -= totalWidth;
		}

		GLfloat box[4][4] = {
			{x2,	-y2,	0, 0},
			{x2+w,	-y2,	1, 0},
			{x2,	-y2-h,	0, 1},
			{x2+w,	-y2-h,	1, 1}
		};

		glBufferData(GL_ARRAY_BUFFER, sizeof box, box, GL_DYNAMIC_DRAW);
		glDrawArrays(GL_TRIANGLE_STRIP, 0, 4);

		x += (currentGlyph.advance.x >> 6) * screenx;
		y += (currentGlyph.advance.y >> 6) * screeny;

		glDeleteTextures(1, &textTexture);
	}

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

	std::cout << "*********************************" << std::endl;
	for (int j = 0; j < glyphData.bitmapRows; j++) {
		for (int i = 0; i < glyphData.bitmapWidth; i++) {
			std::cout << glyphData.bitmapBuffer[(i + j * glyphData.bitmapWidth)];
		}
		std::cout << std::endl;
	}
	std::cout << "*********************************" << std::endl;

}

void Text::draw(void) {
	

	glUseProgram(shaderProgram->getShaderProgramId());

	GLint color = glGetUniformLocation(shaderProgram->getShaderProgramId(), "color");
	glUniform4f(color, 0.0f, 1.0f, 0.0f, 1.0f);
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
	fontOpt(visual::gui::Text::fontOptions::FONT_SIZE, 12);
	write("hallo", 0.0f, 0.0f, ALIGN_CENTER);
}

void Text::fontOpt(Text::fontOptions opt, int value) {
	if (FONT_SIZE) {
		// set the font's pixel size
		FT_Set_Pixel_Sizes(face, 0, value);
		currentTextSize = value; // set current font size equal to 'value' parameter
	}
}

int Text::nextPowerOf2(int n) {
	int result = 1;

	// rval<<=1 Is A Prettier Way Of Writing rval*=2;
	while (result < n) {
		result <<= 1;
	}

	return result;
}