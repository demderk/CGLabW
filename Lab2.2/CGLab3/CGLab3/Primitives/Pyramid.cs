using System;
using System.Drawing;
using System.Linq;
namespace CGLab3.Primitives
{
    public class Pyramid : Shape
    {
        public Pyramid(FloatPoint3D center, int w, int h, int d, int res)
        {
            var b = new Rectangle(center, w, d);
            Merge(b);
            var verts = b.ActualPoints.Append(b.ActualPoints[0]).ToArray();
            var upPoint = center with { Y = center.Y + h/res };
            for (int i = 0; i < verts.Length - 1; i++)
            {
                Merge(new Triangle(verts[i + 1], upPoint, verts[i]));
            }

        }

        public Pyramid(Point3D center, int w, int h, int d) : this(center.ToFloatPoint3D(), w, h, d, 100)
        {

        }

        new public Pyramid ColorFill(Color color)
        {
            base.ColorFill(color);
            return this;
        }
    }
}

