// VisualizationTester.cpp : main project file.

#include <iostream>
#include <vector>
#include <time.h>


using namespace std;

bool test() {
	int n = 4;
	if (visual::Visualization::doSomething(n) == n + 1) {
		cout << "test ok" << endl;
		return false;
	}
	
	cout << "test not ok" << endl;
	return false;
}
/*
	POINT
*/
namespace testPoint {
	bool testPoint_CreateObject(unsigned int& modelId) {
		modelId = 0;
		modelId = visual::Visualization::addPoint();

		if (modelId) {
			return true;
		}

		return false;
	}
	bool testPoint_Position(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			bool b = visual::Visualization::positionModel(modelId, 0.25f, 0.25f, 0.25f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}

		return false;
	}
	bool testPoint_Rotate(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			bool b = visual::Visualization::rotateModel(modelId, 80.0f, 1.0f, 0.0f, 0.0f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}

		return false;
	}
	bool testPoint_Scale(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			bool b = visual::Visualization::scaleModel(modelId, 0.5f, 0.5f, 0.5f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}

		return false;
	}
	bool testPoint() {
		vector<bool> results;
		unsigned int modelId = 0;

		results.push_back(testPoint_CreateObject(modelId));
		results.push_back(testPoint_Position(modelId));
		results.push_back(testPoint_Rotate(modelId));
		results.push_back(testPoint_Scale(modelId));

		int count = 0;
		for (std::vector<bool>::iterator it = results.begin(); it != results.end(); ++it) {
			count++;
			bool result = *it;
			std::cout << "Test " << count << ": ";
			if (result) {
				std::cout << "erfolgreich";
			}
			else {
				std::cout << "fehlgeschlagen";
			}
			std::cout << std::endl;
		}

		return false;
	}
}
/*
	MODEL
*/
namespace testModel {
	bool testModel_CreateObject(unsigned int& modelId) {
		modelId = 0;
		//std::string filename = "cube.obj";
		System::String^ filename = "cube.obj";
		modelId = visual::Visualization::addModel(filename);

		if (modelId) {
			return true;
		}

		return false;
	}
	bool testModel_Position(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}


		begin = clock();
		while (true) {
			bool b = visual::Visualization::positionModel(modelId, 0.25f, 0.25f, -0.25f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}

		return false;
	}
	bool testModel_Rotate(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			bool b = visual::Visualization::rotateModel(modelId, 80.0f, 1.0f, 0.0f, 0.0f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 1) {
				break;
			}
		}

		return false;
	}
	bool testModel_Scale(unsigned int modelId) {
		clock_t begin = clock();
		while (true) {
			bool b = visual::Visualization::scaleModel(modelId, 0.5f, 0.5f, 0.5f);
			if (b) {
				//std::cout << " Erfolg nach " << clock() - begin << "ms" << std::endl;
				return true;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 5) {
				break;
			}
		}

		return false;
	}
	bool testModel() {
		vector<bool> results;
		unsigned int modelId = 0;

		results.push_back(testModel_CreateObject(modelId));
		results.push_back(testModel_Position(modelId));
		results.push_back(testModel_Rotate(modelId));
		results.push_back(testModel_Scale(modelId));

		int count = 0;
		for (std::vector<bool>::iterator it = results.begin(); it != results.end(); ++it) {
			count++;
			bool result = *it;
			std::cout << "Test " << count << ": ";
			if (result) {
				std::cout << "erfolgreich";
			}
			else {
				std::cout << "fehlgeschlagen";
			}
			std::cout << std::endl;
		}

		return false;
	}
}

bool shuttle() {
	unsigned int modelId = 0;
	System::String^ filename = "models/shuttle/SpaceShuttleOrbiter.3ds";
	//System::String^ filename = "textures/SpaceShuttleOrbiter.3ds";

	modelId = visual::Visualization::addModel(filename);

	if (modelId) {
		clock_t begin = clock();

		while (true) {
			if (visual::Visualization::isModelCreated(modelId)) {
				//bool b = visual::Visualization::positionModel(modelId, 0.25f, 0.25f, -0.75f);
				bool b = visual::Visualization::scaleModel(modelId, 0.001f, 0.001f, 0.001f);
				//b = visual::Visualization::rotateModel(modelId, 30.0f, -1.0f, 1.0f, -1.0f);
				return b;
			}

			if (double(clock() - begin) / CLOCKS_PER_SEC > 10) {
				break;
			}
		}
	}

	return false;
}

int main(int argc, char* argv[]) {	
	cout << "VisualizationTest" << endl;

	test();
	//testPoint::testPoint();
	//testModel::testModel();
	shuttle();

    return 0;
}