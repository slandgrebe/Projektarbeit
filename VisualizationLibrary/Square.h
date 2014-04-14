#ifndef SQUARE_H
#define SQUARE_H

#include "Model.h"
#include "TextureSoil.h"
#include "ShaderProgram.h"

namespace visual {
	namespace model {

		/** Die Square Klasse zeichnet ein Rechteck, auf welches eine Textur projeziert wird. Die Square Klasse erbt von der abstrakten Model Klasse.
		* @author Stefan Landgrebe
		*/
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

			/** Konstruktor.
			Zur eigentlichen Erstellung des Modells muss die Methode loadFromFile(), loadImage() oder load() verwendet werden.
			* @author Stefan Landgrebe
			*/
			Square();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~Square();
			

			/** Erzeugt das Modell und verwendet das Bild des übergebenen Dateipfades als Textur.
			* @author Stefan Landgrebe
			* @param filename Dateipfad des Bildes
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			*/
			bool loadFromFile(const std::string filename = "data/textures/sample.png");

			/** Erzeugt das Modell und verwendet den übergebenen Bitmap-Buffer als Textur.
			* @author Stefan Landgrebe
			* @param width Breite des Bildes
			* @param height Höhe des Bildes
			* @param image Bitmap-Buffer des Bildes
			* @param internalFormat Internes Format welches bei der Erstellung der Textur verwendet werden soll
			* @param format Format welches bei der Erstellung der Textur verwendet werden soll.
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			* @see Texture::load()
			*/
			bool loadImage(const int width, const int height, const unsigned char* image, GLint internalFormat = GL_RGB, GLenum format = GL_RGB);

			/** Erzeugt das Modell und verwendet eine vorgegebene Textur.
			* @author Stefan Landgrebe
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			*/
			bool load(void);


			/** Zeichnet das Modell neu
			* @author Stefan Landgrebe
			*/
			virtual void draw(void);
		};
	}
}


#endif