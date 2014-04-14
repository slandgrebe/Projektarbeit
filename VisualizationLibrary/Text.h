#pragma once

#include <GL/glew.h>
#include <glm/glm.hpp>
#include "GraphicEngine.h"
#include "ShaderProgram.h"
#include <vector>

#include <ft2build.h>
#include FT_FREETYPE_H

namespace visual {
	namespace gui {

		/** Text Elemente.
		Diese Klasse verwendet zum Auslesen der Schriftdateien die sehr weit verbreitete Bibliothek FreeType.
		* @author Stefan Landgrebe
		* @see <a href="http://www.freetype.org/">http://www.freetype.org/</a> 
		*/
		class Text {
		private:
			std::string m_text;
			float x;
			float y;
			int currentTextSize;
			glm::vec4 color;
			std::string fontFamily;

			FT_Library		library;	/* handle to library     */
			FT_Face			face;		/* handle to face object */
			FT_GlyphSlot	glyphSlot;

			void getGlyph(char c);

			GLuint vao;
			GLuint vbo;
			GLint fontCoordinates;
			GLuint textTexture;

			visual::graphics::ShaderProgram shaderProgram;

			enum alignment {
				ALIGN_LEFT, ALIGN_CENTER, ALIGN_RIGHT
			};
			enum fontOptions {FONT_SIZE, FONT_COLOR, FONT_FAMILY};

			struct GlyphData {
				char c;
				int size;
				int bitmapWidth;
				int bitmapRows;
				unsigned char* bitmapBuffer;
				FT_Vector advance;
				int bitmapLeft;
				int bitmapTop;
			};

			std::vector<GlyphData> glyphs;
			GlyphData currentGlyph;

			void write(std::string text, float x, float y, int align);
			int nextPowerOf2(int n);

		public:

			/** Konstruktor
			Zum Laden des eigentlichen Textes muss zusätzlich die Methode init() aufgerufen werden.
			* @author Stefan Landgrebe
			* @see init()
			*/
			Text();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~Text();

			/** Erzeugt das effektive Text Objekt
			* @author Stefan Landgrebe
			* @param filename Dateipfad der Schriftart
			* @return Prüfung ob die Operation durchgeführt werden konnte
			*/
			bool init(std::string filename);			

			/** Ändert den darzustellenden Text
			* @author Stefan Landgrebe
			* @param text Der darzustellende Text
			*/
			void setText(const std::string text);

			/** Positioniert den Mittelpunkt des Textes an die übergebene Koordinate auf der Zeichnungsfläche des GUIs.
			Die Zeichnungsfläche umfasst das gesamte Fenster.
			Die Zeichnungsfläche ist 2 Einheiten breit und hoch, wobei die untere, linke Ecke der Koordinate -1/-1 und die obere, rechte Ecke der Koordinate 1/1 entspricht.
			* @author Stefan Landgrebe
			* @param x X-Koordinate
			* @param y Y-Koordinate
			*/
			void setPosition(const float x, const float y);

			/** Ändert die Grösse des Textes
			* @author Stefan Landgrebe
			* @param points Grösse in Punkten
			* @return Prüfung ob die Operation durchgeführt werden konnte
			*/
			bool setSize(const int points);

			/** Ändert die Farbe des Textes
			* @author Stefan Landgrebe
			* @param color Farbe (r,g,b,a)
			*/
			void setColor(const glm::vec4 color);


			/** Zeichnet den Text neu
			* @author Stefan Landgrebe
			*/
			void draw(void);
		};
	}
}
