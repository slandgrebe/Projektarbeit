// Headers
#include "stdafx.h"

// Link statically with GLEW
#define GLEW_STATIC

#include "GraphicEngine.h"
#include <fstream>
#include <string>
#include <vector>

#include "Manager.h"

#include <future>

using namespace visual::graphics;

// static attributes
GraphicEngine* GraphicEngine::singleInstance = 0;
GLFWwindow* GraphicEngine::window = 0;

GraphicEngine* GraphicEngine::getInstance() {
	if (singleInstance == 0) {
		singleInstance = new GraphicEngine;

		std::async(worker);
	}

	return singleInstance;
}


GraphicEngine::GraphicEngine() {
	camera = new Camera;
	projectionMatrix = glm::perspective(60.0f, (float)width/height, 0.1f, 100.0f);
	orthographicMatrix = glm::ortho(-1.0f, 1.0f, -1.0f, 1.0f, -5.0f, 5.0f);
	viewOrthographicMatrix = orthographicMatrix;

	running = false;
	title = "Projektarbeit";
	width = 640;
	height = 480;
}

GraphicEngine::~GraphicEngine() {
	delete camera;
}

glm::mat4 GraphicEngine::getViewProjectionMatrix() {
	return viewProjectionMatrix;
}
glm::mat4 GraphicEngine::getViewOrthographicMatrix() {
	return viewOrthographicMatrix;
}

void GraphicEngine::worker(void) {
	if (0 != GraphicEngine::getInstance()->createWindow()) {
		return;
	}

	if (0 != GraphicEngine::getInstance()->createOpenGLContext()) {
		return;
	}
	
	GraphicEngine::getInstance()->running = true;

	/***************
		LOOP
	****************/

	// Enable depth test
	glEnable(GL_DEPTH_TEST);
	// Accept fragment if it closer to the camera than the former one
	glDepthFunc(GL_LESS);

	glEnable(GL_BLEND);
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	// measure time
	clock_t begin = clock();
	clock_t now = clock();
	float timeDifference = 0.0f;

	while (!glfwWindowShouldClose(window)) {
		timeDifference = (float)(now - begin) / 1000.0f;

		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS) {
			glfwSetWindowShouldClose(window, GL_TRUE);
			break;
		}

		// camera
		GraphicEngine::getInstance()->camera->advance(timeDifference);
		GraphicEngine::getInstance()->viewProjectionMatrix = GraphicEngine::getInstance()->projectionMatrix * GraphicEngine::getInstance()->camera->getViewMatrix();

		// create new objects
		GraphicEngine::getInstance()->processQueue();

		// Clear the screen to black
		glClearColor(0.3f, 0.5f, 0.9f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		// Draw objects
		Manager::getInstance()->draw();

		// Swap buffers
		glfwSwapBuffers(window);
		glfwPollEvents();

		// measure time
		now = clock();
		//Log().debug() << "Bild gezeichnet in " << int(now - begin) / CLOCKS_PER_SEC << "ms. Das entspricht " << 1000 / (now - begin) << " FPS." ;
		begin = now;
	}

	GraphicEngine::getInstance()->running = false;

	glfwTerminate();
}

void GraphicEngine::enqueueSquare(GLuint modelId, std::string filename) {
	Log().debug() << "enqueueSquare modelId[" << modelId << "] filename[" << filename << "]" ;
	
	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	squareQueue.enqueue(e);
}
void GraphicEngine::enqueueModel(GLuint modelId, std::string filename) {
	Log().debug() << "enqueueSquare modelId[" << modelId << "] filename[" << filename << "]" ;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	modelQueue.enqueue(e);
}
void GraphicEngine::enqueueText(GLuint modelId, std::string filename) {
	Log().debug() << "enqueueText modelId[" << modelId << "] filename[" << filename << "]" ;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	textQueue.enqueue(e);
}
void GraphicEngine::enqueueButton(GLuint modelId, std::string filename) {
	Log().debug() << "enqueueButton modelId[" << modelId << "] filename[" << filename << "]" ;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	buttonQueue.enqueue(e);
}
void GraphicEngine::enqueueDispose(GLuint modelId) {
	disposeQueue.enqueue(modelId);
}
/*void GraphicsEngine::disposeModel(GLuint modelId) {

}*/

void GraphicEngine::processQueue() {
	while (modelQueue.hasMore()) {
		modelQueueEntry e = modelQueue.dequeue();
		model::AssimpModel* model = new model::AssimpModel;
		if (model->loadModel(e.filename)) {
			Manager::getInstance()->addToModelList(e.modelId, model);
		}
		else {
			Log().error() << "Could not create model. id[" << e.modelId << "] filename[" << e.filename << "]" ;
		}
	}

	while (squareQueue.hasMore()) {		
		modelQueueEntry e = squareQueue.dequeue();
		model::Square* model = new model::Square;
		if (model->loadFromFile(e.filename)) {
			Manager::getInstance()->addToSquareList(e.modelId, model);
		}
		else {
			Log().error() << "Could not create square. id[" << e.modelId << "] filename[" << e.filename << "]";
		}
	}

	while (textQueue.hasMore()) {
		modelQueueEntry e = textQueue.dequeue();
		gui::Text* text = new gui::Text;
		if (text->init(e.filename)) {
			Manager::getInstance()->addToTextList(e.modelId, text);
		}
		else {
			Log().error() << "Could not create text. id[" << e.modelId << "] text[" << e.filename << "]";
		}
	}

	while (buttonQueue.hasMore()) {
		modelQueueEntry e = buttonQueue.dequeue();
		gui::Button* button = new gui::Button;
		if (button->init(e.filename)) {
			Manager::getInstance()->addToButtonList(e.modelId, button);
		}
		else {
			Log().error() << "Could not create button. id[" << e.modelId << "] fontname[" << e.filename << "]";
		}
	}

	while (disposeQueue.hasMore()) {
		GLuint modelId = disposeQueue.dequeue();
		Manager::getInstance()->remove(modelId);
	}
}

int GraphicEngine::createWindow() {
	// Initialisiere GLFW
	if (!glfwInit()) {
		//fprintf(stderr, "GLFW Library konnte nicht initialisiert werden.\n");
		Log().error() << "GLFW Library konnte nicht initialisiert werden.";
		return 1;
	}

	glfwWindowHint(GLFW_SAMPLES, 4); // Multisampling
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3); // OpenGL Version 3.3
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE); // Core Profile (keine veralteten Funktionen)
	glfwWindowHint(GLFW_RESIZABLE, GL_FALSE); // Fenstergr�sse kann nicht ver�ndert werden

	// OpenGL Kontext erstellen und Fenster �ffnen
	window = glfwCreateWindow(width, height, title.c_str(), 0, 0);
	if (!window) {
		//fprintf(stderr, "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.\n");
		Log().error() << "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.";
		glfwTerminate();
		return 2;
	}
	glfwMakeContextCurrent(window);

	//Log().info() << "Fenster erstellt" ;
	Log().info() << "Fenster erstellt";

	return 0;
}

int GraphicEngine::createOpenGLContext(void) {
	// Initialisiere GLEW - muss nach GLFW Initialisierung getan werden!
	glewExperimental = GL_TRUE; // wird beim Core Profile ben�tigt
	GLenum err = glewInit();
	if (GLEW_OK != err) {
		// glewInit ist fehlgeschlagen
		//fprintf(stderr, "GLEW Initialisierungsfehler: %s\n", glewGetErrorString(err));
		Log().error() << "GLEW Initialisierungsfehler: " << glewGetErrorString(err);
		return 3;
	}

	Log().info() << "GLEW initialisiert" ;

	return 0;
}