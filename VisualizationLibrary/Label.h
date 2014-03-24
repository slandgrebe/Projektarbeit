#ifndef LABEL_H
#define LABEL_H

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// GLM - Mathe Library
#include <glm/glm.hpp>

#include "TextureSoil.h"

#include <ft2build.h>
#include FT_FREETYPE_H

#include <string>

namespace visual {
	/** Der Model Namespace beinhaltet das Model Interface sowie dessen Implementierungen
	* @author Stefan Landgrebe
	*/
	namespace gui {
		class Label {
		private:
			glm::vec3 position;
			GLuint width;
			GLuint height;
			std::string text;

			visual::model::TextureSoil characterTextures[256];
			GLuint advanceX[256];
			GLuint advanceY[256];
			GLuint bearingX[256];
			GLuint bearingY[256];
			GLuint characterWidth[256];
			GLuint characterHeight[256];
			GLuint loadedPixelSize;
			GLuint newline;
			
			GLboolean loaded;

			GLuint vertexArrayId;
			GLuint vertexBufferId;

			FT_Library ftLib;
			FT_Face ftFace;


			GLboolean loadFont(std::string filename, GLuint pixelSize);
			GLuint getTextWidth(std::string text, GLint pixelSize);
			void print(std::string text, GLuint x, GLuint y, GLint pixelSize = -1);
			void releaseFont();

			void createCharacter(GLuint index);
			inline GLuint nextPowerOf2(GLuint n) {
				GLuint res = 1;
				while (res < n) {
					res <<= 1;
				}

				return res;
			}



		public:
			Label();

			void setPosition(glm::vec3 position);
			void setSize(GLuint width, GLuint height);
			void setText(std::string text);

			//void setBackground(glm::vec3 color);
			//void setBackground(Texture texture)

			//setHighlight(color)
			//setHighlight(texture)

			//isHighlighted(boolean)

			void draw();
		};
	}
}

#endif