#include "stdafx.h"
#include "AssimpModel.h"
#include "GraphicEngine.h"
#include <glm/gtc/type_ptr.hpp>
#include <iostream>
#include "Math.h"

using namespace visual::model;

/***************************
	MeshEntry
****************************/
AssimpModel::MeshEntry::MeshEntry() {
	vertexBufferId = 0;
	indexBufferId = 0;
	numIndices = 0;
	materialIndex = 0;
};

AssimpModel::MeshEntry::~MeshEntry() {
	if (vertexBufferId != 0) {
		glDeleteBuffers(1, &vertexBufferId);
	}

	if (indexBufferId != 0) {
		glDeleteBuffers(1, &indexBufferId);
	}
}

void AssimpModel::MeshEntry::init(	const std::vector<Vertex>& vertices,
									const std::vector<unsigned int>& indices) {
	numIndices = indices.size();

	glGenBuffers(1, &vertexBufferId);
	glBindBuffer(GL_ARRAY_BUFFER, vertexBufferId);
	glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex)* vertices.size(), &vertices[0], GL_STATIC_DRAW);

	glGenBuffers(1, &indexBufferId);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBufferId);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(unsigned int)* numIndices, &indices[0], GL_STATIC_DRAW);

	// collision detection
	numTriangles = indices.size() / 3;
	for (unsigned int i = 0; i < numTriangles; i++) {
		Log().debug() << "fill triangle List " << i;
		Triangle t = Triangle(vertices[indices[i * 3 + 0]].position, 
								vertices[indices[i * 3 + 1]].position, 
								vertices[indices[i * 3 + 2]].position);
		this->triangles.push_back(t);
	}

	/*for (std::vector<unsigned int>::const_iterator i = indices.begin(); i != indices.end(); ++i)
		Log().debug() << *i << ' ';

	Log().debug() ;*/
}

AssimpModel::Triangle AssimpModel::MeshEntry::getTriangle(unsigned int n) {
	Triangle t = Triangle(glm::vec3(0), glm::vec3(0), glm::vec3(0));

	// testen ob n g�ltig ist
	if (n < numTriangles) {
		t = triangles[n];
	}
	else {
		// warning
		Log().warning() << "AssimpModel::MeshEntry::getTriangle(): n zu gross.";
	}

	return t;
}



AssimpModel::AssimpModel() {}


AssimpModel::~AssimpModel() {
	//delete shaderProgram;
	while (!textureList.empty()) delete textureList.back(), textureList.pop_back();
}

bool AssimpModel::loadModel(const std::string filename) {
	Log().debug() << "loadModel[" << filename << "]" ;

	bool returnValue = false;

	// Create Vertex Array Object
	glGenVertexArrays(1, &vertexArrayId);
	glBindVertexArray(vertexArrayId);

	Log().debug() << "assimp vao: " << vertexArrayId ;
	
	// shader
	//shaderProgram = new graphics::ShaderProgram;
	shaderProgram.createShaderProgram("data/shader/SimpleVertexShader.vertexshader", "data/shader/SimpleFragmentShader.fragmentshader");

	// Release the previously loaded mesh (if it exists)
	clear();

	Assimp::Importer importer;
	const aiScene* scene = importer.ReadFile(filename,	aiProcess_CalcTangentSpace |
														aiProcess_Triangulate |
														aiProcess_JoinIdenticalVertices |
														aiProcess_SortByPType);

	if (scene) {
		Log().debug() << " Anzahl der Meshes in dieser Datei: " << scene->mNumMeshes ;
		returnValue = this->initAllMeshes(scene, filename);
	}
	else {
		Log().error() << "Fehler beim parsen der Datei '" << filename.c_str() << " ': '" << importer.GetErrorString() << "'\n";
	}

	if (m_boundingSphereRadius > 0) {
		m_scalingNormalizationFactor = 1 / (2 * m_boundingSphereRadius);
		scale(m_scalingVector);
	}

	// Make sure the VAO is not changed from outside code
	glBindVertexArray(0);

	return returnValue;
}

bool AssimpModel::initAllMeshes(const aiScene* scene, const std::string& filename) {
	meshList.resize(scene->mNumMeshes);
	textureList.resize(scene->mNumMaterials);

	// Initialize the meshes in the scene one by one
	for (unsigned int i = 0; i < meshList.size(); i++) {
		const aiMesh* mesh = scene->mMeshes[i];
		if (!initSingleMesh(i, mesh)) {
			return false;
		}
	}

	return initMaterials(scene, filename);
}

