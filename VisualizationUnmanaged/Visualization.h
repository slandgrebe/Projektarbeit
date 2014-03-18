// http://www.codeproject.com/Articles/28969/HowTo-Export-C-classes-from-a-DLL#CppMatureApproach

// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the VISUALIZATION_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// VISUALIZATION_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef VISUALIZATION_EXPORTS
#define VISUALIZATION_API __declspec(dllexport)
#else
#define VISUALIZATION_API __declspec(dllimport)
#endif


////////////////////////////////////////////////////////////////////////////////
// 

/** Der visual Namespace beinhaltet die gesamte Visualisierung
* @author Stefan Landgrebe
*/
namespace visual {
	/** Das Visualization Interface.
	* Diese Bibliothek erlaubt es Punkte und Linien an beliebigen Stellen im Fenster darzustellen.
	* @author Stefan Landgrebe
	*/
	class Visualization {
	public:
		/** Die addPoint Methode fügt einen neuen Punkt (repräsentiert durch ein Dreieck) hinzu.
		* @author Stefan Landgrebe
		* @return pointId
		*/
		virtual int addPoint(void) = 0;

		/** Ändert die Position eines Punktes.
		* Die updatePoint Methode wird dazu verwendet, um die Position eines Punktes zu verändern.
		* Um die Änderung sichtbar zu machen, muss die draw Methode aufgerufen werden.
		* @author Stefan Landgrebe
		* @param pointId Die ID des zu ändernden Punktes.
		* @param x Definiert die Position auf der X-Achse (Horizontale)
		* @param y Definiert die Position auf der Y-Achse (Vertikale)
		* @param z Definiert die Position auf der Z-Achse (Tiefe)
		*/
		virtual void updatePoint(int pointId, float x, float y, float z) = 0;

		/** Die addLine Methode fügt eine neue Linie hinzu.
		* @author Stefan Landgrebe
		* @return lineId
		*/
		virtual int addLine(void) = 0;

		/** Die updateLine Methode setzt die Positionen der Endpunkte einer Linie.
		* Um die Änderung sichtbar zu machen, muss die draw Methode aufgerufen werden.
		* @author Stefan Landgrebe
		* @param lineId Die ID der zu ändernden Linie.
		* @param x1 Definiert die Position des Startpunktes auf der X-Achse (Horizontale)
		* @param y1 Definiert die Position des Startpunktes auf der Y-Achse (Vertikale)
		* @param z1 Definiert die Position des Startpunktes auf der Z-Achse (Tiefe)
		* @param x2 Definiert die Position des Endpunktes auf der X-Achse (Horizontale)
		* @param y2 Definiert die Position des Endpunktes auf der Y-Achse (Vertikale)
		* @param z2 Definiert die Position des Endpunktes auf der Z-Achse (Tiefe)
		*/
		virtual void updateLine(int lineId, float x1, float y1, float z1, float x2, float y2, float z2) = 0;

		/** Die addMesh Methode fügt eine neues Mesh hinzu.
		* @author Stefan Landgrebe
		* @return meshId
		*/
		virtual int addMesh(void) = 0;

		/** Ändert die Position eines Mesh.
		* Die updateMesh Methode wird dazu verwendet, um die Position eines Meshes zu verändern.
		* Um die Änderung sichtbar zu machen, muss die draw Methode aufgerufen werden.
		* @author Stefan Landgrebe
		* @param meshId Die ID des zu ändernden Meshes.
		* @param x Definiert die Position auf der X-Achse (Horizontale)
		* @param y Definiert die Position auf der Y-Achse (Vertikale)
		* @param z Definiert die Position auf der Z-Achse (Tiefe)
		*/
		virtual void updateMesh(int meshId, float x, float y, float z) = 0;

		/** Die draw Methode sorgt dafür, dass das Fenster neu gezeichnet wird, so dass die Änderungen sichtbar werden.
		* @author Stefan Landgrebe
		*/
		virtual void draw(void) = 0;

		/** Close Methode um das Fenster zu schliessen.
		* Die Close Methode sorgt dafür, dass sämtliche Objekte gelöscht werden und schliesslich das Fenster geschlossen wird.
		* @author Stefan Landgrebe
		*/
		virtual void close(void) = 0;
	};
}


#define EXTERN_C extern "C"

/** Die addPoint Methode fügt ein neues Dreieck hinzu. Diese Methode liefert die pointId zurück.
* @author Stefan Landgrebe
* @return pointId
*/
EXTERN_C VISUALIZATION_API INT WINAPI addPoint(VOID);

/** Die updatePoint Methode setzt die Position eines Dreiecks. Es werden die pointId sowie die x, y und z Koordinate der neuen Position.
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API VOID WINAPI updatePoint(INT, FLOAT, FLOAT, FLOAT);

/** Die addLine Methode fügt eine neue Linie hinzu. Diese Methode liefert die lineId zurück.
* @author Stefan Landgrebe
* @return lineId
*/
EXTERN_C VISUALIZATION_API INT WINAPI addLine(VOID);

/** Die updateLine Methode setzt die Positionen der Endpunkte einer Linie. Es werden die lineId sowie die Koordinaten der beiden Punkte erwartet.
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API VOID WINAPI updateLine(INT, FLOAT, FLOAT, FLOAT, FLOAT, FLOAT, FLOAT);

/** Die addMesh Methode fügt eine neues Mesh hinzu.
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API INT WINAPI addMesh(VOID);

/** Die updateMesh Methode setzt die Position eines Meshes. Es werden die meshId sowie die x, y und z Koordinate der neuen Position erwartet.
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API VOID WINAPI updateMesh(INT, FLOAT, FLOAT, FLOAT);

/** Die draw Methode zeichnet das Fenster neu.
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API VOID WINAPI draw(VOID);

/** Die close Funktion schliesst das Fenster
* @author Stefan Landgrebe
*/
EXTERN_C VISUALIZATION_API VOID WINAPI close(VOID);