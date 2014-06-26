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

	/** Der Model Namespace beinhaltet die abstrakte Model Klasse sowie dessen Implementierungen.
	Zusätzlich sind hier auch das Texture Interface und dessen Implementierungen beheimatet, welche von den Model Implementierungen verwendet werden.
	* @author Stefan Landgrebe
	*/
	namespace model {

		/**
		* Das Model Interface repräsentiert ein abstraktes 3D-Modell
		* @author Stefan Landgrebe
		*/
		class Model {
		protected:
			unsigned int m_collisionGroup;
			float m_boundingSphereRadius; /** Radius der umgebenden Kugel, welche das gesamte Modell umspannt und u.a. für die Kollisionserkennung verwendet wird */
			float m_scalingNormalizationFactor; /** Noramlisierungsvektor damit die Bounding Sphere einen Durchmesser von 1m (bzw. 1 Einheit) hat */
			bool m_scalingIsNormalized; /** Definiert ob die Skalierungsnormalisierung verwendet wird */

			glm::vec3 m_positionVector; /** Vektor für die Verschiebung des Modells */
			GLfloat m_rotationAngle; /** Rotationswinkel */
			glm::vec3 m_rotationAxis; /** Rotationsachse */
			glm::vec3 m_scalingVector; /** Vektor für die Skalierung des Modells */
			glm::mat4 m_modelMatrix; /** Matrix des Modells */

			glm::vec4 highlightColor; /** Hervorhebungsfarbe */
			bool m_isHighlighted; /** Hervorhebungszustand */

			bool m_isAttachedToCamera; /** Definiert ob das Modell an die Kamera angehängt wurde */
			bool m_modelChanged; /** Definiert ob sich das Model verändert hat */

			bool m_isVisible; /** Definiert ob das Model im Moment sichtbar ist */

			graphics::ShaderProgram shaderProgram; /** Shader Programm */
			 
			/** Liefert die transformierte Matrix (Skalierung, Rotation, Positionierung) des Modells zurück
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

			/** Konstruktor
			initialisiert alle Attribute
			* @author Stefan Landgrebe
			*/
			Model() {
				m_boundingSphereRadius = 0.0f;
				m_scalingNormalizationFactor = 1.0f;
				m_scalingIsNormalized = false;

				// Matrizen
				m_modelMatrix = glm::mat4(1.0f);
				m_positionVector = glm::vec3(0.0f, 0.0f, 0.0f);
				m_rotationAngle = 0.0f;
				m_rotationAxis = glm::vec3(1.0f, 0.0f, 0.0f);
				m_scalingVector = glm::vec3(1.0f, 1.0f, 1.0f);

				highlightColor = glm::vec4(1.0f, 0.0f, 0.0f, 1.0f);
				m_isHighlighted = false;
				m_isAttachedToCamera = false;
				m_modelChanged = false;

				m_isVisible = false;
			};

			/** virtueller Destruktor
			* @author Stefan Landgrebe
			*/
			virtual ~Model(void) {};

			/** Radius der Bounding Sphere (kleinstmögliche Kugel welche ihren Ursprung im Ursprung des Modells hat und das ganze Modell umfasst)
			* @author Stefan Landgrebe
			* @return Radius
			*/
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

			/** Definiert den Zustand der Skalierungsnormalisierung 
			* @author Stefan Landgrebe
			* @param choice Zustand der Skalierungsnormalisierung
			*/
			virtual void scalingIsNormalized(bool choice) {
				m_scalingIsNormalized = choice;
				scale(m_scalingVector); // Skalierung neu berechnen
			}

			/** Verschieben des Modells an die angegebene Position.
			* @author Stefan Landgrebe
			* @param position Positionsvektor
			*/
			virtual void position(glm::vec3 position) {
				m_positionVector = position;

				m_modelChanged = true;
			};

			/** Liefert die aktuelle Position des Modells zurück
			* @author Stefan Landgrebe
			* @return Positionsvektor
			*/
			virtual glm::vec3 position(void) {
				if (m_isAttachedToCamera) {
					return m_positionVector + graphics::GraphicEngine::getInstance()->camera()->position();
				}
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

				//Log().info() << "Rotation: " << degrees << " Grad um " << axis.x << "/" << axis.y << "/" << axis.z;

				m_rotationAngle = degrees;
				m_rotationAxis = axis;

				m_modelChanged = true;
			};
			
			/** Liefert den Rotationswinkel in Grad zurück
			* @author Stefan Landgrebe
			* @return Rotationswinkel in Grad
			*/
			virtual GLfloat rotationAngle(void) {
				return m_rotationAngle;
			};

			/** Liefert die Rotationsachse zurück
			* @author Stefan Landgrebe
			* @return Rotationsachse
			*/
			virtual glm::vec3 rotationAxis(void) {
				return m_rotationAxis;
			};

			/** Skalierung des Modells.
			* @author Stefan Landgrebe
			* @param scale Skalierungsvektor
			*/
			virtual void scale(glm::vec3 scale) {
				if (m_scalingIsNormalized) {
					m_scalingVector = scale * m_scalingNormalizationFactor;
				}
				else {
					m_scalingVector = scale;
				}

				m_modelChanged = true;
			};

			/** Liefert die aktuelle Skalierung zurück.
			* @author Stefan Landgrebe
			* @return Skalierungsvektor
			*/
			virtual glm::vec3 scale(void) {
				return m_scalingVector;
			};


			/** Berechnet und liefert die perspektivische Model-View Matrix zurück. (Wird für perspektivisch dargestellte 3D Modelle verwendet)
			* @author Stefan Landgrebe
			* @return perspektivische Model-View Matrix
			*/
			virtual glm::mat4 getModelViewMatrix(void) {
				if (m_isAttachedToCamera) {
					return graphics::GraphicEngine::getInstance()->getProjectionMatrix() * getTransformedModelMatrix();
				}
				// Model View Projection Matrix => verkehrte Reihenfolge
				return graphics::GraphicEngine::getInstance()->getViewProjectionMatrix() * getTransformedModelMatrix();
			};

			/** Berechnet und liefert die orthographische Model-View Matrix zurück. (Wird für GUI Elemente verwendet)
			* @author Stefan Landgrebe
			* @return orthographische Model-View Matrix
			*/
			virtual glm::mat4 getOrthographicModelViewMatrix(void) {
				// Model View Projection Matrix => verkehrte Reihenfolge
				return graphics::GraphicEngine::getInstance()->getViewOrthographicMatrix() * getTransformedModelMatrix();
			};


			/** Setzt die Hervorhebungsfarbe.
			Die Farbe wird durch 4 Komponenten definiert: Rot, Grün, Blau, Alpha
			Alle Komponenten Sollten einen Wert von 0 bis 1 haben, wobei der Wert 0 0% und der Wert 1 100% entspricht.
			* @author Stefan Landgrebe
			* @param color Farbe (r,g,b,a)
			*/
			virtual void setHighlightColor(glm::vec4 color) {
				highlightColor = color;
			}

			/** Statusänderung der Hervorhebung
			* @author Stefan Landgrebe
			* @param choice definiert den Zustand der Hervorhebung. True aktiviert und False deaktiviert die Hervorhebung.
			*/
			virtual void isHighlighted(bool choice) {
				m_isHighlighted = choice;
			}


			/** Hängt das Modell an die Kamera an, so dass sich dieses mit der Kamera mitbewegt und Positionierung relativ zur Kamera stattfindet.
			* @author Stefan Landgrebe
			* @param choice True hängt das Modell an und False macht dies wieder rückgängig
			*/
			virtual void attachedToCamera(bool choice) {
				m_isAttachedToCamera = choice;
			}

			/** Liefert den Zustand der Anhängung an die Kamrea zurück
			* @author Stefan Landgrebe
			* @return True wenn das Modell an die Kamera angehängt ist, ansonsten False
			*/
			virtual bool attachedToCamera(void) {
				return m_isAttachedToCamera;
			}

			/** Setzt die Kollisionsgruppe von diesem Modell
			* Modelle welche in derselben Kollisionsgruppe sind, werden nicht miteinander verglichen. Kollisionsgruppe 0 wird komplett ignoriert.
			* @author Stefan Landgrebe
			*/
			virtual void collisionGroup(unsigned int collisionGroup) {
				m_collisionGroup = collisionGroup;
			}

			/** Liefert die Kollisionsgroupde von diesem Modell
			* Modelle welche in derselben Kollisionsgruppe sind, werden nicht miteinander verglichen. Kollisionsgruppe 0 wird komplett ignoriert.
			* @author Stefan Landgrebe
			*/
			virtual unsigned int collisionGroup(void) {
				return m_collisionGroup;
			}

			/** Liefert die Sichtbarkeit des Objekts zurück
			* @author Stefan Landgrebe
			* @return Sichtbarkeit
			*/
			virtual bool visible(void) {
				return m_isVisible;
			}
			/** Setzt die Sichtbarkeit des Objekts
			* @author Stefan Landgrebe
			* @param choice Sichtbarkeit
			*/
			virtual void visible(bool choice) {
				m_isVisible = choice;
			}

			/** Zeichnet das Modell neu
			* @author Stefan Landgrebe
			*/
			virtual void draw(void) = 0;
		};
	}
}
#endif