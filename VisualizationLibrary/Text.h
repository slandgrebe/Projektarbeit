#pragma once

#include <GL/glew.h>
#include "Square.h"
#include "TextureSoil.h"

#include <ft2build.h>
#include FT_FREETYPE_H

namespace visual {
	namespace gui {
		class Text {
		private:
			FT_Library  ftLibrary;	/* handle to library     */
			FT_Face     ftFace;		/* handle to face object */

			visual::model::TextureSoil* texture;
			visual::model::Square* square;

			int nextPowerOf2(int n);

		public:
			Text();
			~Text();

			void draw(void);
		};
	}
}
