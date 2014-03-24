#ifndef TEXTURESOIL_H
#define	TEXTURESOIL_H

#include <GL/glew.h>
#include "Texture.h"
#include <string>

namespace visual {
	namespace model {
		class TextureSoil : Texture {
		public:
			TextureSoil();
			~TextureSoil();

			bool load(const std::string& filename);
			void bind(GLenum textureUnit = GL_TEXTURE0);

		private:
			std::string filename;
			GLenum textureTarget;
			GLuint textureId;
		};
	}
}

#endif

