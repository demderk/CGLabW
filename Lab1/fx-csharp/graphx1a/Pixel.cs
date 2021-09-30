using System.Drawing;

namespace LazyFX
{
    public class Pixel
    {
        public int Scale { get; set; } = 1;

        public Color Color { get; set; } = Color.Black;

        public int X { get; set; }

        public int Y { get; set; }

        public Brush Brush
        {
            get
            {
                return new SolidBrush(Color);
            }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(X, Y, Scale, Scale);
        }

        public Pixel(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Pixel(Point point) : this(point.X, point.Y)
        {

        }

        public Pixel(int x, int y, Color color) : this(x, y)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"X={X},Y={Y}";
        }
    }
}
