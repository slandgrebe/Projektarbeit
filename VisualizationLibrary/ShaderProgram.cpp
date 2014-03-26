#include "stdafx.h"
#include "ShaderProgram.h"
#include <iostream>
#include <fstream>
#include <vector>

using namespace visual::graphics;

ShaderProgram::ShaderProgram() {
}


ShaderProgram::~ShaderProgram() {
	deleteShaderProgram(this->shaderProgramId);
}


GLuint ShaderProgram::createShader(const std::string filename, ShaderProgram::shaderType m_shaderType) {
	// Shader erstellen
	GLuint shaderId;

	if (m_shaderType == shaderType::VERTEX) {
		shaderId = glCreateShader(GL_VERTEX_SHADER);
	}
	else if (m_shaderType == shaderType::FRAGMENT) {
		shaderId = glCreateShader(GL_FRAGMENT_SHADER);
	}
	else {
		std::cout << "Unknown Shader Type." << std::endl;
		return 0;
	}
	// Vertex Shader Code aus der Datei auslesen
	std::string shaderCode;
	std::ifstream shaderStream(filename.c_str(), std::ios::in);
	if (shaderStream.is_open()){
		std::string line = "";
		while (getline(shaderStream, line)) {
			shaderCode += "\n" + line;
		}
		shaderStream.close();

		/*std::cout << "-- Shader Source -------------------------------------" << std::endl;
		std::cout << shaderCode << std::endl;
		std::cout << "------------------------------------------------------" << std::endl;*/
	}
	else {
		printf("Shader Datei kann nicht gelesen werden. Datei: %s.\n", filename.c_str());
		return 0;
	}

	// Variablen für die Prüfung der kompilierten Shader
	GLint result = GL_FALSE;
	int infoLogLength = 0;

	// Vertex Shader kompilieren
	const GLchar* sourcePointer = shaderCode.c_str();

	glShaderSource(shaderId, 1, &sourcePointer, NULL);
	glCompileShader(shaderId);

	// Vertex Shader prüfen
	glGetShaderiv(shaderId, GL_COMPILE_STATUS, &result);
	glGetShaderiv(shaderId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 1) { // Da ging etwas schief
		std::vector<char> vertexShaderErrorMessage(infoLogLength + 1);
		glGetShaderInfoLog(shaderId, infoLogLength, NULL, &vertexShaderErrorMessage[0]);
		printf("Der Vertex Shader konnte nicht kompiliert werden.\nFehlermeldung: [%s]\n", &vertexShaderErrorMessage[0]);
	}

	return shaderId;
}
GLuint ShaderProgram::createShaderProgram(GLuint vertexShaderId, GLuint fragmentShaderId) {
	// Shader Programm linken
	GLuint programId = glCreateProgram();
	glAttachShader(programId, vertexShaderId);
	glAttachShader(programId, fragmentShaderId);
	//glBindFragDataLocation(programId, 0, "outColor");
	glLinkProgram(programId);

	// Variablen für die Prüfung der kompilierten Shader
	GLint result = GL_FALSE;
	int infoLogLength = 0;

	// Shader Programm prüfen
	glGetProgramiv(programId, GL_LINK_STATUS, &result);
	glGetProgramiv(programId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 1) { // da ging etwas schief
		std::vector<char> programErrorMessage(infoLogLength + 1);
		glGetProgramInfoLog(programId, infoLogLength, NULL, &programErrorMessage[0]);
		printf("Das Shader Programm konnte nicht erstellt werden.\nFehlermeldung: [%s]\n", &programErrorMessage[0]);
	}

	// Vertex und Fragment Shader werden nicht mehr benötigt, da wir ja jetzt das fertige Shader Programm haben.
	//glDeleteShader(vertexShaderId);
	//glDeleteShader(fragmentShaderId);

	return programId;
}

GLuint ShaderProgram::createShaderProgram(const std::string vertexShaderFilename, const std::string fragmentShaderfilename) {
	GLuint vertexShaderId = createShader(vertexShaderFilename, shaderType::VERTEX);
	GLuint fragmentShaderId = createShader(fragmentShaderfilename, shaderType::FRAGMENT);
	shaderProgramId = createShaderProgram(vertexShaderId, fragmentShaderId);
	glUseProgram(shaderProgramId);

	return shaderProgramId;
}

GLuint ShaderProgram::getShaderProgramId(void) {
	return shaderProgramId;
}

void ShaderProgram::deleteShaderProgram(const GLuint shaderProgramId) {
	glDeleteProgram(shaderProgramId);
}