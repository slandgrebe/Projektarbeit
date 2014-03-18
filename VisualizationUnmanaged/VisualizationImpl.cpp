#include "VisualizationImpl.h"

// Include standard headers
#include <iostream>
#include <stdlib.h>

#include <string>
#include <vector>
#include <iostream>
#include <fstream>
#include <algorithm>

#include <math.h>

using namespace visual;

// instanz des Singleton
VisualizationImpl* VisualizationImpl::singleInstance = NULL;

/** Mit dieser Methode kann auf die einzige Instanz dieser Klasse zugegriffen werden.
* @author Stefan Landgrebe
* @return singleInstance: Die einzige Instanz der Klasse
*/
VisualizationImpl* VisualizationImpl::getInstance(void) {
	if (singleInstance == NULL) {
		singleInstance = new VisualizationImpl;
	}

	return singleInstance;
}

/**
* Der Konstruktor sorgt dafür, dass sämtliche Attribute, die GLEW Library und die GLFW Library initialisiert werden und öffnet ein Fenster.
* @author Stefan Landgrebe
*/
VisualizationImpl::VisualizationImpl(void) {
	status = initalize();
}
/**
* Diese Methode initialisiert sämtliche Attribute, die GLEW Library und die GLFW Library.
* @author Stefan Landgrebe
* @return Statuscode der Initialisierung
*/
int VisualizationImpl::initalize(void) {
	// Initialisiere GLFW
	if (!glfwInit()) {
		fprintf(stderr, "GLFW Library konnte nicht initialisiert werden.\n");
		return 1;
	}

	glfwWindowHint(GLFW_SAMPLES, 4); // Multisampling
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3); // OpenGL Version 3.3
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE); // Core Profile (keine veralteten Funktionen)

	// OpenGL Kontext erstellen und Fenster öffnen
	window = glfwCreateWindow(1024, 768, "Proof of Concept", 0, 0);
	if (!window) {
		fprintf(stderr, "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.\n");
		glfwTerminate();
		return 2;
	}
	glfwMakeContextCurrent(window);

	// Initialisiere GLEW - muss nach GLFW Initialisierung getan werden!
	glewExperimental = GL_TRUE; // wird beim Core Profile benötigt
	GLenum err = glewInit();
	if (GLEW_OK != err) {
		// glewInit ist fehlgeschlagen
		fprintf(stderr, "GLEW Initialisierungsfehler: %s\n", glewGetErrorString(err));
		return 3;
	}

	// Hintergrundfarbe setzen
	glClearColor(0.0f, 0.2f, 0.4f, 0.0f);

	// Tiefentest aktivieren
	glEnable(GL_DEPTH_TEST);
	// Fragmente zeichnen, welche näher an der Kamera sind
	glDepthFunc(GL_LESS);

	// Projection matrix : 45° Field of View, 4:3 Seitenverhältnis, Sichtweite von 0.1 bis 100 Einheiten
	projectionMatrix = glm::perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
	// Camera matrix
	viewMatrix = glm::lookAt(
		glm::vec3(0, 0, 10), // Kameraposition in World Space
		glm::vec3(0, 0, 0), // Fokus der Kamera
		glm::vec3(0, 1, 0)  // "oben" der Kamera
		);

	// Counter für Punkte und Linien
	pointCounter = 0;
	lineCounter = 0;

	// Shader Programm erstellen und kompilieren
	programId = loadShaders("SimpleVertexShader.vertexshader", "SimpleFragmentShader.fragmentshader");
	if (programId == 0) {
		return 4;
	}

	// Referenz auf die Model-View-Projection-Matrix im Shader Programm besorgen
	matrixId = glGetUniformLocation(programId, "MVP");

	// Referenz auf die Textur im Shader Programm besorgen
	textureId = glGetUniformLocation(programId, "myTextureSampler");

	// alles ok
	return 0;
}

/** Fuegt einen Punkt hinzu.
* @author Stefan Landgrebe
* @return liefert die pointId zurück, welche für die Methode updatePoint benötigt wird
*/
int VisualizationImpl::addPoint(void) {
	pointCounter++;
	//TexturedTriangle* point = new TexturedTriangle;
	//point->scale(0.1f);
	//pointList.insert(std::make_pair(pointCounter, point));

	return pointCounter;
}

