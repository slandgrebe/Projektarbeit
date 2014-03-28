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
			AssimpModel();
			~AssimpModel();

			bool loadModel(const std::string filename);

			/** Zeichnet das Modell
			* @author Stefan Landgrebe
			*/
			void draw(void);
		};
	}
}

#endif