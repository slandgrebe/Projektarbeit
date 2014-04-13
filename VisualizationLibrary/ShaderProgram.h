#pragma once
#include "stdafx.h"
#include <GL\glew.h>
#include <string>

namespace visual {
	namespace graphics {

		/** Abstraktion der OpenGL Shader Befehle.
		* @author Stefan Landgrebe
		*/
		class ShaderProgram {
		private:
			enum shaderType { VERTEX, FRAGMENT, GEOMETRY };
			GLuint shaderProgramId;

			void deleteShaderProgram(const GLuint shaderProgramId);

		public:

			/** Konstruktor
			* @author Stefan Landgrebe
			*/
			ShaderProgram();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~ShaderProgram();


			/** erzeugt einen Shader.
			* @author Stefan Landgrebe
			* @param filename Dateipfad des Shadersourceode
			* @param type Art des Shaders (Vertex, Fragment, Geometry)
			* @return ID des Shaders
			*/
			GLuint createShader(const std::string filename, const shaderType type);

			/** Linkt und kompiliert ein Shader Programm anhand von zuvor erstellten Shadern
			* @author Stefan Landgrebe
			* @param vertexShaderId ID des Vertex Shader
			* @param fragmentShaderId ID des Fragment Shader
			* @return ID des Shader Programms
			*/
			GLuint createShaderProgram(const GLuint vertexShaderId, const GLuint fragmentShaderId);

			/** Erzeugt Shader anhand der übergebenen Dateien und linkt und kompiliert daraus ein Shader Programm
			* @author Stefan Landgrebe
			* @param vertexShaderFilename Dateipfad des Vertexshader Sourccode
			* @param fragmentShaderfilename Dateipfad des Fragmentshader Sourccode
			* @return ID des Shader Programms
			*/
			GLuint createShaderProgram(const std::string vertexShaderFilename, const std::string fragmentShaderfilename);

			/** Liefert die ID des Shader Programms
			* @author Stefan Landgrebe
			* @return ID des Shader Programms
			*/
			GLuint getShaderProgramId(void);

			/** Setzt diesen Shader als den aktuell zu verwendenden
			* @author Stefan Landgrebe
			*/
			inline void use(void) { glUseProgram(shaderProgramId); }

			/** Liefert Referenz auf das Attribut innerhalb dieses Shaders
			* @author Stefan Landgrebe
			* @param name Name des Attributs
			* @return Referenz auf das Attribut
			*/
			inline GLuint getAttribute(std::string name) { return glGetAttribLocation(shaderProgramId, name.c_str()); }

			/** Liefert Referenz auf das Uniform innerhalb dieses Shaders
			* @author Stefan Landgrebe
			* @param name Name des Uniforms
			* @return Referenz auf das Uniform
			*/
			inline GLuint getUniform(std::string name) { return glGetUniformLocation(shaderProgramId, name.c_str()); }
		};
	}
}