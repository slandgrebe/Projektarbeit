#ifndef GRAPHICSENGING_H
#define GRAPHICSENGING_H

#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <iostream>
#include <SOIL.h>
#include "SafeQueue.h"
#include "Camera.h"

namespace visual {

	/** Der Graphics Namespace beinhaltet alle Klassen, welche vor allem direkt mit OpenGL kommunizieren
	* @author Stefan Landgrebe
	*/
	namespace graphics {

		/** Verantwortlich für den OpenGL Kontext, das Fenster und den kontrollierten und kontinuierlichen Ablauf der Bibliothek.
		Die Abarbeitung und somit das Fenster, laufen sich in einem separaten Thread. 
		Zur Erstellung des OpenGL Kontext wird die GLEW (OpenGL Extension Wrangler) Library verwendet und zur Erstellung des Fensters wird die GLFW (keine Bedeutung) Library verwendet.
		Es handelt sich hierbei um einen Singleton.
		* @author Stefan Landgrebe
		* @see <a href="http://glew.sourceforge.net/">http://glew.sourceforge.net/</a>
		* @see <a href="http://www.glfw.org/">http://www.glfw.org/</a>
		*/
		class GraphicEngine {
		public:
			
			/** Factory Method des Singleton
			* @author Stefan Landgrebe
			* @return Instanz der GraphicEngine Klasse
			*/
			static GraphicEngine* getInstance();

			/** Öffnet das Fenster
			* @author Stefan Landgrebe
			* @param windowTitle Fesntertitel
			* @param fullscreen Fullscreen
			* @param windowWidth Fensterbreite, bei 0 wird native Bildschirmbreite verwendet
			* @param windowHeight Fensterhöhe, bei 0 wird die native Bildschirmhöhe verwendet
			*/
			bool init(std::string windowTitle = "Projektarbeit", bool fullscreen = false, unsigned int windowWidth = 640, unsigned int windowHeight = 480);

			/** Prüft den Zustand des Fensters.
			* @author Stefan Landgrebe
			* @return True wenn das Fenster offen ist, ansonsten False
			*/
			bool isRunning(void) { return running; }

			/** Sorgt dafür dass das Fenster geschlossen wird
			* @author Stefan Landgrebe
			*/
			void close(void) { glfwSetWindowShouldClose(window, GL_TRUE); }


			/** Warteschlange für neu zu erstellende Objekte der Klasse Square.
			Dies ist aufgrund der Threadsicherheit notwendig.
			* @author Stefan Landgrebe
			* @param modelId ID des Modells
			* @param filename Dateipfad des Bildes
			* @see Manager::addPoint()
			*/
			void enqueueSquare(GLuint modelId, std::string filename);

			/** Warteschlange für neu zu erstellende Objekte der Klasse AssimpModel.
			Dies ist aufgrund der Threadsicherheit notwendig.
			* @author Stefan Landgrebe
			* @param modelId ID des Modells
			* @param filename Dateipfad 3D Modells
			* @see Manager::addModel()
			*/
			void enqueueModel(GLuint modelId, std::string filename);

			/** Warteschlange für neu zu erstellende Objekte der Klasse Text.
			Dies ist aufgrund der Threadsicherheit notwendig.
			* @author Stefan Landgrebe
			* @param modelId ID des Modells
			* @param filename Dateipfad der Schriftart
			* @see Manager::addText()
			*/
			void enqueueText(GLuint modelId, std::string filename);

			/** Warteschlange für neu zu erstellende Objekte der Klasse Button.
			Dies ist aufgrund der Threadsicherheit notwendig.
			* @author Stefan Landgrebe
			* @param modelId ID des Modells
			* @param filename Dateipfad der Schriftart
			* @see Manager::addButton()
			*/
			void enqueueButton(GLuint modelId, std::string filename);

			/** Warteschlange zur Löschung von Objekten.
			Dies ist aufgrund der Threadsicherheit notwendig.
			* @author Stefan Landgrebe
			* @param modelId ID des Modells
			* @see Manager::dispose()
			* @see Manager::remove()
			*/
			void enqueueDispose(GLuint modelId);


			/** Liefert die Höhe des Fensters
			* @author Stefan Landgrebe
			* @return Höhe in Pixeln
			*/
			int getWindowHeight(void) { return height; }

			/** Liefert die Breite des Fensters
			* @author Stefan Landgrebe
			* @return Breite in Pixeln
			*/
			int getWindowWidth(void) { return width; }

			/** Liefert das Kamera Objekt zurück
			* @author Stefan Landgrebe
			* @return Kamera Objekt
			* @see Camera
			*/
			Camera* camera(void);


			/** Liefert die Projektsionsmatrix
			* @author Stefan Landgrebe
			* @return Projektsionsmatrix
			*/
			glm::mat4 getProjectionMatrix();

			/** Liefert die View-Projektsionsmatrix
			* @author Stefan Landgrebe
			* @return View-Projektsionsmatrix
			*/
			glm::mat4 getViewProjectionMatrix();

			/** Liefert die orthographische View-Projektsionsmatrix
			* @author Stefan Landgrebe
			* @return View-Projektsionsmatrix
			*/
			glm::mat4 getViewOrthographicMatrix();


			//int printOglError(char *file, int line);

		private:
			bool running;

			static GraphicEngine* singleInstance;
			static GLFWwindow* window;
			std::string title;
			bool fullscreen;
			int width;
			int height;

			glm::mat4 projectionMatrix;
			glm::mat4 viewProjectionMatrix;
			glm::mat4 orthographicMatrix;
			glm::mat4 viewOrthographicMatrix;
			Camera* m_camera;

			struct modelQueueEntry {
				GLuint modelId;
				std::string filename;
			};

			SafeQueue<modelQueueEntry> squareQueue;
			SafeQueue<modelQueueEntry> modelQueue;
			SafeQueue<modelQueueEntry> textQueue;
			SafeQueue<modelQueueEntry> buttonQueue;
			SafeQueue<GLuint> disposeQueue;

			enum shaderType {VERTEX, FRAGMENT, GEOMETRY};

			GraphicEngine();
			~GraphicEngine();

			static void worker();
			void processQueue();

			int createWindow();
			int createOpenGLContext(void);
		};
	}
}


#endif