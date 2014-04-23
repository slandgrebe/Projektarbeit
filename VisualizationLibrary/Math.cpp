#include "stdafx.h"
#include "math.h"

using namespace visual::math;

const float Math::NEAR_ZERO = 0.00000001;

Math::Math() {
}


Math::~Math() {
}


bool Math::isPointOnSegment(glm::vec3 point, glm::vec3 s1, glm::vec3 s2) {
	glm::vec3 normal = glm::cross(s2 - s1, point - s1);
	if (glm::length(normal) < NEAR_ZERO) { // alle 3 punkte sind auf einer geraden
		float c = glm::dot(s2 - s1, point - s1);
		if (c >= 0 && c <= glm::length(s2 - s1) * glm::length(s2 - s1)) {
			return true;
		}
	}
	return false;
}

bool Math::segmentIntersection(glm::vec3 a1, glm::vec3 a2, glm::vec3 b1, glm::vec3 b2) {
	glm::vec3 directionA = a2 - a1;
	glm::vec3 directionB = b2 - b1;
	glm::vec3 directionC = b2 - a1;

	glm::vec3 normalAB = glm::cross(directionA, directionB);

	if (glm::dot(directionC, normalAB) != 0.0) { // lines are not coplanar
		return false;
	}

	glm::vec3 normalCB = glm::cross(directionC, directionB);

	float s = glm::dot(normalCB, normalAB) / (glm::length(normalAB) * glm::length(normalAB));

	if (s >= 0.0 && s <= 1.0) {
		glm::vec3 intersection = a1 + directionA * s;
		return isPointOnSegment(intersection, b1, b2);
	}

	return false;
}



// Copyright 2001 softSurfer, 2012 Dan Sunday
// This code may be freely used and modified for any purpose
// providing that this copyright notice is included with it.
// SoftSurfer makes no warranty for this code, and cannot be held
// liable for any real or imagined damage resulting from its use.
// Users of this code must verify correctness for their application.

bool Math::segmentIntersectsTriangle(glm::vec3 start, glm::vec3 end,
		glm::vec3 vertexA, glm::vec3 vertexB, glm::vec3 vertexC) {

	// Dreieckseiten und -normale
	glm::vec3 edgeAB = vertexB - vertexA;	// Seite des Dreiecks von Punkt A bis Punkt B
	glm::vec3 edgeAC = vertexC - vertexA;	// Seite des Dreiecks von Punkt A bis Punkt C
	glm::vec3 normal = glm::cross(edgeAB, edgeAC); // Normale des Dreiecks

	if (normal == glm::vec3(0)) {      // triangle is degenerate
		Log().warning() << "degenerate triangle" << std::endl;
		return false;                  // do not deal with this case
	}

	glm::vec3 direction = end - start; // Richtung der Strecke
	glm::vec3 w0 = start - vertexA; // ray vector: Vektor von Punkt A des Dreiecks bis Anfang der Strecke
	float a = -glm::dot(normal, w0);		// -Cosinus des Winkels zwischen Normale und w0
	float b = glm::dot(normal, direction);	// Cosinus des Winkels zwischen Dreieck-Normale und Strecken-Richtung
	if (b < NEAR_ZERO && b > -NEAR_ZERO) {  // Strecke ist parallel zur Dreieck Ebene
		if (a == 0) { // Strecke liegt in der Dreieck Ebene
			Log().trace() << "same plane";
			if (segmentIntersection(start, end, vertexA, vertexB)) { // schneidet strecke die Seite AB
				Log().trace() << "AB";
				return true;
			}
			else if (segmentIntersection(start, end, vertexA, vertexC)) { // schneidet strecke die Seite AC
				Log().trace() << "AC";
				return true;
			}
			else if (segmentIntersection(start, end, vertexB, vertexC)) { // schneidet strecke die Seite BC
				Log().trace() << "BC";
				return true;
			}
			return false; // keine berührungspunkte
		}
		else {
			Log().trace() << "parallel, but not touching";
			return false; // Strecke berührt die Dreieck Ebene nicht
		}
	}

	// Schnittpunkt der Strecke mit der Dreieckebene
	float r = a / b;					// params to calc ray-plane intersect
	if (r < 0.0 						// ray goes away from triangle
		&& r > 1.0) {                   // for a segment, also test if (r > 1.0) => no intersect

		Log().trace() << "segment not intersecting, but line would";
		return false;                   // => no intersect
	}

	glm::vec3 intersection = start + r * direction;            // intersect point of ray and plane

	// is I inside T?
	float    uu, uv, vv, wu, wv, D;
	uu = glm::dot(edgeAB, edgeAB);
	uv = glm::dot(edgeAB, edgeAC);
	vv = glm::dot(edgeAC, edgeAC);
	glm::vec3 w = intersection - vertexA; // ray vector
	wu = glm::dot(w, edgeAB);
	wv = glm::dot(w, edgeAC);
	D = uv * uv - uu * vv;

	// get and test parametric coords
	float s, t;
	s = (uv * wv - vv * wu) / D;
	if (s < 0.0 || s > 1.0) {        // I is outside T
		Log().trace() << "outside triangle 1";
		return false;
	}
	t = (uv * wu - uu * wv) / D;
	if (t < 0.0 || (s + t) > 1.0) { // I is outside T
		Log().trace() << "outside triangle 2";
		return false;
	}

	return true;                       // I is in T
}


/* ****************************************************** */
bool Math::doTrianglesIntersect(glm::vec3 a1, glm::vec3 b1, glm::vec3 c1,
	glm::vec3 a2, glm::vec3 b2, glm::vec3 c2) {

	// schneidet Seite a1b1 Dreieck2?
	if (segmentIntersectsTriangle(a1, b1 - a1, a2, b2, c2)) {
		return true;
	}
	// schneidet Seite a1c1 Dreieck2?
	else if (segmentIntersectsTriangle(a1, c1 - a1, a2, b2, c2)) {
		return true;
	}
	// schneidet Seite b1c1 Dreieck2?
	else if (segmentIntersectsTriangle(b1, c1 - b1, a2, b2, c2)) {
		return true;
	}
	// Keine Seite von Dreieck 1 schneidet Dreieck2


	// schneidet Seite a2b2 Dreieck1?
	if (segmentIntersectsTriangle(a2, b2 - a2, a1, b1, c1)) {
		return true;
	}
	// schneidet Seite a2c2 Dreieck1?
	else if (segmentIntersectsTriangle(a2, c2 - a2, a1, b1, c1)) {
		return true;
	}
	// schneidet Seite b2c2 Dreieck1?
	else if (segmentIntersectsTriangle(b2, c2 - b2, a1, b1, c1)) {
		return true;
	}

	return false;
}