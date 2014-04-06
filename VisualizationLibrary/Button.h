#pragma once

#include <GL/glew.h>
#include <glm/glm.hpp>
#include <string>

#include "ColoredSquare.h"
#include "Text.h"

namespace visual {
	namespace gui {
		class Button {
		private:
			model::ColoredSquare* square;
			Text* text;
			float zSquare;
		public:
			Button();
			~Button();

			bool init(const std::string fontname);
			void setText(const std::string text);
			
			void setHighlightColor(glm::vec4 color);
			void isHighlighted(bool choice);
			void scale(glm::vec2 scale);

			void position(glm::vec2);

			void draw();
		};
	}
}