/** Verschiebt die Position des Punktes.
* Verschiebt den Punkt auf die übergebene Position.
* @author Stefan Landgrebe
* @param pointId Die ID des Punktes
* @param x X Koordinate
* @param y Y Koordinate
* @param z Z Koordinate
*/
void VisualizationImpl::updatePoint(int pointId, float x, float y, float z) {
	//std::cout << "update x[" << x << "] y[" << y << "] z[" << z << "]" << std::endl;

	/*if (pointList.find(pointId) != pointList.end()) {
		//TexturedTriangle* point = pointList.find(pointId)->second;
		//point->translate(x, y, z);
	}*/
}

/** Fuegt eine Linie hinzu.
* @author Stefan Landgrebe
* @return liefert die lineId zurück, welche für die Methode updateLine benötigt wird
*/
int VisualizationImpl::addLine(void) {
	lineCounter++;
	//Line* line = new Line;

	//lineList.insert(std::make_pair(lineCounter, line));

	return lineCounter;
}

/** Verschiebt den Anfang- und Endpunkt der Linie.
* @author Stefan Landgrebe
* @param lineId: Die ID der Linie.
* @param x1: X Koordinate des Anfangpunktes
* @param y1: Y Koordinate des Anfangpunktes
* @param z1: Z Koordinate des Anfangpunktes
* @param x2: X Koordinate des Endpunktes
* @param y2: Y Koordinate des Endpunktes
* @param z2: Z Koordinate des Endpunktes
*/
void VisualizationImpl::updateLine(int lineId, float x1, float y1, float z1, float x2, float y2, float z2) {
	//std::cout << "\nupdate p1(" << x1 << "/" << y1 << "/" << z1 << ") p2(" << x2 << "/" << y2 << "/" << z2 << ")" << std::endl;

	/*
	1. Vektor v aus den beiden Punkten erzeugen
	2. v normalisieren
	3. Den normalisierten Vektor s definieren, welcher die Standardrichtung des Modells vorgibt.
	4. Kreuzprodukt von v und s definiert die Rotationsachse a
	5. Der Arkuskosinus des Skalarproduktes von v und s definiert den Winkel w der Rotation
	*/

	glm::vec3 v = glm::vec3(x2 - x1, y2 - y1, z2 - z1); // richtungsvektor v
	float length = sqrt(v.x*v.x + v.y*v.y + v.z*v.z); // laenge
	glm::vec3 vn = glm::vec3(v.x / length, v.y / length, v.z / length); // vn = normalisieren von v

	glm::vec3 s = glm::vec3(1, 0, 0); // standardrichtung des Modells (links nach rechts)

	glm::vec3 a = glm::cross(s, vn); // rotationsachse = standardrichtung *  richtungsvektor (nicht umgekehrt! Rechte Hand Regel)
	float cosW = glm::dot(vn, s); // cosinus Rotationswinkel = richtungsvektor * standardrichtung
	float w = acos(cosW); // rotationswinkel (radian)

	float pi = atan(1.0f) * 4; // pi!
	w = w * 180 / pi; // radian in grad umwandeln

	//printf("\n\n");
	//printf("vn(%f/%f/%f) s(%f/%f/%f)\n", vn.x, vn.y, vn.z, s.x, s.y, s.z);
	//printf("a(%f/%f/%f) cosW[%f] w[%f]\n", a.x, a.y, a.z, cosW, w);

	// Transformationen durchführen
	/*if (lineList.find(lineId) != lineList.end()) {
		Line* line = lineList.find(lineId)->second;
		line->rotate(w, a);
		line->scale(length, 0.05f, 0.05f);
		line->translate((x2 + x1) / 2, (y2 + y1) / 2, (z2 + z1) / 2);
	}*/
}

/** Fuegt ein Mesh hinzu.
* @author Stefan Landgrebe
*/
int VisualizationImpl::addMesh(void) {
	meshCounter++;
	//Mesh* mesh = new Mesh;

	//meshList.insert(std::make_pair(meshCounter, mesh));

	return meshCounter;
}

