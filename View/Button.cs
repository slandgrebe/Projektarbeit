﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public class Button
    {
        private uint modelId = 0;
        private float x = 0f;
        private float y = 0f;
        private float scaleX = 0.5f;
        private float scaleY = 0.25f;
        public bool IsHovered { get; private set; }
        public Button(string fontFilename)
        {
            int buttonTextSize = 40;
            float buttonTextR = 0f, buttonTextG = 0f, buttonTextB = 0f, buttonTextA = 1f;
            float buttonR = 0.667f, buttonG = 0.478f, buttonB = 0.224f, buttonA = 1f;

            modelId = View.Model.AddButton(fontFilename);

            while (modelId != 0 && !View.Model.IsCreated(modelId)) { }
            View.Model.Scale(modelId, scaleX, scaleY, 0);
            View.Text.String(modelId, "Text");
            View.Text.TextColor(modelId, buttonTextR, buttonTextG, buttonTextB, buttonTextA);
            View.Text.TextSize(modelId, buttonTextSize);
            View.Model.HighlightColor(modelId, buttonR, buttonG, buttonB, buttonA);
            View.Model.IsHighlighted(modelId, true);
            View.Model.Position(modelId, 0f, 0f, 0f);
        }

        public void Position(float x, float y)
        {
            this.x = x;
            this.y = y;
            View.Model.Position(modelId, x, y, 0f);
        }
        public void Text(string text)
        {
            View.Text.String(modelId, text);
        }

        public void Show()
        {
            View.Model.Position(modelId, x, y, 0f);
        }
        public void Hide()
        {
            View.Model.Position(modelId, -100f, 0f, 0f);
        }
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

        public void CursorUpdate(float cursorX, float cursorY)
        {
            float width = scaleX;
            float xMin = x - width / 2;
            float xMax = x + width / 2;

            float height = scaleY;
            float yMin = y - height / 2;
            float yMax = y + height / 2;

            if ((cursorX > xMin && cursorX < xMax) && (cursorY > yMin && cursorY < yMax)) 
            {
                Highlight(true);
                IsHovered = true;
            }
            else
            {
                Highlight(false);
                IsHovered = false;
            }
        }
    }
}
