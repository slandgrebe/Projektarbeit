using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun.Gui.Elements
{
    /// <summary>
    /// Abstrahiert das Button Element der Visualization Library
    /// </summary>
    public class Button
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        private float scaleX = 0.5f;
        private float scaleY = 0.25f;
        private string text = "Text";
        private bool isVisible = false;
        /// <summary>
        /// Sichtbarkeit
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            private set
            {
                isVisible = value;
                View.Model.modelVisibility(modelId, isVisible);
            }
        }

        private Sound.Sound clickSound = null;
        private Sound.Sound hoverSound = null;
        public bool IsHovered { get; private set; }

        /// <summary>
        /// Delegate für das Entered Event
        /// </summary>
        public delegate void Entered();
        /// <summary>
        /// Entered Event
        /// </summary>
        public event Entered EnteredEvent;

        /// <summary>
        /// Delegate für das Exited Event
        /// </summary>
        public delegate void Exited();
        /// <summary>
        /// Exited Event
        /// </summary>
        public event Exited ExitedEvent;

        /// <summary>
        /// Delegate für das Click Event
        /// </summary>
        public delegate void Clicked();
        /// <summary>
        /// ClickEvent
        /// </summary>
        public event Clicked ClickEvent;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="fontFilename">zu verwendende Schriftart</param>
        public Button(string fontFilename = "data/fonts/JAPAB.TTF")
        {
            int buttonTextSize = 40;
            float buttonTextR = 0f, buttonTextG = 0f, buttonTextB = 0f, buttonTextA = 1f;
            float buttonR = 0.667f, buttonG = 0.478f, buttonB = 0.224f, buttonA = 1f;

            

            modelId = View.Model.AddButton(fontFilename);

            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Model.Scale(modelId, scaleX, scaleY, 0);
            View.Text.String(modelId, text);
            View.Text.TextColor(modelId, buttonTextR, buttonTextG, buttonTextB, buttonTextA);
            View.Text.TextSize(modelId, buttonTextSize);
            View.Model.HighlightColor(modelId, buttonR, buttonG, buttonB, buttonA);
            View.Model.IsHighlighted(modelId, true);
            View.Model.Position(modelId, 0f, 0f, 0f);

            IsVisible = false;

            // Cursor Events
            JumpAndRun.Gui.Elements.Cursor.Instance.MoveEvent += new JumpAndRun.Gui.Elements.Cursor.Move(CursorMoved);
            JumpAndRun.Gui.Elements.Cursor.Instance.ClickEvent += new JumpAndRun.Gui.Elements.Cursor.Click(CursorClicked);

            // sound
            clickSound = new Sound.Sound();
            clickSound.FilePath = "data/sound/menu/button/click.mp3";
            hoverSound = new Sound.Sound();
            hoverSound.FilePath = "data/sound/menu/button/hover.mp3";
        }
        /// <summary>
        /// Position setzen
        /// </summary>
        /// <param name="x">x Koordinate (-1 bis 1)</param>
        /// <param name="y">y Koordinate (-1 bis 1)</param>
        public void Position(float x, float y)
        {
            this.x = x;
            this.y = y;
            View.Model.Position(modelId, x, y, 0f);
        }
        /// <summary>
        /// Text ändern
        /// </summary>
        /// <param name="text">zu verwendender Text</param>
        public void Text(string text)
        {
            this.text = text;
            View.Text.String(modelId, text);
        }
        /// <summary>
        /// Anzeigen
        /// </summary>
        public void Show()
        {
            View.Model.Position(modelId, x, y, 0f);
            if (!IsVisible)
            {
                IsVisible = true;
            }
        }
        /// <summary>
        /// verstecken
        /// </summary>
        public void Hide()
        {
            View.Model.Position(modelId, -100f, 0f, 0f);
            if (IsVisible)
            {
                IsVisible = false; 
            }
        }
        /// <summary>
        /// hervorheben
        /// </summary>
        /// <param name="choice">Auswahl</param>
        public void Highlight(bool choice)
        {
            //http://paletton.com/#uid=50J0B0kllll6HHOe1sOsEdSGU6p
            if (choice)
            {
                View.Model.HighlightColor(modelId, 0.667f, 0.298f, 0.224f, 1f);
            }
            else
            {
                View.Model.HighlightColor(modelId, 0.667f, 0.478f, 0.224f, 1f);
            }
        }
        /// <summary>
        /// Event Listener wenn der Cursor sich bewegt. Wird dazu verwenden, um herauszufinden ob der Cursor sich über dem Button befindet
        /// </summary>
        /// <param name="cursorX">Cursor x Koordinate</param>
        /// <param name="cursorY">Cursor y Koordinate</param>
        public void CursorMoved(float cursorX, float cursorY)
        {
            // 0.5
            float width = scaleX;
            // 0 - 0.5 / 2 = -0.25
            float xMin = x - width / 2;
            // 0 + 0.5 / 2 = 0.25
            float xMax = x + width / 2;

            // 0.25
            float height = scaleY;
            // 0 - 0.25 / 2 = -0.125
            float yMin = y - height / 2;
            // 0 + 0.25 / 2 = 0.125
            float yMax = y + height / 2;

            if ((cursorX > xMin && cursorX < xMax) && (cursorY > yMin && cursorY < yMax))
            {
                if (!IsHovered)
                {
                    Highlight(true);
                    IsHovered = true;
                    hoverSound.Play();

                    // event auslösen
                    if (EnteredEvent != null)
                    {
                        EnteredEvent();
                    }
                }
            }
            else
            {
                if (IsHovered)
                {
                    Highlight(false);
                    IsHovered = false;

                    // event auslösen
                    if (ExitedEvent != null)
                    {
                        ExitedEvent();
                    }
                }
            }
        }
        /// <summary>
        /// Event Listener wenn der Cursor klickt
        /// </summary>
        public void CursorClicked()
        {
            if (IsHovered && IsVisible)
            {
                if (ClickEvent != null)
                {
                    ClickEvent();
                }
                clickSound.Play();
            }
        }
    }
}
