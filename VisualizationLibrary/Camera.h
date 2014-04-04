#pragma once

#include <glm\glm.hpp>

namespace visual {
	namespace graphics {
		class Camera {
		private:
			glm::vec3 position;
			float rotation;
			float tilt;

			glm::mat4 viewMatrix;

			float degreesToRadian(float degrees);
		public:
			Camera();
			~Camera();

			glm::mat4 getViewMatrix();
			void advance(float time);
		};
	}
}

