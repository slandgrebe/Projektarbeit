#include "stdafx.h"
#include "Camera.h"
#include <math.h>
#include <glm/gtc/matrix_transform.hpp>

#include <iostream>

using namespace visual::graphics;

Camera::Camera() {
	position = glm::vec3(0.0f, 0.0f, 0.0f);
	rotation = 180.0f;
	tilt = 0.0f;
}

Camera::~Camera() {
}


glm::mat4 Camera::getViewMatrix() {
	return viewMatrix;
}

void Camera::advance(float time) {
	// sphärische koordinaten in kartesische koordinaten umwandeln
	glm::vec3 direction(	
		cos(degreesToRadian(tilt)) * sin(degreesToRadian(rotation)),
		sin(degreesToRadian(tilt)),
		cos(degreesToRadian(tilt)) * cos(degreesToRadian(rotation))
	);

	// rechts
	glm::vec3 right(
		sin(degreesToRadian(rotation - 90)),
		0,
		cos(degreesToRadian(rotation - 90))
	);

	// oben
	glm::vec3 up = glm::cross(right, direction);

	// Camera Matrix
	viewMatrix = glm::lookAt(
		position,
		position + direction,
		up
	);
}

float Camera::degreesToRadian(float degrees) {
	float pi = (float)(atan(1) * 4);
	return pi * degrees / 180;
}