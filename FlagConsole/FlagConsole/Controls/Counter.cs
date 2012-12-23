using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlagConsole.Controls;
using FlagConsole.Drawing;

namespace FlagConsole.Controls
{
    public class Counter : Control, IFocusable
    {
        public int _value;
        public int Value {
            get { return this._value; } 
            set
            {
                if (value < 0) { _value = 0; return; }
                _value = value;
            }
        }
        protected override void Draw(GraphicBuffer buffer)
        {
            
            buffer.BackgroundDrawingColor = this.BackgroundColor;
            buffer.ForegroundDrawingColor = this.ForegroundColor;

            string background = String.Empty;
            background = background.PadRight(this.Size.Width, ' ');

            buffer.DrawLine(background, Coordinate.Origin);

            buffer.DrawLine(this.Value.ToString(), Coordinate.Origin);
        }

        public Counter()
        {
            this.BackgroundColor = ConsoleColor.White;
            this.ForegroundColor = ConsoleColor.Black;
            this.Value = 1;
        }

        public void Defocus()
        {
            this.IsFocused = false;
            this.ForegroundColor = ConsoleColor.Black;
            this.BackgroundColor = ConsoleColor.White;
            this.Invalidate();
        }

        public void Focus()
        {
            this.IsFocused = true;
            this.IsVisible = true;
            this.ForegroundColor = ConsoleColor.DarkRed;
            this.BackgroundColor = ConsoleColor.Magenta;
            this.ScanInput();
        }

        private void ScanInput()

        {
            bool cursorVisible = Console.CursorVisible;
            ConsoleKeyInfo key;
           

            //Console.CursorVisible = true;

            do
            {
                this.Invalidate();
                 key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    this.Value++;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    this.Value--;
                }
            }
            while (key.Key != ConsoleKey.Q && this.IsVisible);
            //Console.CursorVisible = cursorVisible;

            this.Defocus();
        }

       

        public char[] Text { get; set; }

        public bool IsFocused { get; private set; }
    }
}