bool AssimpModel::initSingleMesh(const int meshIndex, const aiMesh* mesh) {
	Log().debug() << "  Mesh #" << meshIndex << "; Anzahl Seiten: " << mesh->mNumFaces ;

	meshList[meshIndex].materialIndex = mesh->mMaterialIndex;

	std::vector<Vertex> vertices;
	std::vector<unsigned int> indices;

	const aiVector3D zero3D(0.0f, 0.0f, 0.0f);

	// Vertices
	for (unsigned int i = 0; i < mesh->mNumVertices; i++) {
		const aiVector3D* pos = &(mesh->mVertices[i]);
		const aiVector3D* normal = &(mesh->mNormals[i]);
		const aiVector3D* texCoord = mesh->HasTextureCoords(0) ? &(mesh->mTextureCoords[0][i]) : &zero3D;
		
		Vertex v(	glm::vec3(pos->x, pos->y, pos->z),
					glm::vec2(texCoord->x, texCoord->y),
					glm::vec3(normal->x, normal->y, normal->z));

		// bounding Sphere
		float radius = sqrt(pos->x * pos->x + pos->y * pos->y + pos->z * pos->z); // pythagoras
		if (radius > m_boundingSphereRadius) m_boundingSphereRadius = radius;
		//if (std::abs(pos->x) > m_boundingSphereRadius) m_boundingSphereRadius = std::abs(pos->x);
		//if (std::abs(pos->y) > m_boundingSphereRadius) m_boundingSphereRadius = std::abs(pos->y);
		//if (std::abs(pos->z) > m_boundingSphereRadius) m_boundingSphereRadius = std::abs(pos->z);

		vertices.push_back(v);
	}

	// Faces
	for (unsigned int i = 0; i < mesh->mNumFaces; i++) {
		const aiFace& face = mesh->mFaces[i];
		assert(face.mNumIndices == 3);
		indices.push_back(face.mIndices[0]);
		indices.push_back(face.mIndices[1]);
		indices.push_back(face.mIndices[2]);
	}

	meshList[meshIndex].init(vertices, indices);

	return true;
}

bool AssimpModel::initMaterials(const aiScene* scene, const std::string& filename) {
	// Extract the directory part from the file name
	std::string::size_type slashIndex = filename.find_last_of("/");
	std::string dir;

	if (slashIndex == std::string::npos) {
		dir = ".";
	}
	else if (slashIndex == 0) {
		dir = "/";
	}
	else {
		dir = filename.substr(0, slashIndex);
	}

	bool returnValue = true;

	Log().debug() << " Anzahl der Texturen: " << scene->mNumMaterials ;

	for (unsigned int i = 0; i < scene->mNumMaterials; i++) {
		const aiMaterial* material = scene->mMaterials[i];

		textureList[i] = NULL;

		if (material->GetTextureCount(aiTextureType_DIFFUSE) > 0) {
			aiString path;

			if (material->GetTexture(aiTextureType_DIFFUSE, 0, &path, NULL, NULL, NULL, NULL, NULL) == AI_SUCCESS) {
				std::string fullPath = dir + "/" + path.data;
				textureList[i] = new TextureSoil();

				Log().debug() << "  Textur #" << i << "; Pfad: " << fullPath ;

				if (!textureList[i]->loadFromFile(fullPath.c_str())) {
					Log().error() << "Error loading texture '" << fullPath.c_str() << "'";
					delete textureList[i];
					textureList[i] = NULL;
					returnValue = false;
				}
				else {
					Log().debug() << "Loaded texture '" << fullPath.c_str() << "'";
				}
			}
		}

		// Load a white texture in case the model does not include its own texture
		if (!textureList[i]) {
			textureList[i] = new TextureSoil();
			returnValue = textureList[i]->loadFromFile("data/textures/fallback.png");

			Log().warning() << "  Benutze Fallback Textur" ;
		}
	}

	return returnValue;
}

void AssimpModel::clear() {
	for (unsigned int i = 0; i < textureList.size(); i++) {
		if (textureList[i]) { 
			delete textureList[i];
			textureList[i] = NULL;
		}
	}
}

