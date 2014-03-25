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

//#include <thread> //<thread> is not supported when compiling with /clr or /clr:pure.

using namespace visual::graphics;
//using namespace System::Threading;

// Shader sources
const GLchar* vertexSource =
"#version 150 core\n"
"in vec2 position;"
"in vec3 color;"
"in vec2 texcoord;"
"out vec3 Color;"
"out vec2 Texcoord;"
"void main() {"
"   Color = color;"
"   Texcoord = texcoord;"
"   gl_Position = vec4(position, 0.0, 1.0);"
"}";
const GLchar* fragmentSource =
"#version 150 core\n"
"in vec3 Color;"
"in vec2 Texcoord;"
"out vec4 outColor;"
"uniform sampler2D tex;"
"void main() {"
"   outColor = texture(tex, Texcoord) * vec4(Color, 1.0);"
"}";


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


GraphicEngine* GraphicEngine::getInstance() {
	if (singleInstance == 0) {
		singleInstance = new GraphicEngine;

		// http://www.codeproject.com/Articles/12740/Threads-with-Windows-Forms-Controls-in-Managed-C
		//GraphicEngine^ objclass = gcnew GraphicEngine;
		/*ThreadStart^ mThread = gcnew ThreadStart(&GraphicEngine::worker);
		Thread^ uiThread = gcnew Thread(mThread);
		uiThread->Start();*/

		std::async(worker);
	}

	return singleInstance;
}


GraphicEngine::GraphicEngine() {
}

GraphicEngine::~GraphicEngine() {
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
	/**************
		BUFFER
	***************/
	/*// Create Vertex Array Object
	GLuint vao;
	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);

	// Create a Vertex Buffer Object and copy the vertex data to it
	GLuint vertexBufferId;
	glGenBuffers(1, &vertexBufferId);
	GLfloat vertices[] = {
		//  Position				Color			Texcoords
		-0.5f, 0.5f,  // Top-left
		0.5f, 0.5f,  // Top-right
		0.5f, -0.5f,  // Bottom-right
		-0.5f, -0.5f,   // Bottom-left
	};
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);
	GLint posAttrib = glGetAttribLocation(shaderProgramId, "position");
	glEnableVertexAttribArray(posAttrib);
	glVertexAttribPointer(
		posAttrib,          // layout Attribut im Vertex Shader
		2,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
		);

	GLuint colorBufferId;
	glGenBuffers(1, &colorBufferId);
	GLfloat colors[] = {
		1.0f, 0.0f, 0.0f,
		0.0f, 1.0f, 0.0f,
		0.0f, 0.0f, 1.0f,
		1.0f, 1.0f, 1.0f,
	};
	glBindBuffer(GL_ARRAY_BUFFER, colorBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);
	GLint colAttrib = glGetAttribLocation(shaderProgramId, "color");
	glEnableVertexAttribArray(colAttrib);
	glVertexAttribPointer(
		colAttrib,          // layout Attribut im Vertex Shader
		3,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
		);

	GLuint uvsBufferId;
	glGenBuffers(1, &uvsBufferId);
	GLfloat uvs[] = {
		0.0f, 0.0f,
		1.0f, 0.0f,
		1.0f, 1.0f,
		0.0f, 1.0f
	};
	glBindBuffer(GL_ARRAY_BUFFER, uvsBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(uvs), uvs, GL_STATIC_DRAW);
	GLint texAttrib = glGetAttribLocation(shaderProgramId, "texcoord");
	glEnableVertexAttribArray(texAttrib);
	glVertexAttribPointer(
		texAttrib,          // layout Attribut im Vertex Shader
		2,                  // Grösse
		GL_FLOAT,           // Datentyp
		GL_FALSE,           // normalisiert?
		0,                  // Stride
		(void*)0            // Offset
		);

	// Create an element array
	GLuint ebo;
	glGenBuffers(1, &ebo);

	GLuint elements[] = {
		0, 1, 2,
		2, 3, 0
	};

	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(elements), elements, GL_STATIC_DRAW);
	*/


	/**************
		TEXTURE
	***************/

	// Load texture
	/*GLuint tex;
	glGenTextures(1, &tex);

	int width, height;
	//"E:/dev/opengl/ProjektarbeitTest/TextureTest/Debug/test.png"
	unsigned char* image = SOIL_load_image("sample.png", &width, &height, NULL, SOIL_LOAD_RGB);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, width, height, 0, GL_RGB, GL_UNSIGNED_BYTE, image);
	GLenum error = glGetError();

	std::cout << "---------------------------------------------------------" << std::endl;
	std::cout << "SOIL Error: " << error << ": " << gluErrorString(error) << std::endl;
	std::cout << width << " " << height << std::endl;
	std::cout << SOIL_last_result() << std::endl;
	std::cout << "---------------------------------------------------------" << std::endl;

	SOIL_free_image_data(image);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	*/

	/***************
		LOOP
	****************/
	while (!glfwWindowShouldClose(window))
	{
		if (glfwGetKey(window, GLFW_KEY_ESCAPE) == GLFW_PRESS) {
			glfwSetWindowShouldClose(window, GL_TRUE);
			break;
		}

		glfwSwapBuffers(window);
		glfwPollEvents();

		GraphicEngine::getInstance()->processQueue();

		// Clear the screen to black
		glClearColor(0.3f, 0.5f, 0.9f, 1.0f);
		glClear(GL_COLOR_BUFFER_BIT);

		// Draw a rectangle from the 2 triangles using 6 indices
		//glDrawElements(GL_TRIANGLES, 6, GL_UNSIGNED_INT, 0);
		Manager::getInstance()->draw();

		// Swap buffers
	}

	//glDeleteTextures(1, &tex);

	glDeleteProgram(GraphicEngine::getInstance()->shaderProgramId);
	//glDeleteShader(fragmentShader);
	//glDeleteShader(vertexShader);

	/*glDeleteBuffers(1, &ebo);
	glDeleteBuffers(1, &vertexBufferId);

	glDeleteVertexArrays(1, &vao);*/
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

void GraphicEngine::processQueue() {
	while (modelQueue->hasMore()) {
		modelQueueEntry e = modelQueue->dequeue();
		model::AssimpModel* model = new model::AssimpModel;
		if (model->loadModel(e.filename)) {
			Manager::getInstance()->addToModelList(e.modelId, model);
		}
	}

	while (squareQueue->hasMore()) {		
		modelQueueEntry e = squareQueue->dequeue();
		model::Square* model = new model::Square;
		if (model->loadFromFile(e.filename)) {
			Manager::getInstance()->addToSquareList(e.modelId, model);
		}
	}

	while (textQueue->hasMore()) {
		modelQueueEntry e = textQueue->dequeue();
		gui::Text* text = new gui::Text;
		Manager::getInstance()->addToTextList(e.modelId, text);
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