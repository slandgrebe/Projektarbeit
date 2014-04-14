#ifndef TEXTURE_H
#define TEXTURE_H

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

// GLM - Mathe Library
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

#include <string>

namespace visual {
	/** Der Model Namespace beinhaltet das Model Interface sowie dessen Implementierungen
	* @author Stefan Landgrebe
	*/
	namespace model {

		/**
		* Das Texture Interface repräsentiert eine Textur.
		* @author Stefan Landgrebe
		*/
		class Texture {
		public:

			/** Verwendet den übergebenen Bitmap-Buffer zur Erzeugung der Textur.
			* @author Stefan Landgrebe
			* @param width Breite des Bildes
			* @param height Höhe des Bildes
			* @param image Bitmap-Buffer des Bildes
			* @param internalFormat Internes Format welches bei der Erstellung der Textur verwendet werden soll
			* @param format Format welches bei der Erstellung der Textur verwendet werden soll.
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			* @see <a href="https://www.opengl.org/sdk/docs/man3/xhtml/glTexImage2D.xml">relevante OpenGL Dokumentation</a>
			*/
			virtual bool load(const int width, const int height, const unsigned char* image, GLint internalFormat, GLenum format) = 0;

			/** laedt die Textur in den Speicher
			* @author Stefan Landgrebe
			* @param filename Dateiname der zu ladenden Textur
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			*/
			virtual bool loadFromFile(const std::string& filename) = 0;

			/** Bindet diese Textur als die aktuelle Textur
			* @author Stefan Landgrebe
			* @param textureUnit optionaler Parameter welcher die zu verwendende Textur Einheit definiert (Standard GL_TEXTURE0)
			*/
			virtual void bind(GLenum textureUnit = GL_TEXTURE0) = 0;
		};
	}
}
#endif