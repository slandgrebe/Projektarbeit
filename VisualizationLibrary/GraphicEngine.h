#ifndef GRAPHICSENGING_H
#define GRAPHICSENGING_H

#include <GL/glew.h>
#include <GLFW/glfw3.h>
#include <iostream>
#include <SOIL.h>
#include "SafeQueue.h"
#include "Camera.h"

namespace visual {
	namespace graphics {
		class GraphicEngine {
		public:
			static GraphicEngine* getInstance();
			bool isRunning(void) { return running; }
			void enqueueSquare(GLuint modelId, std::string filename);
			void enqueueModel(GLuint modelId, std::string filename);
			void enqueueText(GLuint modelId, std::string filename);
			void enqueueButton(GLuint modelId, std::string filename);
			void enqueueDispose(GLuint modelId);

			int getWindowHeight(void) { return height; }
			int getWindowWidth(void) { return width; }

			glm::mat4 getViewProjectionMatrix();
			glm::mat4 getViewOrthographicMatrix();

		private:
			bool running;

			static GraphicEngine* singleInstance;
			static GLFWwindow* window;
			std::string title;
			int width;
			int height;

			glm::mat4 projectionMatrix;
			glm::mat4 viewProjectionMatrix;
			glm::mat4 orthographicMatrix;
			glm::mat4 viewOrthographicMatrix;
			Camera* camera;

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