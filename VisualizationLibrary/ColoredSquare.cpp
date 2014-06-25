#include "stdafx.h"
#include "ColoredSquare.h"
#include "GraphicEngine.h"
#include <glm/gtc/type_ptr.hpp>

using namespace visual::model;

ColoredSquare::ColoredSquare() {}

ColoredSquare::~ColoredSquare() {
	// VBOs und VAO entfernen
	glDeleteBuffers(1, &vertexBufferId);
	//glDeleteBuffers(1, &colorBufferId);
	glDeleteVertexArrays(1, &vertexArrayId);
}


bool ColoredSquare::loadModel(void) {
	// Create Vertex Array Object
	glGenVertexArrays(1, &vertexArrayId);
	glBindVertexArray(vertexArrayId);

	Log().debug() << " [ColoredSquare] square vao: " << vertexArrayId ;

	// shader
	//shaderProgram = new graphics::ShaderProgram;
	shaderProgram.createShaderProgram("data/shader/colored.vertexshader", "data/shader/colored.fragmentshader");

	m_boundingSphereRadius = 0.5f;
	if (m_boundingSphereRadius > 0) {
		m_scalingNormalizationFactor = 1 / (2 * m_boundingSphereRadius);
		scale(m_scalingVector);
	}

	// positions
	glGenBuffers(1, &vertexBufferId);
	GLfloat vertices[] = {
		-0.5f,  0.5f, -1.0f, // Top-left
		0.5f, 0.5f, -1.0f,	 // Top-right
		0.5f, -0.5f, -1.0f,	  // Bottom-right

		0.5f, -0.5f, -1.0f,	 // Bottom-right
		-0.5f, -0.5f, -1.0f, // Bottom-left
		-0.5f, 0.5f, -1.0f,	 // Top-left
	};
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);

	// colors
	/*glGenBuffers(1, &colorBufferId);
	GLfloat colors[] = {
		1.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f,
		0.0f, 0.0f, 1.0f,

		0.0f, 1.0f, 1.0f,
		1.0f, 1.0f, 0.0f,
		1.0f, 0.0f, 1.0f,
	};
	glBindBuffer(GL_ARRAY_BUFFER, colorBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);*/

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



void ColoredSquare::draw(void) {
	// unsichtbare Objekte müssen nicht geladen werden
	if (!m_isVisible) {
		return;
	}

	glBindVertexArray(vertexArrayId);

	// shader
	shaderProgram.use();

	// positions
	GLint posAttrib = shaderProgram.getAttribute("position");
	glEnableVertexAttribArray(posAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glVertexAttribPointer(
		posAttrib,          // layout Attribut im Vertex Shader
		3,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,					// Stride
		(void*)0            // Offset
		);

	// colors
	/*GLint colAttrib = shaderProgram->getAttribute("color");
	glEnableVertexAttribArray(colAttrib);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glVertexAttribPointer(
		colAttrib,          // layout Attribut im Vertex Shader
		3,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		sizeof(GLfloat) * 6,// Stride
		(const GLvoid*)12   // Offset
		);*/

	// color
	GLint colorAttribute = shaderProgram.getUniform("color");
	GLfloat r = 0.0f, g = 0.5f, b = 0.5f, a = 1.0f; // standard: aquamarin
	if (m_isHighlighted) {
		r = highlightColor.r;
		g = highlightColor.g;
		b = highlightColor.b;
		a = highlightColor.a;
	}
	glUniform4f(colorAttribute, r, g, b, a);

	// transformations
	GLint uniMvp = shaderProgram.getUniform("mvp");
	//glm::mat4 mvp = getModelViewMatrix();
	glm::mat4 mvp = getOrthographicModelViewMatrix();


	glUniformMatrix4fv(uniMvp, 1, GL_FALSE, glm::value_ptr(mvp));

	// Effektives zeichnen des Modells
	glDrawArrays(GL_TRIANGLES, 0, 6); // Dreiecke zeichnen, ab Vertex 0, 3 vertices

	// Vertex Shader Attribute Array deaktivieren
	glDisableVertexAttribArray(posAttrib);
	//glDisableVertexAttribArray(colAttrib);

	glBindVertexArray(0);
}