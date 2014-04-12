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
		* Das Model Interface repräsentiert ein 3D-Modell
		* @author Stefan Landgrebe
		*/
		class Model {
		protected:
			float m_boundingSphereRadius;
			float m_scalingNormalizationFactor;

			glm::vec3 m_positionVector; /** Vektor für die Verschiebung des Modells */
			GLfloat m_rotationAngle;
			glm::vec3 m_rotationAxis;
			glm::vec3 m_scalingVector; /** Vektor für die Skalierung des Modells */
			glm::mat4 m_modelMatrix; /** Matrix des Modells */

			glm::vec4 highlightColor;
			bool m_isHighlighted;

			bool m_isAttachedToCamera;

			graphics::ShaderProgram* shaderProgram;

			/** Liefert die transformierte Matrix des Modells zurück
			* @author Stefan Landgrebe
			* @return die transformierte Matrix
			*/
			virtual glm::mat4 getTransformedModelMatrix(void) {
				// 1. skalieren
				// 2. rotieren
				// 3. verschieben
				// ModelMatrix = Translation * Rotation * Scale * Position;
				glm::mat4 transformedModelMatrix = glm::translate(m_modelMatrix, m_positionVector);
				transformedModelMatrix = glm::rotate(transformedModelMatrix, m_rotationAngle, m_rotationAxis);
				transformedModelMatrix = glm::scale(transformedModelMatrix, m_scalingVector);

				return transformedModelMatrix;
			};

		public:
			Model() {
				m_boundingSphereRadius = 0.0f;
				m_scalingNormalizationFactor = 1.0f;

				// Matrizen
				m_modelMatrix = glm::mat4(1.0f);
				m_positionVector = glm::vec3(0.0f, 0.0f, 0.0f);
				m_rotationAngle = 0.0f;
				m_rotationAxis = glm::vec3(1.0f, 0.0f, 0.0f);
				m_scalingVector = glm::vec3(1.0f, 1.0f, 1.0f);

				highlightColor = glm::vec4(1.0f, 0.0f, 0.0f, 1.0f);
				m_isHighlighted = false;
				m_isAttachedToCamera = false;
			};
			virtual ~Model(void) {};

			virtual float boundingSphereRadius(void) {
				float x = m_scalingVector.x;
				float y = m_scalingVector.y;
				float z = m_scalingVector.z;

				if (x > y && x > z) {
					return m_boundingSphereRadius * x;
				}
				else if (y > x && y > z) {
					return m_boundingSphereRadius * y;
				}

				return m_boundingSphereRadius * z;
			}

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
					Log().warning() << "Nullvektor ist keine zulaessige Rotationsachse";
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
				m_scalingVector = scale * m_scalingNormalizationFactor;
			};

			virtual glm::vec3 scale(void) {
				return m_scalingVector;
			};

			virtual glm::mat4 getModelViewMatrix(void) {
				if (m_isAttachedToCamera) {
					return graphics::GraphicEngine::getInstance()->getProjectionMatrix() * getTransformedModelMatrix();
				}
				// Model View Projection Matrix => verkehrte Reihenfolge
				return graphics::GraphicEngine::getInstance()->getViewProjectionMatrix() * getTransformedModelMatrix();
			};

			virtual glm::mat4 getOrthographicModelViewMatrix(void) {
				// Model View Projection Matrix => verkehrte Reihenfolge
				return graphics::GraphicEngine::getInstance()->getViewOrthographicMatrix() * getTransformedModelMatrix();
			};

			virtual void setHighlightColor(glm::vec4 color) {
				highlightColor = color;
			}

			virtual void isHighlighted(bool choice) {
				m_isHighlighted = choice;
			}


			virtual void attachedToCamera(bool choice) {
				m_isAttachedToCamera = choice;
			}
			virtual bool attachedToCamera(void) {
				return m_isAttachedToCamera;
			}

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			virtual void draw(void) = 0;
		};
	}
}
#endif