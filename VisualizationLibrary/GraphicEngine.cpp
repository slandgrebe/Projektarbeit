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
std::string GraphicEngine::title = "Projektarbeit";
int GraphicEngine::width = 640;
int GraphicEngine::height = 480;
bool GraphicEngine::running = false;
GLuint GraphicEngine::shaderProgramId = 0;
SafeQueue<GraphicEngine::modelQueueEntry>* GraphicEngine::squareQueue = new SafeQueue<modelQueueEntry>;
SafeQueue<GraphicEngine::modelQueueEntry>* GraphicEngine::modelQueue = new SafeQueue<modelQueueEntry>;
SafeQueue<GraphicEngine::modelQueueEntry>* GraphicEngine::textQueue = new SafeQueue<modelQueueEntry>;
SafeQueue<GraphicEngine::modelQueueEntry>* GraphicEngine::buttonQueue = new SafeQueue<modelQueueEntry>;


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
	orthographicMatrix = glm::ortho(0, width, 0, height);
}

GraphicEngine::~GraphicEngine() {
}

glm::mat4 GraphicEngine::getViewProjectionMatrix() {
	return viewProjectionMatrix;
}
glm::mat4 GraphicEngine::getViewOrthographicMatrix() {
	return viewOrthographicMatrix;
}

void GraphicEngine::worker(void) {
	if (0 != GraphicEngine::getInstance()->createWindow(title, width, height)) {
		return;
	}

	if (0 != GraphicEngine::getInstance()->createOpenGLContext()) {
		return;
	}

	// Shader
	
	GLuint shaderProgramId = GraphicEngine::getInstance()->createShaderProgram("data/shader/SimpleVertexShader.vertexshader", "data/shader/SimpleFragmentShader.fragmentshader");
	if (0 == shaderProgramId) {
		return;
	}
	
	running = true;

	glBindFragDataLocation(shaderProgramId, 0, "outColor");

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
		//std::cout << "Bild gezeichnet in " << int(now - begin) / CLOCKS_PER_SEC << "ms. Das entspricht " << 1000 / (now - begin) << " FPS." << std::endl;
		begin = now;
	}

	//glDeleteTextures(1, &tex);

	glDeleteProgram(GraphicEngine::getInstance()->shaderProgramId);
	//glDeleteShader(fragmentShader);
	//glDeleteShader(vertexShader);

	/*glDeleteBuffers(1, &ebo);
	glDeleteBuffers(1, &vertexBufferId);

	glDeleteVertexArrays(1, &vao);*/

	running = false;

	glfwTerminate();
}

void GraphicEngine::enqueueSquare(GLuint modelId, std::string filename) {
	std::cout << "enqueueSquare modelId[" << modelId << "] filename[" << filename << "]" << std::endl;
	
	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	squareQueue->enqueue(e);
}
void GraphicEngine::enqueueModel(GLuint modelId, std::string filename) {
	std::cout << "enqueueSquare modelId[" << modelId << "] filename[" << filename << "]" << std::endl;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	modelQueue->enqueue(e);
}
void GraphicEngine::enqueueText(GLuint modelId, std::string text) {
	std::cout << "enqueueText modelId[" << modelId << "] text[" << text << "]" << std::endl;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = text;
	textQueue->enqueue(e);
}
void GraphicEngine::enqueueButton(GLuint modelId, std::string filename) {
	std::cout << "enqueueButton modelId[" << modelId << "] text[" << filename << "]" << std::endl;

	modelQueueEntry e;
	e.modelId = modelId;
	e.filename = filename;
	buttonQueue->enqueue(e);
}

void GraphicEngine::processQueue() {
	while (modelQueue->hasMore()) {
		modelQueueEntry e = modelQueue->dequeue();
		model::AssimpModel* model = new model::AssimpModel;
		if (model->loadModel(e.filename)) {
			Manager::getInstance()->addToModelList(e.modelId, model);
		}
		else {
			std::cout << "Could not create model. id[" << e.modelId << "] filename[" << e.filename << "]" << std::endl;
		}
	}

	while (squareQueue->hasMore()) {		
		modelQueueEntry e = squareQueue->dequeue();
		model::Square* model = new model::Square;
		if (model->loadFromFile(e.filename)) {
			Manager::getInstance()->addToSquareList(e.modelId, model);
		}
		else {
			std::cout << "Could not create square. id[" << e.modelId << "] filename[" << e.filename << "]" << std::endl;
		}
	}

	while (textQueue->hasMore()) {
		modelQueueEntry e = textQueue->dequeue();
		gui::Text* text = new gui::Text;
		if (text->init(e.filename)) {
			Manager::getInstance()->addToTextList(e.modelId, text);
		}
		else {
			std::cout << "Could not create text. id[" << e.modelId << "] text[" << e.filename << "]" << std::endl;
		}
	}

	while (buttonQueue->hasMore()) {
		modelQueueEntry e = buttonQueue->dequeue();
		gui::Button* button = new gui::Button;
		if (button->init(e.filename)) {
			Manager::getInstance()->addToButtonList(e.modelId, button);
		}
		else {
			std::cout << "Could not create button. id[" << e.modelId << "] filename[" << e.filename << "]" << std::endl;
		}
	}
}

