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

			struct Triangle {
				Triangle(glm::vec3 a, glm::vec3 b, glm::vec3 c) {
					this->a = a;
					this->b = b;
					this->c = c;
				}

				glm::vec3 a;
				glm::vec3 b;
				glm::vec3 c;
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

				
				unsigned int numTriangles; 
				std::vector<Triangle> triangles;

				Triangle getTriangle(unsigned int n);
			};	

			std::vector<MeshEntry> meshList;
			std::vector<TextureSoil*> textureList;
			// Collision Detection
			float xMax = 0;
			float xMin = 0;
			float yMax = 0;
			float yMin = 0;
			float zMax = 0;
			float zMin = 0;

			std::vector<Triangle> collisionModel;

			
			long unsigned int collisionModelUpdatedOnFrame;

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


			bool addCollisionModel(const std::string filename);

			void updateCollisionModel(long unsigned int frame);

			/** Prüft ob dieses Modell ein anderes schneidet.
			* @author Stefan Landgrebe
			* @param other das andere Modell
			* @return True wenn es min. 1 Schnittpunkt gibt.
			*/
			bool doesIntersect(AssimpModel* other, long unsigned int frame);

			/** Zeichnet das Modell neu
			* @author Stefan Landgrebe
			*/
			void draw(void);
		};
	}
}

#endif