using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
using log4net;
using log4net.Config;


namespace JumpAndRun.Gui
{
    /// <summary>
    /// Darstellung des GUI nach erfolgreichem beenden des Spieles.
    /// </summary>
    public class ScoreUi
    {
        private static ScoreUi instance = null;
        /// <summary>Hintergrundbild</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>Abzug Text</summary>
        private JumpAndRun.Gui.Elements.TextWithBackground penalties = null;
        /// <summary>
        /// Bonus Text
        /// </summary>
        private JumpAndRun.Gui.Elements.TextWithBackground gains = null;
        /// <summary>
        /// Resultat Text
        /// </summary>
        private JumpAndRun.Gui.Elements.TextWithBackground score = null;
        /// <summary>Button</summary>
        private Gui.Elements.Button button = null;
        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(ScoreUi).Name);

        /// <summary>
        /// Delegate für das Button Click Event
        /// </summary>
        public delegate void ButtonClick();
        /// <summary>
        /// Button Click Event
        /// </summary>
        public event ButtonClick ButtonClickedEvent;

        private uint _Penalties = 0;
        private uint _Gains = 0;
        private int Score = 0;
        /// <summary>
        /// Anzuzeigende Abzüge
        /// </summary>
        public uint Penalties
        {
            get
            {
                return _Penalties;
            }
            set
            {
                _Penalties = value;
                RecalculatePoints();
            }
        }
        /// <summary>
        /// Anzuzeigender Bonus
        /// </summary>
        public uint Gains
        {
            get
            {
                return _Gains;
            }
            set
            {
                _Gains = value;
                RecalculatePoints();
            }
        }
        private void RecalculatePoints()
        {
            Score = (int)Gains - (int)Penalties;
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static ScoreUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreUi();
                }
                return instance;
            }
        }

        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private ScoreUi()
        {
            Score = 0;

            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.4f);
            background.Scale(1.333f, 1f);

            // Text erzeugen
            gains = new JumpAndRun.Gui.Elements.TextWithBackground();
            gains.setText("Punkte: " + Gains);
            gains.Size(50);
            gains.Position(0, 0.8f);

            penalties = new JumpAndRun.Gui.Elements.TextWithBackground();
            penalties.setText("Abzug: " + Penalties);
            penalties.Size(50);
            penalties.Position(0, 0.6f);

            score = new JumpAndRun.Gui.Elements.TextWithBackground();
            score.setText("Resultat: " + Score);
            score.Size(50);
            score.Position(0, 0.3f);

            // Button erzeugen
            button = new Gui.Elements.Button();
            button.Text("Nochmal");
            button.ClickEvent += new Gui.Elements.Button.Clicked(ButtonClicked);

            // Cursor erzeugen

            /*cursorId = View.Model.AddPoint("data/models/hand/hand-stop-2.jpg");
            while (cursorId != 0 && !View.Model.IsCreated(cursorId)) { }
            View.Model.Scale(cursorId, 0.03f, 0.05f, 1);*/

            // GUI nicht anzeigen
            Hide();
        }

        /// <summary>
        /// Button Click Event Listener
        /// </summary>
        public void ButtonClicked()
        {
            log.Debug("score click");
            //DifficultySelectedEvent(JumpAndRun.Difficulty.Easy);
            ButtonClickedEvent();
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            // Kamera zurücksetzen
            Camera.ChangeCameraSpeed(0);
            Camera.PositionCamera(0, 0, 0);

            // Texte updaten
            gains.setText("Punkte: " + Gains);
            penalties.setText("Abzug: " + Penalties);
            score.setText("Resultat: " + Score);

            // anzeigen
            background.Show();

            gains.Show();
            penalties.Show();
            score.Show();
            
            button.Show();

            JumpAndRun.Gui.Elements.Cursor.Instance.Show();
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            gains.Hide();
            penalties.Hide();
            score.Hide();
            button.Hide();

            JumpAndRun.Gui.Elements.Cursor.Instance.Hide();
        }
    }
}
