#ifndef MANAGER_H
#define MANAGER_H
#include "msclr\marshal_cppstd.h"
#include "Square.h"
#include "AssimpModel.h"
#include "GraphicEngine.h"
#include <map>
#include "SafeQueue.h"

namespace visual {
	class Manager {
	public:
		static Manager* getInstance(void);

		void processQueue(void);

		GLint doSomething(GLint n);
		GLuint addModel(const ::std::string filename);
		GLuint addPoint(const ::std::string textureFilename = "sample.png");

		GLboolean isModelCreated(GLuint modelId);

		GLboolean positionModel(GLuint modelId, glm::vec3 position);
		GLboolean rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis);
		GLboolean scaleModel(GLuint modelId, glm::vec3 scale);

		void draw(void);
	private:
		static Manager* instance;

		GLuint modelInstantiationCounter = 0;
		::std::map<GLuint, model::AssimpModel*> assimpModelList;
		::std::map<GLuint, model::Square*> squareList;
		::std::vector<GLuint> squareQueue;

		model::Square* square = 0;
		model::AssimpModel* assimpModel = 0;

		Manager(void);

		GLboolean isRunning(void);
	};

	// da man mutex in einem Projekt mit managed code nicht verwenden darf, muss man auf die .Net Lösung zurückgreifen
	// die .Net Lösung kann nur in managed code verwendet werden
	// damit man aus unmanaged code auf managed code zugreifen kann, muss man mit static methoden arbeiten
	// (und aus irgendeinem grund, kriege ich es nicht hin, einen managed singleton zu erstellen)
	//http://msdn.microsoft.com/en-us/library/de0542zz(v=vs.110).aspx?cs-save-lang=1&cs-lang=cpp#code-snippet-1

	using namespace System;
	using namespace System::Threading;
	using namespace System::Collections::Generic;

	public ref class SquareQueue {
	private:
		// A queue that is protected by Monitor.
		static Queue<unsigned int>^ m_inputQueue = gcnew Queue<unsigned int>();

		/*SafeQueue()	{
		m_inputQueue = gcnew Queue<unsigned int>();
		};*/

	public:
		// Lock the queue and add an element. 
		static void enqueue(unsigned int qValue) {
			// Request the lock, and block until it is obtained.
			Monitor::Enter(m_inputQueue);

			try	{
				// When the lock is obtained, add an element.
				m_inputQueue->Enqueue(qValue);
			}
			finally {
				// Ensure that the lock is released.
				Monitor::Exit(m_inputQueue);
			}
		};

		// Lock the queue and dequeue an element.
		static unsigned int dequeue() {
			unsigned int retval = 0;

			// Request the lock, and block until it is obtained.
			Monitor::Enter(m_inputQueue);
			try	{
				if (m_inputQueue->Count > 0) {
					// When the lock is obtained, dequeue an element.
					retval = m_inputQueue->Dequeue();
				}
			}
			finally	{
				// Ensure that the lock is released.
				Monitor::Exit(m_inputQueue);
			}

			return retval;
		};

		static bool hasMore() {
			bool retval = false;

			Monitor::Enter(m_inputQueue);
			try {
				if (m_inputQueue->Count > 0) {
					retval = true;
				}
			}
			finally {
				Monitor::Exit(m_inputQueue);
			}

			return retval;
		}

	};

	public ref class ModelQueue {
	private:
		// A queue that is protected by Monitor.
		//static Queue<unsigned int>^ m_inputQueue = gcnew Queue<unsigned int>();

		static Dictionary<GLuint, String^>^ m_queue = gcnew Dictionary<GLuint, String^>();
		/*SafeQueue()	{
		m_inputQueue = gcnew Queue<unsigned int>();
		};*/

	public:
		// Lock the queue and add an element. 
		static void enqueue(GLuint modelId, ::std::string filename) {
			// Request the lock, and block until it is obtained.
			Monitor::Enter(m_queue);

			try	{
				System::String^ str = gcnew String(filename.c_str());

				// When the lock is obtained, add an element.
				m_queue->Add(modelId, str);
			}
			finally {
				// Ensure that the lock is released.
				Monitor::Exit(m_queue);
			}
		};

		// Lock the queue and dequeue an element.
		static unsigned int dequeue(::std::string& filename) {
			unsigned int retval = 0;
			String^ str = "";
			// Request the lock, and block until it is obtained.
			Monitor::Enter(m_queue);
			try	{
				if (m_queue->Count > 0) {
					for each (GLuint modelId in m_queue->Keys) {
						m_queue->TryGetValue(modelId, str);
						m_queue->Remove(modelId);
						retval = modelId;
						break;
					}
					filename = msclr::interop::marshal_as<::std::string>(str);
					::std::cout << "dequeue: " << filename << ::std::endl;
				}

			}
			finally	{
				// Ensure that the lock is released.
				Monitor::Exit(m_queue);
			}

			return retval;
		};

		static bool hasMore() {
			bool retval = false;

			Monitor::Enter(m_queue);
			try {
				if (m_queue->Count > 0) {
					retval = true;
				}
			}
			finally {
				Monitor::Exit(m_queue);
			}

			return retval;
		}

	};
}

#endif