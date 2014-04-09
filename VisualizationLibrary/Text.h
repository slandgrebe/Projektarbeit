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

			visual::graphics::ShaderProgram* shaderProgram;

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

			int nextPowerOf2(int n);

		public:
			Text();
			~Text();

			bool init(std::string filename);
			void write(std::string text, float x, float y, int align);

			void setText(const std::string text);
			void setPosition(const float x, const float y);
			bool setSize(const int points);
			void setColor(const glm::vec4 color);
			//bool setFontFamily(const std::string filename = "data/fonts/arial.ttf");

			//void fontOpt(fontOptions opt, int value);

			void draw(void);
		};
	}
}
