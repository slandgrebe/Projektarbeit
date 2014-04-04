#ifndef MODEL_H
#define MODEL_H

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

// GLM - Mathe Library
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

#include "GraphicEngine.h"
#include "ShaderProgram.h"

#include <string>
#include <vector>
#include <iostream>

namespace visual {
	/** Der Model Namespace beinhaltet das Model Interface sowie dessen Implementierungen
	* @author Stefan Landgrebe
	*/
	namespace model {

		/**
		* Das Model Interface repr�sentiert ein 3D-Modell
		* @author Stefan Landgrebe
		*/
		class Model {
		protected:
			glm::vec3 m_positionVector; /** Vektor f�r die Verschiebung des Modells */
			GLfloat m_rotationAngle;
			glm::vec3 m_rotationAxis;
			glm::vec3 m_scalingVector; /** Vektor f�r die Skalierung des Modells */
			glm::mat4 m_modelMatrix; /** Matrix des Modells */

			glm::vec4 highlightColor;
			bool m_isHighlighted;

			graphics::ShaderProgram* shaderProgram;

		public:

			virtual ~Model(void) {};

			/** Verschieben des Modells an die angegebene Position.
			* @author Stefan Landgrebe
			* @param x
			* @param y
			* @param z
			*/
			virtual void position(glm::vec3 position) {
				m_positionVector = position;
			};

			virtual glm::vec3 position(void) {
				return m_positionVector;
			};

			/** Rotation des Modells.
			* @author Stefan Landgrebe
			* @param degrees: Rotation in Grad
			* @param axis: Rotationsachse
			*/
			virtual void rotate(GLfloat degrees, glm::vec3 axis) {
				if (axis.x == 0 && axis.y == 0 && axis.z == 0) {
					std::cout << "Nullvektor ist keine zulaessige Rotationsachse" << std::endl;
					return;
				}

				m_rotationAngle = degrees;
				m_rotationAxis = axis;
			};
			
			virtual GLfloat rotationAngle(void) {
				return m_rotationAngle;
			};

			virtual glm::vec3 rotationAxis(void) {
				return m_rotationAxis;
			};

			/** Skalierung des Modells.
			* @author Stefan Landgrebe
			* @param x: Skalierung in x Richtung
			* @param y: Skalierung in y Richtung
			* @param z: Skalierung in z Richtung
			*/
			virtual void scale(glm::vec3 scale) {
				m_scalingVector = scale;
			};

			virtual glm::vec3 scale(void) {
				return m_scalingVector;
			};

			/** Liefert die transformierte Matrix des Modells zur�ck
			* @author Stefan Landgrebe
			* @return die transformierte Matrix
			*/
			virtual glm::mat4 getTransformedMatrix(void) {
				// 1. skalieren
				// 2. rotieren
				// 3. verschieben
				// ModelMatrix = Translation * Rotation * Scale * Position;
				glm::mat4 transformedMatrix = glm::translate(m_modelMatrix, m_positionVector);
				transformedMatrix = glm::rotate(transformedMatrix, m_rotationAngle, m_rotationAxis);
				transformedMatrix = glm::scale(transformedMatrix, m_scalingVector);

				// Model View Projection Matrix => verkehrte Reihenfolge
				transformedMatrix = graphics::GraphicEngine::getInstance()->getViewProjectionMatrix() * transformedMatrix;

				return transformedMatrix;
			};

			virtual void setHighlightColor(glm::vec4 color) {
				highlightColor = color;
			}

			virtual void isHighlighted(bool choice) {
				m_isHighlighted = choice;
			}

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			virtual void draw(void) = 0;
		};
	}
}
#endif