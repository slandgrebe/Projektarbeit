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

	highlightColor = glm::vec4(1.0f, 0.0f, 0.0f, 1.0f);
	m_isHighlighted = false;
}

Square::~Square() {
	delete shaderProgram;
	delete texture;

	// VBOs und VAO entfernen
	glDeleteBuffers(1, &positionBufferId);
	glDeleteBuffers(1, &colorBufferId);
	glDeleteBuffers(1, &textureBufferId);
	glDeleteVertexArrays(1, &vertexArrayId);
}

bool Square::loadModel(void) {
	Log().debug() <<  " [Square] glewExperimental: " << (glewExperimental == GL_TRUE) ;
	
	// Create Vertex Array Object
	glGenVertexArrays(1, &vertexArrayId);
	glBindVertexArray(vertexArrayId);

	Log().debug() << " [Square] square vao: " << vertexArrayId ;

	// shader
	shaderProgram = new graphics::ShaderProgram;
	shaderProgram->createShaderProgram("data/shader/SimpleVertexShader.vertexshader", "data/shader/SimpleFragmentShader.fragmentshader");

	// positions
	glGenBuffers(1, &positionBufferId);
	GLfloat positions[] = {
		-0.5f, 0.5f, 0.0f,  // Top-left
		0.5f, 0.5f, 0.0f,  // Top-right
		0.5f, -0.5f, 0.0f,  // Bottom-right

		0.5f, -0.5f, 0.0f,  // Bottom-right
		-0.5f, -0.5f, 0.0f,   // Bottom-left
		-0.5f, 0.5f, 0.0f,  // Top-left
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

	// Create an element array
	/*GLuint ebo;
	glGenBuffers(1, &ebo);
	GLuint elements[] = {
	0, 1, 2,
	2, 3, 0
	};
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(elements), elements, GL_STATIC_DRAW);*/
	
	return true;
}

bool Square::loadFromFile(const std::string filename) {
	if (!loadModel()) {
		return false;
	}
	// texture
	texture = new TextureSoil;
	if (!texture->loadFromFile(filename)) {
		return false;
	}

	// Make sure the VAO is not changed from outside code
	glBindVertexArray(0);

	return true;
}

bool Square::loadImage(const int width, const int height, const unsigned char* image, GLint internalFormat, GLenum format) {
	if (!loadModel()) {
		return false;
	}
	// texture
	texture = new TextureSoil;
	if (!texture->load(width, height, image, internalFormat, format)) {
		return false;
	}

	// Make sure the VAO is not changed from outside code
	glBindVertexArray(0);

	return true;
}

bool Square::load(void) {
	if (!loadModel()) {
		return false;
	}

	texture = 0;

	// Make sure the VAO is not changed from outside code
	glBindVertexArray(0);

	return true;
}

void Square::draw(void) {
	glBindVertexArray(vertexArrayId);

	// shader
	shaderProgram->use();

	// positions
	GLint posAttrib = shaderProgram->getAttribute("position");
	glEnableVertexAttribArray(posAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, positionBufferId);
	glVertexAttribPointer(
		posAttrib,          // layout Attribut im Vertex Shader
		3,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
		);

	// colors
	GLint colAttrib = shaderProgram->getAttribute("color");
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
	GLint texAttrib = shaderProgram->getAttribute("texcoord");
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
	if (texture) {
		texture->bind();
	}

	// highlight color
	GLint highlightAttribute = shaderProgram->getUniform("highlightColor");
	GLfloat r = 1.0f, g = 1.0f, b = 1.0f, a = 1.0f; // standard: weiss
	if (m_isHighlighted) {
		r = highlightColor.r;
		g = highlightColor.g;
		b = highlightColor.b;
		a = highlightColor.a;
	}
	glUniform4f(highlightAttribute, r, g, b, a);

	// transformations
	GLint uniMvp = shaderProgram->getUniform("mvp");
	glm::mat4 mvp = getModelViewMatrix();
	glUniformMatrix4fv(uniMvp, 1, GL_FALSE, glm::value_ptr(mvp)); 

	// Effektives zeichnen des Modells
	glDrawArrays(GL_TRIANGLES, 0, 6); // Dreiecke zeichnen, ab Vertex 0, 3 vertices

	// Vertex Shader Attribute Array deaktivieren
	glDisableVertexAttribArray(posAttrib);
	glDisableVertexAttribArray(colAttrib);
	glDisableVertexAttribArray(texAttrib);

	glBindVertexArray(0);
}