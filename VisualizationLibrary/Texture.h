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
		* Das Texture Interface repräsentiert eine Textur
		* @author Stefan Landgrebe
		*/
		class Texture {
		public:

			/** laedt die Textur in den Speicher
			* @author Stefan Landgrebe
			* @param filename Dateiname der zu ladenden Textur
			* @return bool
			*/
			virtual bool load(	const std::string& filename) = 0;

			/** Bindet diese Textur als die aktuelle Textur
			* @author Stefan Landgrebe
			* @param textureUnit optionaler Parameter welcher die zu verwendende Textur Einheit definiert (Standard GL_TEXTURE0)
			*/
			virtual void bind(GLenum textureUnit = GL_TEXTURE0) = 0;
		};
	}
}
#endif