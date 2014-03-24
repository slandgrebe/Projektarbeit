#include "stdafx.h"
#include "AssimpModel.h"
#include "GraphicEngine.h"
#include <glm/gtc/type_ptr.hpp>
#include <iostream>

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

	/*for (std::vector<unsigned int>::const_iterator i = indices.begin(); i != indices.end(); ++i)
		std::cout << *i << ' ';

	std::cout << std::endl;*/
}



AssimpModel::AssimpModel() {
	

	// Matrizen
	m_modelMatrix = glm::mat4(1.0f);
	m_positionVector = glm::vec3(0.0f, 0.0f, 0.0f);
	m_rotationAngle = 0.0f;
	m_rotationAxis = glm::vec3(1.0f, 0.0f, 0.0f);
	m_scalingVector = glm::vec3(1.0f, 1.0f, 1.0f);

	// .obj Datei lesen
	//bool result = loadModel("TexturedPlane_ImageUv_248.blend");
}


AssimpModel::~AssimpModel() {
}

bool AssimpModel::loadModel(const std::string filename) {
	std::cout << "loadModel[" << filename << "]" << std::endl;

	bool returnValue = false;

	// Create Vertex Array Object
	glGenVertexArrays(1, &vertexArrayId);
	glBindVertexArray(vertexArrayId);

	std::cout << "assimp vao: " << vertexArrayId << std::endl;
	
	// Release the previously loaded mesh (if it exists)
	clear();

	Assimp::Importer importer;
	const aiScene* scene = importer.ReadFile(filename,	aiProcess_CalcTangentSpace |
														aiProcess_Triangulate |
														aiProcess_JoinIdenticalVertices |
														aiProcess_SortByPType);

	if (scene) {
		std::cout << " Anzahl der Meshes in dieser Datei: " << scene->mNumMeshes << std::endl;
		returnValue = this->initAllMeshes(scene, filename);
	}
	else {
		printf("Fehler beim parsen der Datei '%s': '%s'\n", filename.c_str(), importer.GetErrorString());
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
	std::cout << "  Mesh #" << meshIndex << "; Anzahl Seiten: " << mesh->mNumFaces << std::endl;

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

	std::cout << " Anzahl der Texturen: " << scene->mNumMaterials << std::endl;

	for (unsigned int i = 0; i < scene->mNumMaterials; i++) {
		const aiMaterial* material = scene->mMaterials[i];

		textureList[i] = NULL;

		if (material->GetTextureCount(aiTextureType_DIFFUSE) > 0) {
			aiString path;

			if (material->GetTexture(aiTextureType_DIFFUSE, 0, &path, NULL, NULL, NULL, NULL, NULL) == AI_SUCCESS) {
				std::string fullPath = dir + "/" + path.data;
				textureList[i] = new TextureSoil();

				std::cout << "  Textur #" << i << "; Pfad: " << fullPath << std::endl;

				if (!textureList[i]->load(fullPath.c_str())) {
					printf("Error loading texture '%s'\n", fullPath.c_str());
					delete textureList[i];
					textureList[i] = NULL;
					returnValue = false;
				}
				else {
					printf("Loaded texture '%s'\n", fullPath.c_str());
				}
			}
		}

		// Load a white texture in case the model does not include its own texture
		if (!textureList[i]) {
			textureList[i] = new TextureSoil();
			returnValue = textureList[i]->load("data/textures/red.jpg");

			std::cout << "  Benutze Fallback Textur" << std::endl;
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

void AssimpModel::draw() {
	//std::cout << "draw AssimpModel" << std::endl;
	//std::cout << " Anzahl Meshes: " << meshList.size() << std::endl;
	
	glBindVertexArray(vertexArrayId);

	// get Shader Program Reference
	GLuint shaderProgramId = graphics::GraphicEngine::getInstance()->getShaderProgramId();

	// positions
	GLint posAttrib = glGetAttribLocation(shaderProgramId, "position");
	glEnableVertexAttribArray(posAttrib);

	// texture
	GLint texAttrib = glGetAttribLocation(shaderProgramId, "texcoord");
	glEnableVertexAttribArray(texAttrib);

	for (unsigned int i = 0; i < meshList.size(); i++) {
		glBindBuffer(GL_ARRAY_BUFFER, meshList[i].vertexBufferId);
		
		// Position
		glVertexAttribPointer(
			posAttrib,          // layout Attribut im Vertex Shader
			3,                  // Grösse
			GL_FLOAT,           // Datentyp
			GL_FALSE,           // normalisiert?
			sizeof(Vertex),     // Stride
			(void*)0            // Offset
		);
		
		// Texture
		glVertexAttribPointer(
			texAttrib,          // layout Attribut im Vertex Shader
			2,                  // Grösse
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

		// transformations
		GLint uniMvp = glGetUniformLocation(shaderProgramId, "mvp");
		glm::mat4 mvp = getTransformedMatrix();
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