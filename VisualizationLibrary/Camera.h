#pragma once

#include <glm\glm.hpp>

namespace visual {
	namespace graphics {

		/** Kamera Klasse wird zur Erzeugung der Viewmatrix verwendet.
		* @author Stefan Landgrebe
		*/
		class Camera {
		private:
			glm::vec3 m_position;
			float m_rotation;
			float m_tilt;
			float m_speed;

			glm::mat4 viewMatrix;

			float degreesToRadian(float degrees);
		public:
			/** Konstruktor
			* @author Stefan Landgrebe
			*/
			Camera();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~Camera();


			/** Positioniert die Kamera.
			Für die Bewegung der Kamera, sollte auf die Methoden rotate, tilt und changeSpeed zurückgegriffen werden.
			* @author Stefan Landgrebe
			* @param position Positionsvektor
			*/
			void position(glm::vec3 position);

			/** Liefert die Position der Kamera
			* @author Stefan Landgrebe
			* @return Positionsvektor
			*/
			glm::vec3 position(void);

			/** Rotiert die Kamera um die Y-Achse.
			* @author Stefan Landgrebe
			* @param degrees Rotationswinkel in Grad
			*/
			void rotate(float degrees);

			/** Rotiert die Kamrea um die X-Achse.
			* @author Stefan Landgrebe
			* @param degrees Rotationswinkel in Grad
			*/
			void tilt(float degrees);

			/** Ändert die Bewegungsgeschwindigkeit der Kamera
			* @author Stefan Landgrebe
			* @param speed Geschwindigkeit in m/s
			*/
			void changeSpeed(float speed);


			/** Liefert die Viewmatrix zurück
			* @author Stefan Landgrebe
			* @return Viewmatrix
			*/
			glm::mat4 getViewMatrix();

			/** Bewegt die Kamera anhang der Rotation, Neigung, Geschwindigkeit und der vergangenen Zeit seit dem letzten Aufruf.
			* @author Stefan Landgrebe
			* @param time vergangene Zeit seit dem letzten Aufruf in Sekunden.
			*/
			void advance(float time);
		};
	}
}

