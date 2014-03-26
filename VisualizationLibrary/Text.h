#pragma once

#include <GL/glew.h>
#include "ShaderProgram.h"
#include <vector>

#include <ft2build.h>
#include FT_FREETYPE_H

namespace visual {
	namespace gui {
		class Text {
		private:
			FT_Library		library;	/* handle to library     */
			FT_Face			face;		/* handle to face object */
			FT_GlyphSlot	glyphSlot;

			int currentTextSize;

			void getGlyph(char c);

			GLuint vao;
			GLuint vbo;
			GLint fontCoordinates;
			GLuint textTexture;

			visual::graphics::ShaderProgram* shaderProgram;

			enum alignment {
				ALIGN_LEFT, ALIGN_CENTER, ALIGN_RIGHT
			};
			enum fontOptions {FONT_SIZE};

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

			bool init(int pixelSize);
			void write(std::string text, float x, float y, int align);

			void setText(std::string text, float x, float y);

			void fontOpt(fontOptions opt, int value);

			void draw(void);
		};
	}
}
