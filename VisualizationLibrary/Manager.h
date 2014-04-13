#ifndef MANAGER_H
#define MANAGER_H

#include "Square.h"
#include "AssimpModel.h"
#include "Text.h"
#include "Button.h"
#include "GraphicEngine.h"
#include <map>
#include <mutex>

namespace visual {
	/** Managet die ganze Bibliothek.
	* Es handelt sich hierbei um einen Singleton.
	*/
	class Manager {
	public:
		/** Factory Method des Singleton
		* @author Stefan Landgrebe
		* @return Instanz der Manager Klasse
		*/
		static Manager* getInstance(void);

		/** Prüft den Zustand der Bibliothek und ob das Fenster offen ist
		* @author Stefan Landgrebe
		* @return True wenn das Fenster offen ist, ansonsten False.
		*/
		bool isRunning(void);

		/** Fügt Modell zur Liste aller Models hinzu. Diese Methode wird von GraphicEngine nach der Erstellung des Objektes aufgerufen.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param model AssimpModel Objekt
		* @see model::AssimpModel
		* @see graphics::GraphicEngine
		* @see addModel()
		* @see isModelCreated()
		*/
		void addToModelList(GLuint modelId, model::AssimpModel* model);

		/** Fügt Modell zur Liste aller Squares hinzu. Diese Methode wird von GraphicEngine nach der Erstellung des Objektes aufgerufen.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param model Square Objekt
		* @see model::Square
		* @see graphics::GraphicEngine
		* @see addPoint()
		* @see isModelCreated()
		*/
		void addToSquareList(GLuint modelId, model::Square* model);

		/** Fügt Modell zur Liste aller Texte hinzu. Diese Methode wird von GraphicEngine nach der Erstellung des Objektes aufgerufen.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param text Text Objekt
		* @see gui::Text
		* @see graphics::GraphicEngine
		* @see addText()
		* @see isModelCreated()
		*/
		void addToTextList(GLuint textId, gui::Text* text);

		/** Fügt Modell zur Liste aller Buttons hinzu. Diese Methode wird von GraphicEngine nach der Erstellung des Objektes aufgerufen.
		* @author Stefan Landgrebe
		* @param buttonId ID des Modells
		* @param button Button Objekt
		* @see gui::Button
		* @see graphics::GraphicEngine
		* @see addModel()
		* @see isModelCreated()
		*/
		void addToButtonList(GLuint buttonId, gui::Button* button);


		/** Test Methode, welche nicht für den produktiven Einsatz gedacht ist.
		* @author Stefan Landgrebe
		* @param s Ein String
		*/
		void doSomething(std::string s);

		/** Fügt ein 3D Modell hinzu
		* @author Stefan Landgrebe
		* @param filename Dateipfad des 3D Modells
		* @return ID des Modells. Diese wird für die weitere Arbeit mit diesem Modell benötigt.
		*/
		GLuint addModel(const ::std::string filename);

		/** Fügt eine Fläche welche von einem Bild ausgefüllt wird hinzu.
		* @author Stefan Landgrebe
		* @param textureFilename Dateipfad des Bildes
		* @return ID des Modells
		*/
		GLuint addPoint(const ::std::string textureFilename);

		/** Fügt ein Textelement hinzu. Hierbei handelt sich um ein GUI Element.
		* @author Stefan Landgrebe
		* @param filename Dateipfad der Schriftart. Es werden Truetype und Opentype Schriften unterstützt.
		* @return ID des Text Objektes
		* @see setText()
		* @see setTextSize()
		* @see setTextColor()
		*/
		GLuint addText(const std::string text);

		/** Fügt ein Button Objekt hinzu.
		* @author Stefan Landgrebe
		* @param filename Dateipfad zur Schriftart.
		* @return Liefert die modelId zurück.
		*/
		GLuint addButton(const std::string fontname);


		/** Prüft ob ein Modell existiert. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button).
		Bevor ein Modell bearbeitet wird, sollte sichergestellt werden, dass das Modell existiert. Dies hat den Hintergrund,
		dass die Modelle effektiv in einem separaten Thread erstellt werden.
		* @author Stefan Landgrebe
		* @param modelId ID des zu prüfenden Modells
		* @return Resultat der Prüfung
		*/
		GLboolean isModelCreated(GLuint modelId);


		/** Markiert ein Modell zur Entfernung. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button).
		* @author Stefan Landgrebe
		* @param modelId ID des zu entfernenden Modells
		*/
		void dispose(GLuint modelId);

		/** Entfernt die zur Entfernung markierten Modelle. Diese Methode wird von der GraphicEngine aus angestossen.
		* @author Stefan Landgrebe
		* @param modelId ID des zu entfernenden Modells
		* @see dispose()
		* @see graphics::GraphicEngine
		*/
		void remove(GLuint modelId);

		/** Positioniert ein Modell. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt.
		Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert.
		Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird.
		X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param position Vektor der Position
		* @return Prüfung ob die Operation durchgeführt werden konnte
		*/
		GLboolean positionModel(GLuint modelId, glm::vec3 position);
		
		/** Positioniert ein Modell. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt.
		Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert.
		Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird.
		X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param position Vektor der Position
		* @return Prüfung ob die Operation durchgeführt werden konnte
		*/
		GLboolean rotateModel(GLuint modelId, GLfloat degrees, glm::vec3 axis);

