using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyFX
{
    public sealed class FX
    {
        private Control Control;

        private List<Pixel> Pixels = new List<Pixel>();

        public int Scale { get; set; }

        public FX(Control control)
        {
            Control = control;
            Control.Paint += PaintHandler;
        }

        private void PaintHandler(object sender, PaintEventArgs e)
        {
            foreach (var item in Pixels)
            {
                e.Graphics.FillRectangle(item.Brush, item.ToRectangle());
            }
        }

        public void ShowGrid(int w, int h) 
        {
            for (int i = 0; i < h/Scale; i++)
            {
                
            }
        }

        public void DrawPixel(Pixel pixel)
        {
            pixel.Scale = Scale;
            pixel.X = pixel.X * Scale;
            pixel.Y = pixel.Y * Scale;
            Pixels.Add(pixel);
        }

        public void DrawPixel(int x, int y)
        {
            DrawPixel(new Pixel(x, y));
        }

        public void DrawPixel(Point point)
        {
            DrawPixel(new Pixel(point));
        }

        public void DrawPixelAndUpdate(int x, int y)
        {
            DrawPixel(x,y);
            Control.Refresh();
        }

        public void DrawPixelAndUpdate(Point point)
        {
            DrawPixel(point);
            Control.Refresh();
        }

        public void DrawPixelAndUpdate(Pixel pixel)
        {
            DrawPixel(pixel);
            Control.Refresh();
        }

        public void Refresh()
        {
            Control.Refresh();
        }

        public void DrawLineACDA(Point from, Point to)
        {
            Point pointA = from;
            Point pointB = to;
            var dx = pointB.X - pointA.X;
            var dy = pointB.Y - pointA.Y;
            if (dx < 0 || dy < 0)
            {
                Point temp = pointA;
                pointA = pointB;
                pointB = temp;
                dx = pointB.X - pointA.X;
                dy = pointB.Y - pointA.Y;
            }
            float d;
            DrawPixel(new Pixel(pointA));
            var at = new PointF((float)pointA.X, (float)pointA.Y);
            if (dx == 0)
            {
                d = 0;
                while (FX.RoundToIntPoint(at) != pointB)
                {
                    at.Y = at.Y + 1;
                    at.X = (at.X + d);
                    DrawPixelAndUpdate(new Pixel(FX.RoundToIntPoint(at)));
                }
                //y
            }
            else if (dy == 0)
            {
                d = 0;
                while (FX.RoundToIntPoint(at) != pointB)
                {
                    at.Y = at.Y + d;
                    at.X = at.X + 1;
                    DrawPixelAndUpdate(new Pixel(FX.RoundToIntPoint(at)));
                }
            }
            else if (dx >= dy)
            {
                d = (float)dy / (float)dx;
                while (FX.RoundToIntPoint(at) != pointB)
                {
                    at.Y = at.Y + d;
                    at.X = at.X + 1;
                    DrawPixelAndUpdate(new Pixel(FX.RoundToIntPoint(at)));
                }
            }
            else
            {
                d = (float)dx / (float)dy;
                while (FX.RoundToIntPoint(at) != pointB)
                {
                    at.Y = at.Y + 1;
                    at.X = at.X + d;
                    DrawPixelAndUpdate(new Pixel(FX.RoundToIntPoint(at)));
                }
            }
        }

        public void DrawLine(Point from, Point to)
        {
            Point pointA = from;
            Point pointB = to;
            var dx = pointB.X - pointA.X;
            var dy = pointB.Y - pointA.Y;
            if (pointA.Y > pointB.Y)
            {
                Point temp = pointA;
                pointA = pointB;
                pointB = temp;
                dx = pointB.X - pointA.X;
                dy = pointB.Y - pointA.Y;
            }
            int deltaX = -1;
            if (dx >= 0)
            {
                deltaX = 1;
            }
            else
            {
                deltaX = -1;
                dx = dx * -1;
            }
            int d = 0;
            int t = 0;
            int delta = 0;
            if (dy >= dx)
            {
                t = dx << 1;
                delta = dy << 1;
            }
            else
            {
                t = dy << 1;
                delta = dx << 1;
            }
            Point at = pointA;
                    DrawPixelAndUpdate(at);
            if (dy >= dx)
            {
                while (at != pointB)
                {
                    at.Y += 1;
                    d += t;
                    if (d > dy)
                    {
                        at.X += deltaX;
                        d -= delta;
                    }
                    DrawPixelAndUpdate(at);
                }
                return;
            }
            else
            {
                while (at != pointB)
                {
                    at.X += 1;
                    d += t;
                    if (d > dx)
                    {
                    at.Y += 1;
                    d -= delta;
                    }
                    DrawPixelAndUpdate(at);
                }
                return;
            }

        }

        public Pixel[] GetPixels() 
        {
            return Pixels.ToArray();
        }
        public static Point RoundToIntPoint(PointF point)
        {
            return new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y));
        }
    }
}
