#pragma once

#include <GL/glew.h>
#include <glm/glm.hpp>
#include <string>

#include "ColoredSquare.h"
#include "Text.h"

namespace visual {

	/** gui Namespace umfasst alle Elemente des GUIs. Alle Elemente werden orthographisch Projeziert und die Zeichnungsfläche umfasst das gesamte Fenster.
	Die Zeichnungsfläche ist 2 Einheiten breit und hoch, wobei die untere, linke Ecke der Koordinate -1/-1 und die obere, rechte Ecke der Koordinate 1/1 entspricht.
	* @author Stefan Landgrebe
	*/
	namespace gui {

		/** Button Objekt bestehend aus einer farbenen Hintergrundfläche und einem Text. Ein Button ist ein GUI Element.
		* @author Stefan Landgrebe
		*/
		class Button {
		private:
			model::ColoredSquare square;
			Text text;
			float zSquare;
			bool isVisible;

		public:

			/** Konstruktor
			Zum eigentlichen Laden des Buttons muss zusätzlich die Methode init() aufgerufen werden
			* @author Stefan Landgrebe
			* @see init()
			*/
			Button();
			/** Destruktor
			* @author Stefan Landgrebe
			*/
			~Button();

			/** Erzeugt das Square und Text Objekt
			* @author Stefan Landgrebe
			* @param fontname Dateipfad der Schriftart
			* @return Prüfung ob die Operation durchgeführt werden konnte
			*/
			bool init(const std::string fontname);
			

			/** Ändert den Text
			* @author Stefan Landgrebe
			* @param text darzustellender Text
			*/
			void setText(const std::string text);

			/** Ändert die Textgrösse
			* @author Stefan Landgrebe
			* @param points Schriftgrösse in Punkten
			* @return Prüfung ob die Operation durchgeführt werden konnte
			*/
			bool setTextSize(const int points);

			/** Ändert die Farbe des Textes
			* @author Stefan Landgrebe
			* @param color Farbe (r,g,b,a)
			*/
			void setTextColor(const glm::vec4 color);
			

			/** Ändert die Hervorhebungsfarbe der Hintergundfläche
			* @author Stefan Landgrebe
			* @param color Farbe (r,g,b,a)
			*/
			void setHighlightColor(glm::vec4 color);

			/** Ändert den Zustand der Hervorhebung
			* @author Stefan Landgrebe
			* @param choice True aktiviert und False deaktiviert die Hervorhebung
			*/
			void isHighlighted(bool choice);

			/** Skalierung der Hintergrundfläche.
			Die Zeichnungsfläche des GUIs ist 2 Einheiten breit und hoch. Standardmässig ist der Hintergrund 1 Einheit breit und hoch.
			D.h. eine Skalierung mit dem Vektor 0.5/1.5 würde dafür sorgen, dass der Hintergrund 0.5 Einheiten breit (1/4 der Fensterbreite) und 1.5 Einheiten hoch (3/4 der Fensterhöhe) ist.
			* @author Stefan Landgrebe
			* @param scale Skalierungsvektor
			*/
			void scale(glm::vec2 scale);

			/** Positioniert den Mittelpunkt des Buttons an die angegebene Positions.
			Die Zeichnungsfläche des GUIs ist 2 Einheiten breit und hoch (jeweils von -1 bis +1). Standardmässig befindet sich der Button an der Position 0/0 (dem Mittelpunkt).
			Bsp.: Positioniert man den Button an die Position 0.5/-0.5 befindet er sich unten rechts im Fenster.
			* @author Stefan Landgrebe
			* @param position Positionsvektor
			*/
			void position(glm::vec2 position);

			/** Liefert die Sichtbarkeit des Objekts zurück
			* @author Stefan Landgrebe
			* @return Sichtbarkeit
			*/
			bool visible(void);
			/** Setzt die Sichtbarkeit des Objekts
			* @author Stefan Landgrebe
			* @param choice Sichtbarkeit
			*/
			void visible(bool choice);

			/** Liefert die Position eines Modell.
			Die Zeichnungsfläche des GUIs ist 2 Einheiten breit und hoch (jeweils von -1 bis +1). Standardmässig befindet sich der Button an der Position 0/0 (dem Mittelpunkt).
			Bsp.: Positioniert man den Button an die Position 0.5/-0.5 befindet er sich unten rechts im Fenster.
			* @author Stefan Landgrebe
			* @return Position
			*/
			glm::vec3 position(void);

			/** Zeichnet den Button neu.
			* @author Stefan Landgrebe
			*/
			void draw();
		};
	}
}
