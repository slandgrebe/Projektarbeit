#pragma once

#include "Model.h"
#include "ShaderProgram.h"

namespace visual {
	namespace model {

		/** ColoredSquare erbt von der abstrakten Klasse Model.
		Die Klasse wird für den Hintergrund der Buttons verwendet.
		* @author Stefan Landgrebe
		*/
		class ColoredSquare : public Model {
		private:
			GLuint vertexArrayId; /** Referenz auf das VAO (Vertex Array Object) */
			GLuint vertexBufferId; /** Referenz auf den Buffer für die Positionen */
			//GLuint colorBufferId; /** Referenz auf den Buffer für die Farben */

			graphics::ShaderProgram* shaderProgram;

		public:

			/** Konstruktor
			Zur Erstellung des eigentlichen Modells muss zusätzlich die loadModel() Methode aufgerufen werden
			* @author Stefan Landgrebe
			* @see loadModel()
			*/
			ColoredSquare();
			~ColoredSquare();

			/** Implementierung der Model::loadModel() Methode
			Generiert die Vertices und übergibt diese an OpenGL
			* @author Stefan Landgrebe
			* @see Model::loadModel()
			*/
			bool loadModel(void);

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			virtual void draw(void);
		};
	}
}