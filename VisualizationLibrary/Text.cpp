#include "stdafx.h"
#include "Text.h"

#include <iostream>

using namespace visual::gui;

// http://nehe.gamedev.net/tutorial/freetype_fonts_in_opengl/24001/
Text::Text() {
	/*
		FreeType Library
	*/
	FT_Error error = FT_Init_FreeType(&ftLibrary);
	if (error) {
		std::cout << "FreeType Bibliothek konnte nicht initialisiert werden." << std::endl;
	}

	/*
		FreeType Face
	*/
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

	error = FT_Set_Char_Size(
		ftFace,    /* handle to face object           */
		0,       /* char_width in 1/64th of points  */
		24 * 64,   /* char_height in 1/64th of points */
		300,     /* horizontal device resolution    */
		300);   /* vertical device resolution      */

	error = FT_Set_Pixel_Sizes(
		ftFace,   /* handle to face object */
		0,      /* pixel_width           */
		16);   /* pixel_height          */

	error = FT_Select_Charmap(
		ftFace,               /* target face object */
		FT_ENCODING_UNICODE); /* encoding           */
	if (error) {
		std::cout << "Charactermap konnte nicht geladen werden." << std::endl;
	}

	/*
		FreeType Glyph
	*/

	//FT_ULong charcode = 0x00000021;
	char * text = "!";
	FT_UInt glyph_index = FT_Get_Char_Index(ftFace, 077);

	std::cout << "glyph index: " << glyph_index << std::endl;

	error = FT_Load_Glyph(
		ftFace,          /* handle to face object */
		glyph_index,   /* glyph index           */
		FT_LOAD_DEFAULT);  /* load flags, see below */

	error = FT_Render_Glyph(ftFace->glyph,   /* glyph slot  */
		FT_RENDER_MODE_NORMAL); /* render mode */

	FT_Bitmap& bitmap = ftFace->glyph->bitmap;

	int width = nextPowerOf2(bitmap.width);
	int height = nextPowerOf2(bitmap.rows);

	std::cout << "width: " << width << " bitmap.width: " << bitmap.width << std::endl;

	/*GLubyte* expandedData = new GLubyte[2 * width * height];

	std::cout << "*********************************" << std::endl;
	for (int j = 0; j < height; j++) {
		for (int i = 0; i < width; i++) {
			expandedData[2 * (i + j * width)] = expandedData[2 * (i + j * width) + 1] =
				(i >= bitmap.width || j >= bitmap.rows) ? 0 : bitmap.buffer[i + bitmap.width * j];

			//std::cout << "row: " << j << " column: " << i << " content[" << expandedData[2 * (i + j * width)] << "]" << std::endl;
			std::cout << expandedData[2 * (i + j * width)];
		}
		std::cout << std::endl;
	}
	std::cout << "*********************************" << std::endl;*/
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

	/*texture = new visual::model::TextureSoil;
	texture->load(width, height, expandedData, GL_RGBA, GL_LUMINANCE_ALPHA);*/
	/*// Now We Just Setup Some Texture Parameters..
	GLuint textureId;
	glGenTextures(1, &textureId);
	glActiveTexture(GL_TEXTURE0);
	glBindTexture(GL_TEXTURE_2D, textureId);
	
	// Here We Actually Create The Texture Itself, Notice
	// That We Are Using GL_LUMINANCE_ALPHA To Indicate That
	// We Are Using 2 Channel Data.
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, width, height, 0,
		GL_LUMINANCE_ALPHA, GL_UNSIGNED_BYTE, expandedData);
	
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);*/

	square = new visual::model::Square;
	square->loadImage(width, height, expandedData/*, GL_RGBA, GL_LUMINANCE_ALPHA*/);

	delete[] expandedData;

	// We Don't Need The Face Information Now That The Display
	// Lists Have Been Created, So We Free The Assosiated Resources.
	FT_Done_Face(ftFace);

	// Ditto For The Font Library.
	FT_Done_FreeType(ftLibrary);
}


Text::~Text() {
}

int Text::nextPowerOf2(int n) {
	int result = 1;
	
	// rval<<=1 Is A Prettier Way Of Writing rval*=2;
	while (result < n) {
		result <<= 1;
	}

	return result;
}

void Text::draw(void) {
	square->draw();
}