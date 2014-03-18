#ifndef VISUALIZATIONIMPL_H
#define VISUALIZATIONIMPL_H

#include "stdafx.h"
#include "Visualization.h"

// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

// GLM - Mathe Library
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

/*
#include "Point.h"
#include "TexturedTriangle.h"
#include "Line.h"
#include "Mesh.h"
*/
#include <map>



namespace visual {

	//using namespace model;

	/**
	* Die Klasse VisualizationImpl implementiert das Visualization Interface.
	* @author Stefan Landgrebe
	* @see Visualization
	*/
	class VisualizationImpl : public Visualization {
	private:
		static VisualizationImpl* singleInstance;
		GLFWwindow* window; /** Referenz das das Fenster */
		int status; /** Statuscode der Initalisierung */

		GLuint programId; /** Referenz auf das Shader Programm */
		GLuint matrixId; /** Referenz auf die Moder-View-Projection Matrix im Shader Programm */
		GLuint textureId; /** Referenz auf die Textur im Shader Programm */

		glm::mat4 projectionMatrix; /** Projection Matrix */
		glm::mat4 viewMatrix; /** View Matrix */

		//std::map<int, TexturedTriangle*> pointList;
		int pointCounter;
		//std::map<int, Line*> lineList;
		int lineCounter;
		//std::map<int, Mesh*> meshList;
		int meshCounter;

		VisualizationImpl();
		~VisualizationImpl() {};

		int initalize(void);
		GLuint loadShaders(const char* vertexFilePath, const char* fragmentFilePath);

	public:
		static VisualizationImpl* getInstance();

		/** Liefert den Status der Visualisierung zurück
		* @author Stefan Landgrebe
		* @return status: 0 = ok, 1 = GLFW Initialisierungsfehler, 2 = Fehler beim erstellen des Fensters, 3 = GLEW Initialisierungsfehler, 4 = Fehler bei der Erstellung des Shaderprogramms
		*/
		int getStatus(void) { return status; }

		int addPoint(void);
		void updatePoint(int pointId, float x, float y, float z);

		int addLine(void);
		void updateLine(int lineId, float x1, float y1, float z1, float x2, float y2, float z2);

		int addMesh(void);
		void updateMesh(int meshId, float x, float y, float z);

		void draw(void);
		void close(void);
	};
}
////////////////////////////////////////////////////////////////////////////////
// Factory function that creates instances if the Visualization object.

// Export both decorated and undecorated names.
//   initalize    - Undecorated name, which can be easily used with GetProcAddress
//                         Win32 API function.
//   _initalize@0 - Common name decoration for __stdcall functions in C language.
//
// For more information on name decoration see here:
// "Format of a C Decorated Name"
// http://msdn.microsoft.com/en-us/library/x7kb4e2f.aspx

#if !defined(_WIN64)
// This pragma is required only for 32-bit builds. In a 64-bit environment,
// C functions are not decorated.
#pragma comment(linker, "/export:close=_close@0")
#pragma comment(linker, "/export:draw=_draw@0")
#pragma comment(linker, "/export:addPoint=_addPoint@0")
#pragma comment(linker, "/export:updatePoint=_updatePoint@16")
#pragma comment(linker, "/export:addLine=_addLine@0")
#pragma comment(linker, "/export:updateLine=_updateLine@28")
#pragma comment(linker, "/export:addMesh=_addMesh@0")
#pragma comment(linker, "/export:updateMesh=_updateMesh@16")
#endif  // _WIN64

/** Implementierung der addPoint Methode. Die addPoint Methode fügt ein neues Dreieck hinzu. Diese Methode liefert die pointId zurück.
* @author Stefan Landgrebe
* @return pointId
*/
VISUALIZATION_API int WINAPI addPoint(void) {
	return visual::VisualizationImpl::getInstance()->addPoint();
}

/** Implementierung der updatePoint Methode. Die updatePoint Methode setzt die Position eines Dreiecks. Es werden die pointId sowie die x, y und z Koordinate der neuen Position.
* @author Stefan Landgrebe
*/
VISUALIZATION_API void WINAPI updatePoint(int pointId, float x, float y, float z) {
	visual::VisualizationImpl::getInstance()->updatePoint(pointId, x, y, z);
}

/** Implementierung der addLine Methode. Die addLine Methode fügt eine neue Linie hinzu. Diese Methode liefert die lineId zurück.
* @author Stefan Landgrebe
* @return lineId
*/
VISUALIZATION_API int WINAPI addLine(void) {
	return visual::VisualizationImpl::getInstance()->addLine();
}

/** Implementierung der updateLine Methode. Die updateLine Methode setzt die Positionen der Endpunkte einer Linie. Es werden die lineId sowie die Koordinaten der beiden Punkte erwartet.
* @author Stefan Landgrebe
*/
VISUALIZATION_API void WINAPI updateLine(int lineId, float x1, float y1, float z1, float x2, float y2, float z2) {
	visual::VisualizationImpl::getInstance()->updateLine(lineId, x1, y1, z1, x2, y2, z2);
}

/** Implementierung der addMesh Methode. Die addMesh Methode fügt eine neues Mesh hinzu. Diese Methode liefert die meshId zurück
* @author Stefan Landgrebe
* @return meshId
*/
VISUALIZATION_API int WINAPI addMesh(void) {
	return visual::VisualizationImpl::getInstance()->addMesh();
}

/** Implementierung der updateMesh Methode. Die updateMesh Methode setzt die Positionen der Endpunkte einer Linie. Es werden die meshId sowie die Koordinaten der neuen Position erwartet.
* @author Stefan Landgrebe
*/
VISUALIZATION_API void WINAPI updateMesh(int meshId, float x, float y, float z) {
	visual::VisualizationImpl::getInstance()->updateMesh(meshId, x, y, z);
}


/** Implementierung der draw Methode. Die draw Methode zeichnet das Fenster neu.
* @author Stefan Landgrebe
*/
VISUALIZATION_API void WINAPI draw(void) {
	visual::VisualizationImpl::getInstance()->draw();
}

/** Implementierung der close Methode. Die close Funktion schliesst das Fenster
* @author Stefan Landgrebe
*/
VISUALIZATION_API void WINAPI close(void) {
	visual::VisualizationImpl::getInstance()->close();
}

#endif 
