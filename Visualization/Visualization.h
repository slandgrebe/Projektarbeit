#ifdef VISUALIZATIONLIBRARY_EXPORTS
#define VISUALIZATIONLIBRARY_API __declspec(dllexport) 
#else
#define VISUALIZATIONLIBRARY_API __declspec(dllimport) 
#endif

extern "C" VISUALIZATIONLIBRARY_API void APIENTRY doSomething(int n);