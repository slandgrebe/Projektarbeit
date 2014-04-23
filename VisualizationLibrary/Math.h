#pragma once

#include <glm/glm.hpp>

namespace visual {
	namespace math {
		class Math
		{
		public:
			static bool isPointOnSegment(glm::vec3 point, glm::vec3 s1, glm::vec3 s2);
			static bool segmentIntersection(glm::vec3 a1, glm::vec3 a2, glm::vec3 b1, glm::vec3 b2);
			static bool segmentIntersectsTriangle(glm::vec3 start, glm::vec3 end, glm::vec3 vertexA, glm::vec3 vertexB, glm::vec3 vertexC);
			static bool doTrianglesIntersect(glm::vec3 a1, glm::vec3 b1, glm::vec3 c1, glm::vec3 a2, glm::vec3 b2, glm::vec3 c2);

		private:
			Math();
			~Math();

			static const float NEAR_ZERO; // anything that avoids division overflow
		};
	}
}