int GraphicEngine::createWindow(const std::string title, int width, int height) {
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
	window = glfwCreateWindow(width, height, title.c_str(), 0, 0);
	if (!window) {
		fprintf(stderr, "GLFW Fenster konnte nicht geoeffnet werden. OpenGL Version 3.3 wird vorausgesetzt.\n");
		glfwTerminate();
		return 2;
	}
	glfwMakeContextCurrent(window);


	std::cout << "Fenster erstellt" << std::endl;

	return 0;
}

int GraphicEngine::createOpenGLContext(void) {
	// Initialisiere GLEW - muss nach GLFW Initialisierung getan werden!
	glewExperimental = GL_TRUE; // wird beim Core Profile benötigt
	GLenum err = glewInit();
	if (GLEW_OK != err) {
		// glewInit ist fehlgeschlagen
		fprintf(stderr, "GLEW Initialisierungsfehler: %s\n", glewGetErrorString(err));
		return 3;
	}

	std::cout << "GLEW initialisiert" << std::endl;

	return 0;
}

GLuint GraphicEngine::createShader(const std::string filename, GraphicEngine::shaderType m_shaderType) {
	// Shader erstellen
	GLuint shaderId;
	
	if (m_shaderType == shaderType::VERTEX) {
		shaderId = glCreateShader(GL_VERTEX_SHADER);
	}
	else if (m_shaderType == shaderType::FRAGMENT) {
		shaderId = glCreateShader(GL_FRAGMENT_SHADER);
	}
	else {
		std::cout << "Unknown Shader Type." << std::endl;
		return 0;
	}
	// Vertex Shader Code aus der Datei auslesen
	std::string shaderCode;
	std::ifstream shaderStream(filename.c_str(), std::ios::in);
	if (shaderStream.is_open()){
		std::string line = "";
		while (getline(shaderStream, line)) {
			shaderCode += "\n" + line;
		}
		shaderStream.close();

		/*std::cout << "-- Shader Source -------------------------------------" << std::endl;
		std::cout << shaderCode << std::endl;
		std::cout << "------------------------------------------------------" << std::endl;*/
	}
	else {
		printf("Shader Datei kann nicht gelesen werden. Datei: %s.\n", filename.c_str());
		return 0;
	}

	// Variablen für die Prüfung der kompilierten Shader
	GLint result = GL_FALSE;
	int infoLogLength = 0;

	// Vertex Shader kompilieren
	const GLchar* sourcePointer = shaderCode.c_str();

	glShaderSource(shaderId, 1, &sourcePointer, NULL);
	glCompileShader(shaderId);

	// Vertex Shader prüfen
	glGetShaderiv(shaderId, GL_COMPILE_STATUS, &result);
	glGetShaderiv(shaderId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 1) { // Da ging etwas schief
		std::vector<char> vertexShaderErrorMessage(infoLogLength + 1);
		glGetShaderInfoLog(shaderId, infoLogLength, NULL, &vertexShaderErrorMessage[0]);
		printf("Der Vertex Shader konnte nicht kompiliert werden.\nFehlermeldung: [%s]\n", &vertexShaderErrorMessage[0]);
	}

	return shaderId;
}
GLuint GraphicEngine::createShaderProgram(GLuint vertexShaderId, GLuint fragmentShaderId) {
	// Shader Programm linken
	GLuint programId = glCreateProgram();
	glAttachShader(programId, vertexShaderId);
	glAttachShader(programId, fragmentShaderId);
	//glBindFragDataLocation(programId, 0, "outColor");
	glLinkProgram(programId);

	// Variablen für die Prüfung der kompilierten Shader
	GLint result = GL_FALSE;
	int infoLogLength = 0;

	// Shader Programm prüfen
	glGetProgramiv(programId, GL_LINK_STATUS, &result);
	glGetProgramiv(programId, GL_INFO_LOG_LENGTH, &infoLogLength);
	if (infoLogLength > 1) { // da ging etwas schief
		std::vector<char> programErrorMessage(infoLogLength + 1);
		glGetProgramInfoLog(programId, infoLogLength, NULL, &programErrorMessage[0]);
		printf("Das Shader Programm konnte nicht erstellt werden.\nFehlermeldung: [%s]\n", &programErrorMessage[0]);
	}

	// Vertex und Fragment Shader werden nicht mehr benötigt, da wir ja jetzt das fertige Shader Programm haben.
	//glDeleteShader(vertexShaderId);
	//glDeleteShader(fragmentShaderId);

	return programId;
}

GLuint GraphicEngine::createShaderProgram(const std::string vertexShaderFilename, const std::string fragmentShaderfilename) {
	GLuint vertexShaderId = createShader(vertexShaderFilename, shaderType::VERTEX);
	GLuint fragmentShaderId = createShader(fragmentShaderfilename, shaderType::FRAGMENT);
	shaderProgramId = createShaderProgram(vertexShaderId, fragmentShaderId);
	glUseProgram(shaderProgramId);

	return shaderProgramId;
}

GLuint GraphicEngine::getShaderProgramId(void) {
	return shaderProgramId;
}

void GraphicEngine::deleteShaderProgram(const GLuint shaderProgramId) {
	glDeleteProgram(shaderProgramId);
}