/** Verschiebt die Position des Meshes.
* Verschiebt das Mesh an die übergebene Position.
* @author Stefan Landgrebe
* @param meshId Die ID des Meshes
* @param x X Koordinate
* @param y Y Koordinate
* @param z Z Koordinate
*/
void VisualizationImpl::updateMesh(int meshId, float x, float y, float z) {
	//std::cout << "update x[" << x << "] y[" << y << "] z[" << z << "]" << std::endl;

	/*if (meshList.find(meshId) != meshList.end()) {
		Mesh* mesh = meshList.find(meshId)->second;
		mesh->translate(x, y, z);
	}*/
}

/** Zeichnet das Fenster neu.
* Zeichnet das Fenster neu. MVP Matrix wird neu erstellt und dem Shader übergeben und anschliessend die Draw Methode des Modells aufgerufen.
* @author Stefan Landgrebe
*/
void VisualizationImpl::draw(void) {
	// Offscreen Buffer leeren
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	// Shader Programm aktivieren
	glUseProgram(programId);

	// Punkte zeichnen
	/*std::map<int, TexturedTriangle*>::iterator it;
	for (it = pointList.begin(); it != pointList.end(); it++) {
		TexturedTriangle* point = (*it).second;

		// Model-View-Projection-Matrix neu berechnen
		glm::mat4 mvp = projectionMatrix * viewMatrix * point->getTransformedMatrix(); // Matrix Multiplikationen immer in verkehrter Reihenfolge!

		// MVP Matrix an Shader Programm übergeben
		glUniformMatrix4fv(matrixId, 1, GL_FALSE, &mvp[0][0]);

		point->bindTexture();

		// Set our "myTextureSampler" sampler to user Texture Unit 0
		glUniform1i(textureId, 0);

		point->draw();
	}*/

	// Linien zeichnen
	/*std::map<int, Line*>::iterator it2;
	for (it2 = lineList.begin(); it2 != lineList.end(); it2++) {
		Line* line = (*it2).second;

		// Model-View-Projection-Matrix neu berechnen
		glm::mat4 mvp = projectionMatrix * viewMatrix * line->getTransformedMatrix(); // Matrix Multiplikationen immer in verkehrter Reihenfolge!

		// MVP Matrix an Shader Programm übergeben
		glUniformMatrix4fv(matrixId, 1, GL_FALSE, &mvp[0][0]);


		line->draw();
	}*/

	// Meshes zeichnen
	/*std::map<int, Mesh*>::iterator it3;
	for (it3 = meshList.begin(); it3 != meshList.end(); it3++) {
		Mesh* mesh = (*it3).second;

		// Model-View-Projection-Matrix neu berechnen
		glm::mat4 mvp = projectionMatrix * viewMatrix * mesh->getTransformedMatrix(); // Matrix Multiplikationen immer in verkehrter Reihenfolge!

		// MVP Matrix an Shader Programm übergeben
		glUniformMatrix4fv(matrixId, 1, GL_FALSE, &mvp[0][0]);

		// Bind our texture in Texture Unit 0
		glActiveTexture(GL_TEXTURE0);
		//glBindTexture(GL_TEXTURE_2D, Texture);
		// Set our "myTextureSampler" sampler to user Texture Unit 0
		glUniform1i(textureId, 0);

		mesh->draw();
	}*/

	// Buffer austauschen
	glfwSwapBuffers(window);
	//glfwPollEvents();
}

/** Schliesst das Fenster.
* Entfernt sämtliche erzeugte Objekte, schliesst das Fenster und entfernt schliesslich das eigene Objekt.
* @author Stefan Landgrebe
*/
void VisualizationImpl::close() {
	// Delete Shader
	glDeleteProgram(programId);

	//delete model;
	/*std::map<int, TexturedTriangle*>::iterator it;
	for (it = pointList.begin(); it != pointList.end(); it++) {
		TexturedTriangle* point = (*it).second;
		delete point;
	}*/

	// GLWF beenden und somit das Fenster schliessen
	glfwTerminate();

	delete this;
}

