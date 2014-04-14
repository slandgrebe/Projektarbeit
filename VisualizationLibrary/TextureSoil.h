#ifndef TEXTURESOIL_H
#define	TEXTURESOIL_H

#include <GL/glew.h>
#include "Texture.h"
#include <string>

namespace visual {
	namespace model {

		/** Implementierung des Texture Interface.
		Diese Implementierung verwendet die SOIL (Simple OpenGL Image Library) Bibliothek.
		* @author Stefan Landgrebe
		* @see <a href="http://www.lonesock.net/soil.html">http://www.lonesock.net/soil.html</a>
		*/
		class TextureSoil : Texture {
		public:

			/** Konstruktor
			Zur eigentlichen Erzeugung der Textur muss zusätzlich die load oder die loadFromFile Methode verwendet werden.
			* @author Stefan Landgrebe
			*/
			TextureSoil();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~TextureSoil();


			/** Verwendet den übergebenen Bitmap-Buffer zur Erzeugung der Textur.
			* @author Stefan Landgrebe
			* @param width Breite des Bildes
			* @param height Höhe des Bildes
			* @param image Bitmap-Buffer des Bildes
			* @param internalFormat Internes Format welches bei der Erstellung der Textur verwendet werden soll
			* @param format Format welches bei der Erstellung der Textur verwendet werden soll.
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			* @see Texture::load()
			* @see <a href="https://www.opengl.org/sdk/docs/man3/xhtml/glTexImage2D.xml">relevante OpenGL Dokumentation</a>
			*/
			bool load(const int width, const int height, const unsigned char* image, GLint internalFormat = GL_RGB, GLenum format = GL_RGB);
			
			/** laedt die Textur in den Speicher
			* @author Stefan Landgrebe
			* @param filename Dateiname der zu ladenden Textur
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			* @see Texture::loadFromFile()
			*/
			bool loadFromFile(const std::string& filename);
			
			/** Bindet diese Textur als die aktuelle Textur
			* @author Stefan Landgrebe
			* @param textureUnit optionaler Parameter welcher die zu verwendende Textur Einheit definiert (Standard GL_TEXTURE0)
			* @see Texture::bind()
			*/
			void bind(GLenum textureUnit = GL_TEXTURE0);

		private:
			std::string filename;
			GLenum textureTarget;
			GLuint textureId;
		};
	}
}

#endif

