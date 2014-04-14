#pragma once

/**
@mainpage VisualizationLibrary

@author Stefan Landgrebe

VisualizationLibrary ist ein Teil der Projektarbeit der KTSI. Die Bibliothek ermöglicht es auf einfache Art und Weise 3D-Modelle 
darzustellen sowie ein einfaches GUI zu erstellen. Diese Bibliothek baut auf OpenGL 3.3 auf.

API
===
Fenster
-------
Beschreibung | Dokumentation
------ | -------
Prüfung ob das Fenster offen ist | @link isRunning @endlink
\n

Modell Erzeugung
----------------
Beschreibung | Dokumentation
------ | -------
3D Modell |	@link addModel @endlink
Punkt (Rechteck mit Textur) | @link addPoint @endlink
Text | @link addText @endlink
Button | @link addButton @endlink
Prüfung ob Modell bereits erzeugt wurde | @link isCreated @endlink
Entfernung eines Modells | @link dispose @endlink
\n

Modell Manipulation
-------------------
Beschreibung | Dokumentation
------ | -------
Positionieren | @link position @endlink
Rotation \n (Nur Model und Point) | @link rotate @endlink
Skalierung \n (Nur Model, Point und Button) | @link scale @endlink
Hervorhebungsfarbe definieren \n (Nur Model, Point und Button) | @link highlightColor @endlink
Hervorhebung aktivieren und deaktivieren \n (Nur Model, Point und Button) | @link isHighlighted @endlink
Modell an die Kamera anhängen \n (Nur Model und Point) | @link attachToCamera @endlink
\n

Text
----
Beschreibung | Dokumentation
------ | -------
Text Element erzeugen | @link addText @endlink
anzuzeigender Text ändern | @link text @endlink
Textgrösse ändern | @link textSize @endlink
Farbe des Textes ändern | @link textColor @endlink
Entfernung des Textelements | @link dispose @endlink
\n

Kamera
------
Beschreibung | Dokumentation
------ | -------
Positionieren | @link positionCamera @endlink
Rotation um y-Achse | @link rotateCamera @endlink
Rotation um x-Achse | @link tiltCamera @endlink
Bewegungsgeschwindigkeit in m/s | @link changeCameraSpeed @endlink
\n

Kollisionserkennung
-------------------
Bei der Kollisionserkennung wird das Resultat als Text gespeichert. \n
Mit der @link collisionsTextLength @endlink Funktion kann die Länge des Textes abgefragt werden, bevor mit der @link collisionsText @endlink Funktion der Text ausgelesen wird. \n
\n
Der Text hat folgendes Format \n
__Format:__ 1:2,3;2:1;3:1;4: \n
__Bedeutung:__ 
- Objekt mit der ID 1 kollidiert mit Objekt 2 und 3 \n
- Objekt 2 kollidiert mit Objekt 1 \n
- Objekt 3 kollidiert mit Objekt 1 \n
- Objekt 4 kollidiert mit nichts. \n
\n
Beschreibung | Dokumentation
------ | -------
Länge des Textes der Kollisionserkennung | @link collisionsTextLength @endlink
Text der Kollisionserkennung | @link collisionsText @endlink
\n
\n
\n
**/

//#include "stdafx.h"

#ifdef DLL_EXPORTS
#define DLL_API __declspec(dllexport) 
#else
#define DLL_API __declspec(dllimport) 
#endif


// exportierte funktionen

/** Hier sind alle Funktionen der Bibliothek definiert.
 * Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
 * Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.
 */

/** Prüft den Zustand der Bibliothek und ob das Fenster offen ist
* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.
* @author Stefan Landgrebe
* @return (Bool) True wenn das Fenster offen ist, ansonsten False.
*/
extern "C" DLL_API int APIENTRY isRunning(void);

/** Test Funktion welche nicht für den produktiven Einsatz gedacht ist.
* @author Stefan Landgrebe
*/
extern "C" DLL_API void APIENTRY doSomething(const char* text);


/** Fügt ein 3D Modell hinzu
* @author Stefan Landgrebe
* @param filename Dateipfad des 3D Modells
* @return ID des Modells. Diese wird für die weitere Arbeit mit diesem Modell benötigt.
*/
extern "C" DLL_API unsigned int APIENTRY addModel(const char* filename);

