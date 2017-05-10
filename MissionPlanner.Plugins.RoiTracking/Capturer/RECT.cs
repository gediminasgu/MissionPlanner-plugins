namespace MissionPlanner.Plugins.RoiTracking.Capturer
{
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }
        public RECT(int Left, int Top, int Right, int Bottom)
        {
            this._Left = Left;
            this._Top = Top;
            this._Right = Right;
            this._Bottom = Bottom;
        }

        public int X
        {
            get { return this._Left; }
            set { this._Left = value; }
        }
        public int Y
        {
            get { return this._Top; }
            set { this._Top = value; }
        }
        public int Left
        {
            get { return this._Left; }
            set { this._Left = value; }
        }
        public int Top
        {
            get { return this._Top; }
            set { this._Top = value; }
        }
        public int Right
        {
            get { return this._Right; }
            set { this._Right = value; }
        }
        public int Bottom
        {
            get { return this._Bottom; }
            set { this._Bottom = value; }
        }
        public int Height
        {
            get { return this._Bottom - this._Top; }
            set { this._Bottom = value + this._Top; }
        }
        public int Width
        {
            get { return this._Right - this._Left; }
            set { this._Right = value + this._Left; }
        }
        public Point Location
        {
            get { return new Point(this.Left, this.Top); }
            set
            {
                this._Left = value.X;
                this._Top = value.Y;
            }
        }
        public Size Size
        {
            get { return new Size(this.Width, this.Height); }
            set
            {
                this._Right = value.Width + this._Left;
                this._Bottom = value.Height + this._Top;
            }
        }

        public static implicit operator Rectangle(RECT Rectangle)
        {
            return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
        }
        public static implicit operator RECT(Rectangle Rectangle)
        {
            return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }
        public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + this._Left + "; " + "Top: " + this._Top + "; Right: " + this._Right + "; Bottom: " + this._Bottom + "}";
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public bool Equals(RECT Rectangle)
        {
            return Rectangle.Left == this._Left && Rectangle.Top == this._Top && Rectangle.Right == this._Right && Rectangle.Bottom == this._Bottom;
        }

        public override bool Equals(object Object)
        {
            if (Object is RECT)
            {
                return this.Equals((RECT)Object);
            }
            else if (Object is Rectangle)
            {
                return this.Equals(new RECT((Rectangle)Object));
            }

            return false;
        }
    }
}
