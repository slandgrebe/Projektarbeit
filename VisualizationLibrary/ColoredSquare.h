#pragma once

#include "Model.h"
#include "ShaderProgram.h"

namespace visual {
	namespace model {
		class ColoredSquare : public Model {
		private:
			GLuint vertexArrayId; /** Referenz auf das VAO (Vertex Array Object) */
			GLuint vertexBufferId; /** Referenz auf den Buffer f�r die Positionen */
			//GLuint colorBufferId; /** Referenz auf den Buffer f�r die Farben */

			graphics::ShaderProgram* shaderProgram;

		public:
			ColoredSquare();
			~ColoredSquare();

			bool loadModel(void);

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			virtual void draw(void);
		};
	}
}