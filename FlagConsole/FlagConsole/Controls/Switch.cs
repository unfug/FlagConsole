using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlagConsole.Controls;
using FlagConsole.Drawing;

namespace FlagConsole.Controls
{
    public class Switch : Control, IFocusable
    {
        
        public bool _value;
        public bool Value { 
            get{return _value;}
            set { _value = value; }
        }

        public event EventHandler FocusChanged;
        protected override void Draw(GraphicBuffer buffer)
        {
            
            buffer.BackgroundDrawingColor = this.BackgroundColor;
            buffer.ForegroundDrawingColor = this.ForegroundColor;

            string background = String.Empty;
            background = background.PadRight(this.Size.Width, ' ');

            buffer.DrawLine(background, Coordinate.Origin);

            buffer.DrawLine(this.Value.ToString(), Coordinate.Origin);
        }

        public Switch()
        {
            this.BackgroundColor = ConsoleColor.White;
            this.ForegroundColor = ConsoleColor.Black;
            this.Value = false;
        }

        public void Defocus()
        {
            this.IsFocused = false;
            this.ForegroundColor = ConsoleColor.Black;
            OnFocusChanged(EventArgs.Empty);
        
        }

        public void Focus()
        {
            this.IsFocused = true;
            this.ForegroundColor = ConsoleColor.DarkRed;
            this.IsVisible = true;
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
                if (key.Key == ConsoleKey.Spacebar)
                {
                    this.Value = !Value;
                }
            }
            while (key.Key != ConsoleKey.Q && this.IsVisible);
            

            this.Defocus();
        }

       

        public char[] Text { get; set; }

        public bool IsFocused { get; private set; }

        public virtual void OnFocusChanged(EventArgs e)
        {
            if (this.FocusChanged != null)
            {
                this.FocusChanged(this, e);
            }
        }
    }
}