/** Fügt eine Fläche welche von einem Bild ausgefüllt wird hinzu.
* @author Stefan Landgrebe
* @param textureFilename Dateipfad des Bildes
* @return ID des Modells
*/
extern "C" DLL_API unsigned int APIENTRY addPoint(const char* filename);

/** Fügt ein Textelement hinzu. Hierbei handelt sich um ein GUI Element.
* @author Stefan Landgrebe
* @param filename Dateipfad der Schriftart. Es werden Truetype und Opentype Schriften unterstützt.
* @return ID des Text Objektes
* @see text()
* @see textSize()
* @see textColor()
*/
extern "C" DLL_API unsigned int APIENTRY addText(const char* filename);

/** Fügt ein Button Objekt hinzu.
* @author Stefan Landgrebe
* @param filename Dateipfad zur Schriftart.
* @return Liefert die modelId zurück.
*/
extern "C" DLL_API unsigned int APIENTRY addButton(const char* fontname);


/** Prüft ob das Modell existiert. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button).
Bevor ein Modell bearbeitet wird, sollte sichergestellt werden, dass das Modell existiert. Dies hat den Hintergrund,
dass die Modelle effektiv in einem separaten Thread erstellt werden.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des zu prüfenden Modells
* @return (Bool) Liefert True wenn das Objekt existiert, ansonsten False
*/
extern "C" DLL_API int APIENTRY isCreated(const unsigned int modelId);

/** Markiert ein Modell zur Entfernung. Diese Methode kann für alle Arten von Modellen verwendet werden (Model, Point, Text, Button).
* @author Stefan Landgrebe
* @param modelId ID des zu entfernenden Modells
*/
extern "C" DLL_API void APIENTRY dispose(const unsigned int modelId);


/** Positioniert ein Modell. Kann mit allen Arten von Modellen umgehen, allerdings werden diese unterschiedlich behandelt.
Model und Point werden relativ zum Ursprung in einem normalen, dreidimensionalem, kartesischem Koordinatensystem positioniert.
Bei Text und Button handelt es sich um GUI Elemente, weswegen hier die z-Koordinate ignoriert wird.
X- und Y-Koordinaten gehen von -1 bis +1 wobei -1 dem linken bzw. unteren Rand und +1 dem rechten bzw. oberen Rand entspricht.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param x x Koordinate
* @param y y Koordinate
* @param z z Koordinate
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
*/
extern "C" DLL_API int APIENTRY position(const unsigned int modelId, const float x, const float y, const float z);

/** Rotiert ein Modell. Diese Methode kann nur für Model und Point verwendet werden.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param degrees Rotationswinkel in Grad
* @param x x Komponente der Rotationsachse
* @param y y Komponente der Rotationsachse
* @param z z Komponente der Rotationsachse
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
*/
extern "C" DLL_API int APIENTRY rotate(const unsigned int modelId, const float degrees, const float x, const float y, const float z);

/** skaliert das Modell auf die angegebene Grösse
Diese Methode kann nicht für Text Modelle verwendet werden. Für Texte kann die @link textSize @endlink Methode verwendet werden.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param x Skalierung auf der x-Achse
* @param y Skalierung auf der y-Achse
* @param z Skalierung auf der z-Achse
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
* @see textSize()
*/
extern "C" DLL_API int APIENTRY scale(const unsigned int modelId, const float x, const float y, const float z);

/** Setzt die Hervorhebungsfarbe für dieses Objekt. Diese Methode kann nicht für Text Objekte verwendet werden.
Die Farbe wird durch 4 Komponenten definiert: Rot, Grün, Blau, Alpha
Alle Komponenten Sollten einen Wert von 0 bis 1 haben, wobei der Wert 0 0% und der Wert 1 100% entspricht.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param r Rot Komponente der Farbe
* @param g Grün Komponente der Farbe
* @param b Blau Komponente der Farbe
* @param a Alpha Komponente (Undurchsichtigkeit) der Farbe
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
* @see textColor()
*/
extern "C" DLL_API int APIENTRY highlightColor(const unsigned int modelId, const float r, const float g, const float b, const float a);