		/** skaliert das Modell auf die angegebene Grösse
		Diese Methode kann nicht für Text Modelle verwendet werden. Für Texte kann die @link setTextSize @endlink Methode verwendet werden.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param scale Skalierungsvektor
		* @return Prüfung ob die Operation durchgeführt werden konnte
		* @see setTextSize()
		*/
		GLboolean scaleModel(GLuint modelId, glm::vec3 scale);

		/** Setzt die Hervorhebungsfarbe für dieses Objekt. Diese Methode kann nicht für Text Objekte verwendet werden.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param color Farbe (r,g,b,a)
		* @return Prüfung ob die Operation durchgeführt werden konnte
		* @see setTextColor()
		*/
		bool setModelHighlightColor(GLuint modelId, glm::vec4 color);

		/** Ändert den Status der Hervorhebung dieses Objektes. Zur Hervorhebung wird die Hervorhebungsfarbe verwendet.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param choice True aktiviert und False deaktiviert die Hervorhebung.
		* @return Prüfung ob die Operation durchgeführt werden konnte
		* @see setModelHighlightColor()
		*/
		bool isModelHighlighted(GLuint modelId, bool choice);

		/** Hängt ein Modell an die Kamera an. Dies hat zur Folge, dass dieses Objekt relativ zur Kamera positioniert wird.
		Diese Methode kann nur für Modelle und Punkte verwendet werden.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param choice True aktiviert und False deaktiviert die Anhängung an die Kamera.
		* @return Prüfung ob die Operation durchgeführt werden konnte
		* @see positionModel()
		* @see addModel()
		* @see addPoint()
		*/
		bool attachModelToCamera(GLuint modelId, bool choice);


		/** Ändert den darzustellenden Text
		Diese Methode kann für Text und Button Objekte verwendet werden.
		* @author Stefan Landgrebe
		* @param textId ID des Modells
		* @param text Der neu darzustellende Text
		* @return Prüfung ob die Operation durchgeführt werden konnte
		*/
		bool setText(const GLuint textId, const std::string filename);

		/** Ändert die Grösse des Textes. Die Grössenangabe erfolgt in Punkten (@link http://de.wikipedia.org/wiki/Schriftgrad @endlink).
		Diese Methode kann für Text und Button Objekte verwendet werden.
		* @author Stefan Landgrebe
		* @param modelId ID des Modells
		* @param points Die neue Grösse.
		* @return Prüfung ob die Operation durchgeführt werden konnte
		*/
		bool setTextSize(const GLuint textId, const int points);

		/** Ändert die Farbe des Textes.
		Diese Methode kann für Text und Button Objekte verwendet werden.
		* @author Stefan Landgrebe
		* @param textId ID des Modells
		* @param color Farbe (r,g,b,a)
		* @return Prüfung ob die Operation durchgeführt werden konnte
		*/
		bool setTextColor(const GLuint textId, const glm::vec4 color);

		/** Positioniert die Kamera
		* @author Stefan Landgrebe
		* @param position Positionsvektor
		*/
		void positionCamera(glm::vec3 position);

		/** Rotiert die Kamera um die Y-Achse.
		* @author Stefan Landgrebe
		* @param degrees Rotationswinkel
		*/
		void rotateCamera(float degrees);

		/** Rotiert die Kamera um die x-Achse
		* @author Stefan Landgrebe
		* @param degrees Rotationswinkel
		*/
		void tiltCamera(float degrees);

		/** Ändert die Geschwindikeit
		* @author Stefan Landgrebe
		* @param speed Geschwindigkeit in m/s
		*/
		void changeCameraSpeed(float speed);

		/** Führt die Kollisions Erkennung von Modellen durch
		* @author Stefan Landgrebe
		*/
		void doCollisionDetection(void);

		/** Länge des Textes mit allen erkannten Kollisionen
		* @author Stefan Landgrebe
		* @return Länge des Textes
		* @see collisionsText()
		*/
		unsigned int collisionsTextLength(void);

		/** Text mit allen Kollisionen.
		Format: 1:2,3;2:1;3:1;4:
		Bedeutung: Objekt mit der ID 1 kollidiert mit Objekt 2 und 3
		Objekt 2 kollidiert mit Objekt 1
		Objekt 3 kollidiert mit Objekt 1
		Objekt 4 kollidiert mit nichts.
		* @author Stefan Landgrebe
		* @return Text
		* @see collisionsTextLength()
		*/
		std::string collisionsText(void);

		/** Sorgt dafür dass alle Objekte neu gezeichnet werden. Diese Methode wird von GraphicEngine aufgerufen
		* @author Stefan Landgrebe
		* @see graphics::GraphicEngine
		*/
		void draw(void);

	private:
		static Manager* instance;

		std::mutex m_mutex;

		GLuint modelInstantiationCounter = 0;
		std::map<GLuint, model::AssimpModel*> assimpModelList;
		std::map<GLuint, model::Square*> squareList;
		std::map<GLuint, gui::Text*> textList;
		std::map<GLuint, gui::Button*> buttonList;

		std::string m_collisions;
		std::string m_collisionsCache;

		Manager(void);

		gui::Text* getTextFromList(GLuint textId);
		gui::Button* getButtonFromList(GLuint buttonId);
	};
}

#endif