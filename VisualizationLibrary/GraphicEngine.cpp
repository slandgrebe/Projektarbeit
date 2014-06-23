// Headers
#include "stdafx.h"

// Link statically with GLEW


#include "GraphicEngine.h"
#include <fstream>
#include <string>
#include <vector>

#include "Manager.h"

#include <future>

using namespace visual::graphics;


// Hilfsfunktion //http://www.lighthouse3d.com/cg-topics/error-tracking-in-opengl/
#define printOpenGLError() printOglError(__FILE__, __LINE__)

int printOglError(char *file, int line) {
	GLenum glErr;
	int    retCode = 0;

	glErr = glGetError();
	if (glErr != GL_NO_ERROR) {
		Log().debug() << "glError in file " << file << " @ line " << line << ": " << gluErrorString(glErr);
		/*printf("glError in file %s @ line %d: %s\n",
			file, line, gluErrorString(glErr));*/
		retCode = 1;
	}
	return retCode;
}

// static attributes
GraphicEngine* GraphicEngine::singleInstance = 0;
GLFWwindow* GraphicEngine::window = 0;

GraphicEngine* GraphicEngine::getInstance() {
	if (singleInstance == 0) {
		singleInstance = new GraphicEngine;
	}

	return singleInstance;
}


GraphicEngine::GraphicEngine() {
	m_camera = new Camera;
	projectionMatrix = glm::perspective(60.0f, (float)width/height, 0.1f, 100.0f);
	orthographicMatrix = glm::ortho(-1.0f, 1.0f, -1.0f, 1.0f, -5.0f, 5.0f);
	viewOrthographicMatrix = orthographicMatrix;

	running = false;
	title = "Projektarbeit";
	fullscreen = false;
	width = 640;
	height = 480;
}

GraphicEngine::~GraphicEngine() {
	delete m_camera;
}

bool GraphicEngine::init(std::string windowTitle, bool fullscreen, unsigned int windowWidth, unsigned int windowHeight) {
	if (running) {
		return false;
	}


	title = windowTitle;
	this->fullscreen = fullscreen;
	width = windowWidth;
	height = windowHeight;
	
	std::async(worker);

	return true;
}


Camera* GraphicEngine::camera(void) {
	return m_camera;
}
glm::mat4 GraphicEngine::getProjectionMatrix() {
	return projectionMatrix;
}
glm::mat4 GraphicEngine::getViewProjectionMatrix() {
	return viewProjectionMatrix;
}
glm::mat4 GraphicEngine::getViewOrthographicMatrix() {
	return viewOrthographicMatrix;
}

void GraphicEngine::worker(void) {
	Log().debug() << "worker started";
	
	if (0 != GraphicEngine::getInstance()->createWindow()) {
		return;
	}

	if (0 != GraphicEngine::getInstance()->createOpenGLContext()) {
		return;
	}
	
	GraphicEngine::getInstance()->running = true;

	printOpenGLError();

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
		GraphicEngine::getInstance()->m_camera->advance(timeDifference);
		GraphicEngine::getInstance()->viewProjectionMatrix = GraphicEngine::getInstance()->projectionMatrix * GraphicEngine::getInstance()->m_camera->getViewMatrix();

		// create new objects
		GraphicEngine::getInstance()->processQueue();

		// collision detection
		Manager::getInstance()->doCollisionDetection();

		// Clear the screen to black
		glClearColor(0.3f, 0.5f, 0.9f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

		// Draw objects
		Manager::getInstance()->draw();

		// Swap buffers
		glfwSwapBuffers(window);
		glfwPollEvents();

		printOpenGLError();

		// measure time
		begin = now;
		now = clock();
		//Log().debug() << "Bild gezeichnet in " << int(now - begin) / CLOCKS_PER_SEC << "ms. Das entspricht " << 1000 / (now - begin) << " FPS." ;
		//Log().debug() << "Bild gezeichnet in " << timeDifference << "ms. " << 1 / timeDifference << " FPS";
	}

	glfwTerminate(); 
	GraphicEngine::getInstance()->running = false;
	Log().info() << "Fenster geschlossen";
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
		Log().fatal() << "GLFW Library konnte nicht initialisiert werden.";
		return 1;
	}

	glfwWindowHint(GLFW_SAMPLES, 4); // Multisampling
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3); // OpenGL Version 3.3
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE); // Core Profile (keine veralteten Funktionen)
	glfwWindowHint(GLFW_RESIZABLE, GL_FALSE); // Fenstergrösse kann nicht verändert werden

	// Bildschirmauflösung auslesen
	const GLFWvidmode * mode = glfwGetVideoMode(glfwGetPrimaryMonitor());
	
	Log().debug() << "createWindow width[" << width << "] height[" << height << "]";

	if (width == 0) {
		width = mode->width;
	}
	if (height == 0) {
		height = mode->height;
	}

	// OpenGL Kontext erstellen und Fenster öffnen
	if (fullscreen) window = glfwCreateWindow(width, height, title.c_str(), glfwGetPrimaryMonitor(), 0);
	else window = glfwCreateWindow(width, height, title.c_str(), 0, 0);
	if (!window) {
		//fprintf(stderr, "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.\n");
		Log().fatal() << "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.";
		glfwTerminate();
		return 2;
	}
	glfwMakeContextCurrent(window);

	Log().info() << "Fenster erstellt";
	printOpenGLError();


	return 0;
}

int GraphicEngine::createOpenGLContext(void) {
	// Initialisiere GLEW - muss nach GLFW Initialisierung getan werden!
	glewExperimental = GL_TRUE; // wird beim Core Profile benötigt
	GLenum err = glewInit();
	printOpenGLError();

	if (GLEW_OK != err) {
		// glewInit ist fehlgeschlagen
		//fprintf(stderr, "GLEW Initialisierungsfehler: %s\n", glewGetErrorString(err));
		Log().fatal() << "GLEW Initialisierungsfehler: " << glewGetErrorString(err);
		return 3;
	}

	Log().info() << "GLEW initialisiert" ;
	printOpenGLError();

	return 0;
}