/** Erstellt Shader Programm.
* Lädt den Vertex und Fragment Shader vom Filesystem, kompiliert und überprüft diese jeweils und linkt und überprüft diese in einem Shader Programm
* @author Stefan Landgrebe
* @param vertexFilePath Pfad zur Vertex Shader Datei
* @param fragmentFilePath Pfad zur Fragment Shader Datei
* @return Referenz auf das Shader Programm oder 0 falls etwas schief ging
*/
GLuint VisualizationImpl::loadShaders(const char* vertexFilePath, const char* fragmentFilePath) {

	// Shader erstellen
	GLuint vertexShaderId = glCreateShader(GL_VERTEX_SHADER);
	GLuint fragmentShaderId = glCreateShader(GL_FRAGMENT_SHADER);

	// Vertex Shader Code aus der Datei auslesen
	std::string vertexShaderCode;
	std::ifstream vertexShaderStream(vertexFilePath, std::ios::in);
	if (vertexShaderStream.is_open()){
		std::string line = "";
		while (getline(vertexShaderStream, line)) {
			vertexShaderCode += "\n" + line;
		}
		vertexShaderStream.close();
	}
	else {
		printf("Vertex Shader Datei kann nicht gelesen werden. Datei: %s.\n", vertexFilePath);
		return 0;
	}

	// Fragment Shader Code aus der Datei auslesen
	std::string fragmentShaderCode;
	std::ifstream fragmentShaderStream(fragmentFilePath, std::ios::in);
	if (fragmentShaderStream.is_open()){
		std::string line = "";
		while (getline(fragmentShaderStream, line)) {
			fragmentShaderCode += "\n" + line;
		}
		fragmentShaderStream.close();
	}
	else {
		printf("Fragment Shader Datei kann nicht gelesen werden. Datei: %s.\n", fragmentFilePath);
		return 0;
	}

	// Variablen für die Prüfung der kompilierten Shader
	GLint result = GL_FALSE;
	int infoLogLength = 0;

	// Vertex Shader kompilieren
	char const* vertexSourcePointer = vertexShaderCode.c_str();
	glShaderSource(vertexShaderId, 1, &vertexSourcePointer, NULL);
	glCompileShader(vertexShaderId);

	// Vertex Shader prüfen
	glGetShaderiv(vertexShaderId, GL_COMPILE_STATUS, &result);
	glGetShaderiv(vertexShaderId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 0) { // Da ging etwas schief
		std::vector<char> vertexShaderErrorMessage(infoLogLength + 1);
		glGetShaderInfoLog(vertexShaderId, infoLogLength, NULL, &vertexShaderErrorMessage[0]);
		printf("Der Vertex Shader konnte nicht kompiliert werden.\nFehlermeldung: [%s]\n", &vertexShaderErrorMessage[0]);
	}

	// Fragment Shader kompilieren
	char const* fragmentSourcePointer = fragmentShaderCode.c_str();
	glShaderSource(fragmentShaderId, 1, &fragmentSourcePointer, NULL);
	glCompileShader(fragmentShaderId);

	// Fragment Shader prüfen
	glGetShaderiv(fragmentShaderId, GL_COMPILE_STATUS, &result);
	glGetShaderiv(fragmentShaderId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 0) { // Da ging etwas schief
		std::vector<char> fragmentShaderErrorMessage(infoLogLength + 1);
		glGetShaderInfoLog(fragmentShaderId, infoLogLength, NULL, &fragmentShaderErrorMessage[0]);
		printf("Der Fragment Shader konnte nicht kompiliert werden.\nFehlermeldung: [%s]\n", &fragmentShaderErrorMessage[0]);
	}

	// Shader Programm linken
	GLuint programId = glCreateProgram();
	glAttachShader(programId, vertexShaderId);
	glAttachShader(programId, fragmentShaderId);
	glLinkProgram(programId);

	// Shader Programm prüfen
	glGetProgramiv(programId, GL_LINK_STATUS, &result);
	glGetProgramiv(programId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 0) { // da ging etwas schief
		std::vector<char> programErrorMessage(infoLogLength + 1);
		glGetProgramInfoLog(programId, infoLogLength, NULL, &programErrorMessage[0]);
		printf("Das Shader Programm konnte nicht erstellt werden.\nFehlermeldung: [%s]\n", &programErrorMessage[0]);
	}

	// Vertex und Fragment Shader werden nicht mehr benötigt, da wir ja jetzt das fertige Shader Programm haben.
	glDeleteShader(vertexShaderId);
	glDeleteShader(fragmentShaderId);

	return programId;
}