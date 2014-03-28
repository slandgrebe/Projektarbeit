#ifndef SQUARE_H
#define SQUARE_H

#include "Model.h"
#include "TextureSoil.h"
#include "ShaderProgram.h"

namespace visual {
	namespace model {
		class Square : public Model {
		private:
			GLuint vertexArrayId; /** Referenz auf das VAO (Vertex Array Object) */
			GLuint positionBufferId; /** Referenz auf den Buffer für die Positionen */
			GLuint colorBufferId; /** Referenz auf den Buffer für die Farben */
			GLuint textureBufferId; /** Referenz auf den Buffer für die Textur */
			GLuint textureId;

			graphics::ShaderProgram* shaderProgram;
			TextureSoil* texture;

			bool loadModel(void);

		public:
			Square();
			~Square();

			bool loadFromFile(const std::string filename = "data/textures/sample.png");
			bool loadImage(const int width, const int height, const unsigned char* image, GLint internalFormat = GL_RGB, GLenum format = GL_RGB);

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			virtual void draw(void);
		};
	}
}


#endif