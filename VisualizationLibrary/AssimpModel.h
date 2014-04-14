#ifndef ASSIMPMODEL_H
#define ASSIMPMODEL_H




// Include GLEW - muss vor GLFW inkludiert werden!!!
#include <GL/glew.h>

// Include GLFW
#include <GLFW/glfw3.h>

// GLM - Mathe Library
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>

// assimp
#include <assimp/Importer.hpp>
#include <assimp/scene.h>
#include <assimp/postprocess.h>

#include "Model.h"
#include "TextureSoil.h"

namespace visual {
	namespace model {

		/** AssimpModel erbt von der abstrakten Klasse Model. 
		Die Klasse wird zum laden von 3D Modellen gebraucht und verwendet dafür die Assimp Library, welche unzählige Formate unterstützt.
		* @author Stefan Landgrebe
		* @see <a href="http://assimp.sourceforge.net/">http://assimp.sourceforge.net/</a> 
		*/
		class AssimpModel : public Model {
		private:
			GLuint vertexArrayId; /** Referenz auf das VAO (Vertex Array Object) */
			
			bool initAllMeshes(const aiScene* scene, const std::string& filename);
			bool initSingleMesh(const int meshIndex, const aiMesh* mesh);
			bool initMaterials(const aiScene* scene, const std::string& filename);
			//bool initTexture() {};
			void clear();

			struct Vertex {
				glm::vec3 position;
				glm::vec2 textureCoordinate;
				glm::vec3 normal;

				Vertex() {}

				Vertex(const glm::vec3& position, const glm::vec2& textureCoordinate, const glm::vec3& normal) {
					this->position = position;
					this->textureCoordinate = textureCoordinate;
					this->normal = normal;
				}
			};

			struct MeshEntry {
				MeshEntry();
				~MeshEntry();

				void init(const std::vector<Vertex>& vertices,
					const std::vector<unsigned int>& indices);

				GLuint vertexBufferId;
				GLuint indexBufferId;
				unsigned int numIndices;
				unsigned int materialIndex;
			};	

			std::vector<MeshEntry> meshList;
			std::vector<TextureSoil*> textureList;

		public:

			/** Konstruktor
			Für das eigentliche Laden des 3D Modells muss zusätzlich die Methode loadModel() aufgerufen werden.
			* @author Stefan Landgrebe
			* @see loadModel()
			*/
			AssimpModel();

			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~AssimpModel();

			/** Implementierung der Model::loadModel() Methode.
			Liest die übergebene Datei aus und übergibt die Vertices an OpenGL.
			* @author Stefan Landgrebe
			* @param filename Dateipfad der 3D Modell Datei
			* @return Prüfung ob die Operation durchgeführt werden konnte.
			* @see Model::loadModel()
			*/
			bool loadModel(const std::string filename);

			/** Zeichnet das Modell neu
			* @author Stefan Landgrebe
			*/
			void draw(void);
		};
	}
}

#endif