bool AssimpModel::doesIntersect(AssimpModel* other) {
	// mit sich selber testen macht wenig sinn
	if (this == other) {
		return false;
	}

	glm::vec3 pos1 = this->position(); // attachedToCamera in der position Methode noch ber�cksichtigen!
	glm::vec3 pos2 = other->position();

	float radius1 = this->boundingSphereRadius();
	float radius2 = other->boundingSphereRadius();

	// sehr einfacher, schneller, ungenauer Test zuerst
	if (pow(pos1.x - pos2.x, 2) + pow(pos1.y - pos2.y, 2) + pow(pos1.z - pos2.z, 2) <= pow(radius1 + radius2, 2)) { // ber�hren oder schneiden sich die Bounding Spheres?

		return true;

		// zu langsam :(

		// measure time
		clock_t begin = clock();
		float timeDifference = 0.0f;

		// sehr komplexer, langsamer, exakter Test danach
		glm::mat4 mvp1 = this->getTransformedModelMatrix();
		glm::mat4 mvp2 = other->getTransformedModelMatrix();

		// Jedes Teilmodell von diesem Modell...
		for (unsigned int i = 0; i < this->meshList.size(); i++) {

			// ... und davon jedes Dreieck ...
			for (unsigned int j = 0; j < this->meshList[i].numTriangles; j++) {
				// This Triangle
				Triangle t1 = this->meshList[i].triangles[j];

				glm::vec3 a1 = glm::vec3(mvp1 * glm::vec4(t1.a, 1.0));
				glm::vec3 b1 = glm::vec3(mvp1 * glm::vec4(t1.b, 1.0));
				glm::vec3 c1 = glm::vec3(mvp1 * glm::vec4(t1.c, 1.0));

				// ... wird mit jedem Teilmodell des anderen Modells...
				for (unsigned int k = 0; k < other->meshList.size(); k++) {
					// ... und davon mit jedem Dreieck getestet.
					for (unsigned int l = 0; l < other->meshList[k].numTriangles; l++) {
						// Other Triangle
						Triangle t2 = other->meshList[k].triangles[l];

						glm::vec3 a2 = glm::vec3(mvp2 * glm::vec4(t2.a, 1.0));
						glm::vec3 b2 = glm::vec3(mvp2 * glm::vec4(t2.b, 1.0));
						glm::vec3 c2 = glm::vec3(mvp2 * glm::vec4(t2.c, 1.0));

						// Test
						if (math::Math::doTrianglesIntersect(a1, b1, c1,  a2, b2, c2)) { // ber�hren oder schneiden sich die zwei Dreiecke?
							return true;
						}
					}
				}
			}
		}
		// doch keine kollision

		//Log().debug() << "time: " << (float)(clock() - begin) / 1.0f;

	} // if - einfacher test

	return false;
}

void AssimpModel::draw() {
	//Log().debug() << "draw AssimpModel" ;
	//Log().debug() << " Anzahl Meshes: " << meshList.size() ;
	
	glBindVertexArray(vertexArrayId);

	// get Shader Program Reference
	//GLuint shaderProgramId = graphics::GraphicEngine::getInstance()->getShaderProgramId();

	shaderProgram.use();

	// positions
	GLint posAttrib = shaderProgram.getAttribute("position");
	glEnableVertexAttribArray(posAttrib);

	// texture
	GLint texAttrib = shaderProgram.getAttribute("texcoord");
	glEnableVertexAttribArray(texAttrib);

	for (unsigned int i = 0; i < meshList.size(); i++) {
		glBindBuffer(GL_ARRAY_BUFFER, meshList[i].vertexBufferId);
		
		// Position
		glVertexAttribPointer(
			posAttrib,          // layout Attribut im Vertex Shader
			3,                  // Gr�sse
			GL_FLOAT,           // Datentyp
			GL_FALSE,           // normalisiert?
			sizeof(Vertex),     // Stride
			(void*)0            // Offset
		);
		
		// Texture
		glVertexAttribPointer(
			texAttrib,          // layout Attribut im Vertex Shader
			2,                  // Gr�sse
			GL_FLOAT,           // Datentyp
			GL_FALSE,           // normalisiert?
			sizeof(Vertex),     // Stride
			(const GLvoid*)12   // Offset
		);

		/*glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), 0);
		glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), (const GLvoid*)12);
		glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), (const GLvoid*)20);*/

		glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, meshList[i].indexBufferId);

		const unsigned int MaterialIndex = meshList[i].materialIndex;

		if (MaterialIndex < textureList.size() && textureList[MaterialIndex]) {
			textureList[MaterialIndex]->bind(GL_TEXTURE0);
		}

		// highlight color
		GLint highlightAttribute = shaderProgram.getUniform("highlightColor");
		GLfloat r = 1.0f, g = 1.0f, b = 1.0f, a = 1.0f; // standard: weiss

		if (m_isHighlighted) {
			r = highlightColor.r;
			g = highlightColor.g;
			b = highlightColor.b;
			a = highlightColor.a;
			//Log().debug() << "Model is highlighted!" << r << "/" << g << "/" << b << "/" << a;
		}
		glUniform4f(highlightAttribute, r, g, b, a);

		// transformations
		GLint uniMvp = shaderProgram.getUniform("mvp");
		glm::mat4 mvp = getModelViewMatrix();
		glUniformMatrix4fv(uniMvp, 1, GL_FALSE, glm::value_ptr(mvp));

		glDrawElements(GL_TRIANGLES, meshList[i].numIndices, GL_UNSIGNED_INT, 0);
	}

	// Vertex Shader Attribute Array deaktivieren
	glDisableVertexAttribArray(posAttrib);
	//glDisableVertexAttribArray(colAttrib);
	glDisableVertexAttribArray(texAttrib);

	// Make sure the VAO is not changed from the outside 
	glBindVertexArray(0);
}