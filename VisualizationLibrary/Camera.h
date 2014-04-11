#pragma once

#include <glm\glm.hpp>

namespace visual {
	namespace graphics {
		class Camera {
		private:
			glm::vec3 m_position;
			float m_rotation;
			float m_tilt;
			float m_speed;

			glm::mat4 viewMatrix;

			float degreesToRadian(float degrees);
		public:
			Camera();
			~Camera();

			void position(glm::vec3 position);
			void rotate(float degrees);
			void tilt(float degrees);
			void changeSpeed(float speed);

			glm::mat4 getViewMatrix();
			void advance(float time);
		};
	}
}

