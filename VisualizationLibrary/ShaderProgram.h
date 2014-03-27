#pragma once
#include "stdafx.h"
#include <GL\glew.h>
#include <string>

namespace visual {
	namespace graphics {
		class ShaderProgram {
		private:
			enum shaderType { VERTEX, FRAGMENT, GEOMETRY };
			GLuint shaderProgramId;

			void deleteShaderProgram(const GLuint shaderProgramId);

		public:
			ShaderProgram();
			~ShaderProgram();

			GLuint createShader(const std::string filename, const shaderType);
			GLuint createShaderProgram(const GLuint vertexShaderId, const GLuint fragmentShaderId);
			GLuint createShaderProgram(const std::string vertexShaderFilename, const std::string fragmentShaderfilename);
			GLuint getShaderProgramId(void);
			
		};
	}
}