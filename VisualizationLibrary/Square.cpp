#include "stdafx.h"
#include "Square.h"
#include "GraphicEngine.h"
#include <glm/gtc/type_ptr.hpp>

using namespace visual::model;

Square::Square() {
	// Transformationen
	m_modelMatrix = glm::mat4(1.0f);
	m_positionVector = glm::vec3(0.0f, 0.0f, 0.0f);
	m_rotationAngle = 0.0f;
	m_rotationAxis = glm::vec3(0.0f, 0.0f, 1.0f);
	m_scalingVector = glm::vec3(1.0f, 1.0f, 1.0f);
}

Square::~Square() {
	// VBOs und VAO entfernen
	delete texture;
	glDeleteBuffers(1, &positionBufferId);
	glDeleteBuffers(1, &colorBufferId);
	glDeleteBuffers(1, &textureBufferId);
	glDeleteVertexArrays(1, &vertexArrayId);
}

GLboolean Square::loadModel(void) {
	std::cout <<  " [Square] glewExperimentalddd: " << (glewExperimental == GL_TRUE) << std::endl;
	
	// Create Vertex Array Object
	glGenVertexArrays(1, &vertexArrayId);
	glBindVertexArray(vertexArrayId);

	std::cout << " [Square] square vao: " << vertexArrayId << std::endl;

	// positions
	glGenBuffers(1, &positionBufferId);
	GLfloat positions[] = {
		-0.5f, 0.5f,  // Top-left
		0.5f, 0.5f,  // Top-right
		0.5f, -0.5f,  // Bottom-right

		0.5f, -0.5f,  // Bottom-right
		-0.5f, -0.5f,   // Bottom-left
		-0.5f, 0.5f,  // Top-left
	};
	glBindBuffer(GL_ARRAY_BUFFER, positionBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(positions), positions, GL_STATIC_DRAW);

	// colors
	glGenBuffers(1, &colorBufferId);
	GLfloat colors[] = {
		1.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f,
		0.0f, 0.0f, 1.0f,

		0.0f, 1.0f, 1.0f,
		1.0f, 1.0f, 0.0f,
		1.0f, 0.0f, 1.0f,
	};
	glBindBuffer(GL_ARRAY_BUFFER, colorBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);

	// texture
	glGenBuffers(1, &textureBufferId);
	GLfloat textureCoordinates[] = {
		0.0f, 0.0f,
		1.0f, 0.0f,
		1.0f, 1.0f,

		1.0f, 1.0f,
		0.0f, 1.0f,
		0.0f, 0.0f,
	};
	glBindBuffer(GL_ARRAY_BUFFER, textureBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(textureCoordinates), textureCoordinates, GL_STATIC_DRAW);

	// texture
	texture = new TextureSoil;
	if (!texture->load("data/textures/sample.png")) {
		return false;
	}
	
	// Create an element array
	/*GLuint ebo;
	glGenBuffers(1, &ebo);
	GLuint elements[] = {
	0, 1, 2,
	2, 3, 0
	};
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(elements), elements, GL_STATIC_DRAW);*/


	// Make sure the VAO is not changed from outside code
	glBindVertexArray(0);
	
	return true;
}

void Square::draw(void) {
	glBindVertexArray(vertexArrayId);
	
	// get Shader Program Reference
	GLuint shaderProgramId = graphics::GraphicEngine::getInstance()->getShaderProgramId();
	
	// positions
	GLint posAttrib = glGetAttribLocation(shaderProgramId, "position");
	glEnableVertexAttribArray(posAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, positionBufferId);
	glVertexAttribPointer(
		posAttrib,          // layout Attribut im Vertex Shader
		2,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
	);

	// colors
	GLint colAttrib = glGetAttribLocation(shaderProgramId, "color");
	glEnableVertexAttribArray(colAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, colorBufferId);
	glVertexAttribPointer(
		colAttrib,          // layout Attribut im Vertex Shader
		3,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
	);

	// texture
	GLint texAttrib = glGetAttribLocation(shaderProgramId, "texcoord");
	glEnableVertexAttribArray(texAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, textureBufferId);
	glVertexAttribPointer(
		texAttrib,          // layout Attribut im Vertex Shader
		2,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
	);

	// texture
	texture->bind();

	// transformations
	GLint uniMvp = glGetUniformLocation(shaderProgramId, "mvp");
	glm::mat4 mvp = getTransformedMatrix();
	glUniformMatrix4fv(uniMvp, 1, GL_FALSE, glm::value_ptr(mvp)); 

	// Effektives zeichnen des Modells
	glDrawArrays(GL_TRIANGLES, 0, 6); // Dreiecke zeichnen, ab Vertex 0, 3 vertices

	// Vertex Shader Attribute Array deaktivieren
	glDisableVertexAttribArray(posAttrib);
	glDisableVertexAttribArray(colAttrib);
	glDisableVertexAttribArray(texAttrib);

	glBindVertexArray(0);
}