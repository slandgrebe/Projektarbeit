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
    /// Darstellung des GUI nach einem Game Over.
    /// </summary>
    public class ButtonTutorialUi
    {
        private static ButtonTutorialUi instance = null;
        /// <summary>ID des Hintergrundbildes</summary>
        private JumpAndRun.Gui.Elements.Point background = null;
        /// <summary>obere Text</summary>
        private JumpAndRun.Gui.Elements.TextWithBackground textTop2 = null;
        private JumpAndRun.Gui.Elements.TextWithBackground textTop3 = null;

        private JumpAndRun.Gui.Elements.TextWithBackground textBottom1 = null;
        private JumpAndRun.Gui.Elements.TextWithBackground textBottom2 = null;
        private JumpAndRun.Gui.Elements.TextWithBackground textBottom3 = null;

        /// <summary>ID des Buttons</summary>
        private Gui.Elements.Button button = null;
        private DateTime StartTime = DateTime.Now;
        private bool HasHovered = false;
        private bool IsVisible = false;

        /// <summary>Logger</summary>
        private static readonly ILog log = LogManager.GetLogger(typeof(ButtonTutorialUi).Name);

        /// <summary>
        /// Delegate für das Button Click Event
        /// </summary>
        public delegate void ButtonClick();
        /// <summary>
        /// Button Click Event
        /// </summary>
        public event ButtonClick ButtonClickedEvent;

        /// <summary>
        /// Singleton
        /// </summary>
        public static ButtonTutorialUi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ButtonTutorialUi();
                }
                return instance;
            }
        }
        /// <summary>
        /// Initialisiert das GUI, zeigt sie aber nicht an.
        /// </summary>
        private ButtonTutorialUi()
        {
            // Hintergrund erzeugen
            background = new JumpAndRun.Gui.Elements.Point("data/background/hintergrund.png");
            background.Position(0, 0, -0.55f);
            background.Scale(1.333f, 1f);

            // Text erzeugen
            textTop2 = new JumpAndRun.Gui.Elements.TextWithBackground();
            textTop2.setText("Beweg doch einmal deine");
            textTop2.Position(0, 0.65f);
            textTop2.Color(0.796f, 0.624f, 0.157f, 1f); // kaki
            textTop2.Size(44);

            textTop3 = new JumpAndRun.Gui.Elements.TextWithBackground();
            textTop3.setText("rechte Hand zum Knopf");
            textTop3.Position(0, 0.45f);
            textTop3.Color(0.796f, 0.624f, 0.157f, 1f); // kaki
            textTop3.Size(44);


            textBottom1 = new JumpAndRun.Gui.Elements.TextWithBackground();
            textBottom1.setText("Siehst du wie er rot geworden ist?");
            textBottom1.Position(0, -0.45f);
            textBottom1.Size(40);
            textBottom1.Color(0.784f, 0.125f, 0.125f, 1f); // rot

            textBottom2 = new JumpAndRun.Gui.Elements.TextWithBackground();
            textBottom2.setText("Wenn du gleichzeitig noch deine Hand");
            textBottom2.Position(0, -0.65f);
            textBottom2.Color(0.796f, 0.624f, 0.157f, 1f); // kaki
            textBottom2.Size(40);

            textBottom3 = new JumpAndRun.Gui.Elements.TextWithBackground();
            textBottom3.setText("nach vorne bewegst, klickst du den Knopf");
            textBottom3.Position(0, -0.85f);
            textBottom3.Color(0.796f, 0.624f, 0.157f, 1f); // kaki
            textBottom3.Size(40);


            // Button erzeugen
            button = new Gui.Elements.Button();
            button.Text("Knopf");
            button.EnteredEvent += new Gui.Elements.Button.Entered(ButtonEntered);
            button.ClickEvent += new Gui.Elements.Button.Clicked(ButtonClicked);

            // Cursor
            JumpAndRun.Gui.Elements.Cursor.Instance.MoveEvent += new JumpAndRun.Gui.Elements.Cursor.Move(CursorMove);

            // GUI nicht anzeigen
            Hide();
        }

        // zum regelmässigen update
        private void CursorMove(float x, float y)
        {
            //button.Text(truncate(x, 2) + "/" + truncate(y, 2));

            // Hover Text mindestens 2s lang anzeigen
            if (IsVisible && HasHovered && DateTime.Now.Subtract(StartTime).TotalSeconds > 2)
            {
                textBottom1.Show();
                textBottom2.Show();
                textBottom3.Show();
            }
        }

        public void ButtonEntered()
        {
            HasHovered = true;
        }
        /// <summary>
        /// Listener des Button Click Events
        /// </summary>
        public void ButtonClicked()
        {
            if (ButtonClickedEvent != null)
            {
                ButtonClickedEvent();
            }

            log.Debug("Button Tutorial clicked");
        }

        /// <summary>
        /// GUI anzeigen
        /// </summary>
        public void Show()
        {
            // Kamera zurücksetzen
            Camera.ChangeCameraSpeed(0);
            Camera.PositionCamera(0, 0, 0);

            background.Show();
            textTop2.Show();
            textTop3.Show();
            button.Show();

            JumpAndRun.Gui.Elements.Cursor.Instance.Show();

            // Starzeit zurücksetzen
            StartTime = DateTime.Now;
            HasHovered = false;

            IsVisible = true;
        }

        /// <summary>
        /// GUI nicht mehr anzeigen
        /// </summary>
        public void Hide()
        {
            background.Hide();
            textTop2.Hide();
            textTop3.Hide();
            textBottom1.Hide();
            textBottom2.Hide();
            textBottom3.Hide();
            button.Hide();

            JumpAndRun.Gui.Elements.Cursor.Instance.Hide();

            IsVisible = false;
        }

        private float truncate(float value, int digits) // nur zum testen
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }
    }
}
