#include "stdafx.h"
#include "Camera.h"
#include <math.h>
#include <glm/gtc/matrix_transform.hpp>

#include <iostream>

using namespace visual::graphics;

Camera::Camera() {
	m_position = glm::vec3(0.0f, 0.0f, 0.0f);
	m_rotation = 180.0f;
	m_tilt = 0.0f;
}

Camera::~Camera() {
}

void Camera::position(glm::vec3 position) {
	m_position = position;
}
void Camera::rotate(float degrees) {
	m_rotation = degrees;
}
void Camera::tilt(float degrees) {
	m_tilt = degrees;
}
void Camera::changeSpeed(float speed) {
	m_speed = speed;
}

glm::mat4 Camera::getViewMatrix() {
	return viewMatrix;
}

void Camera::advance(float time) {
	// sph�rische koordinaten in kartesische koordinaten umwandeln
	glm::vec3 direction(	
		cos(degreesToRadian(m_tilt)) * sin(degreesToRadian(m_rotation)),
		sin(degreesToRadian(m_tilt)),
		cos(degreesToRadian(m_tilt)) * cos(degreesToRadian(m_rotation))
	);

	// rechts
	glm::vec3 right(
		sin(degreesToRadian(m_rotation - 90)),
		0,
		cos(degreesToRadian(m_rotation - 90))
	);

	// oben
	glm::vec3 up = glm::cross(right, direction);

	// Camera Matrix
	viewMatrix = glm::lookAt(
		m_position,
		m_position + direction,
		up
	);
}

float Camera::degreesToRadian(float degrees) {
	float pi = (float)(atan(1) * 4);
	return pi * degrees / 180;
}