/** Ändert den Status der Hervorhebung dieses Objektes. Zur Hervorhebung wird die Hervorhebungsfarbe verwendet.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param choice True aktiviert und False deaktiviert die Hervorhebung.
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
* @see highlightColor()
*/
extern "C" DLL_API int APIENTRY isHighlighted(const unsigned int modelId, const bool choice);

/** Hängt ein Modell an die Kamera an. Dies hat zur Folge, dass dieses Objekt relativ zur Kamera positioniert wird.
Diese Methode kann nur für Modelle und Punkte verwendet werden.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param choice True aktiviert und False deaktiviert die Anhängung an die Kamera.
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
* @see position()
* @see addModel()
* @see addPoint()
*/
extern "C" DLL_API int APIENTRY attachToCamera(const unsigned int modelId, const bool choice);


/** Ändert den darzustellenden Text
Diese Methode kann für Text und Button Objekte verwendet werden.
* @author Stefan Landgrebe
* @param textId ID des Modells
* @param text Der neu darzustellende Text
*/
extern "C" DLL_API void APIENTRY text(const unsigned int textId, const char* text);

/** Ändert die Grösse des Textes. Die Grössenangabe erfolgt in Punkten (@link http://de.wikipedia.org/wiki/Schriftgrad @endlink).
Diese Methode kann für Text und Button Objekte verwendet werden.

* Aus Kompatibilitätsgründen muss für die Rückgabe von Bool Werten auf int ausgewichen werden.
* Der Wert 0 entspricht dabei jeweils False und der Wert 1 entspricht True.

* @author Stefan Landgrebe
* @param modelId ID des Modells
* @param points Die neue Grösse.
* @return (Bool) Prüfung ob die Operation durchgeführt werden konnte
*/
extern "C" DLL_API int APIENTRY textSize(const unsigned int textId, const int points);

/** Ändert die Farbe des Textes.
Diese Methode kann für Text und Button Objekte verwendet werden.
Die Farbe wird durch 4 Komponenten definiert: Rot, Grün, Blau, Alpha
Alle Komponenten Sollten einen Wert von 0 bis 1 haben, wobei der Wert 0 0% und der Wert 1 100% entspricht.
* @author Stefan Landgrebe
* @param textId ID des Modells
* @param r Rot Komponente der Farbe
* @param g Grün Komponente der Farbe
* @param b Blau Komponente der Farbe
* @param a Alpha Komponente (Undurchsichtigkeit) der Farbe
*/
extern "C" DLL_API void APIENTRY textColor(const unsigned int textId, const float r, const float g, const float b, const float a);


/** Positioniert die Kamera
* @author Stefan Landgrebe
* @param position Positionsvektor
*/
extern "C" DLL_API void APIENTRY positionCamera(float x, float y, float z);

/** Rotiert die Kamera um die Y-Achse.
* @author Stefan Landgrebe
* @param degrees Rotationswinkel in Grad
*/
extern "C" DLL_API void APIENTRY rotateCamera(float degrees);

/** Rotiert die Kamera um die x-Achse
* @author Stefan Landgrebe
* @param degrees Rotationswinkel in Grad
*/
extern "C" DLL_API void APIENTRY tiltCamera(float degrees);

/** Ändert die Geschwindikeit der Kamera
* @author Stefan Landgrebe
* @param speed Geschwindigkeit in m/s (bzw. Einheiten/s)
*/
extern "C" DLL_API void APIENTRY changeCameraSpeed(float speed);


/** Länge des Textes mit allen erkannten Kollisionen
* @author Stefan Landgrebe
* @return Länge des Textes
* @see collisionsText()
*/
extern "C" DLL_API unsigned int APIENTRY collisionsTextLength(void);

/** Text mit allen Kollisionen.
Format: 1:2,3;2:1;3:1;4:
Bedeutung: Objekt mit der ID 1 kollidiert mit Objekt 2 und 3
Objekt 2 kollidiert mit Objekt 1
Objekt 3 kollidiert mit Objekt 1
Objekt 4 kollidiert mit nichts.
* @author Stefan Landgrebe
* @param string Das zu beschreibende String Objekt
* @param length Die Anzahl Zeichen welche in den String kopiert werden sollen
* @see collisionsTextLength()
*/
extern "C" DLL_API void APIENTRY collisionsText(char* string, int length);