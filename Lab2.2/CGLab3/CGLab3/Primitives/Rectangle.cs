using System;
using System.Drawing;
using OpenTK.Core;
using OpenTK.Graphics.OpenGL4;

namespace CGLab3.Primitives
{
    public class Rectangle : Shape
    {
        public Point3D[] ActualPoints { get; private set; } = new Point3D[4]; 

        public Rectangle(Point3D a, Point3D b, Point3D c, Point3D d) : this(a, b, c, d, false)
        {

        }

        public Rectangle(Point3D a, Point3D b, Point3D c, Point3D d, bool autoNormalDisabled)
        {
            Build(a, b, c, d);
            if (!autoNormalDisabled)
            {
                GenNormal(a, b, c);
            }
        }

        public Rectangle(Point3D a, Point3D b, Point3D c, Point3D d, Point3D normal) : this(a, b, c, d, true)
        {
            SetNormal(normal);
        }

        public Rectangle(Point3D a, Point3D b, Point3D c, Point3D d, FloatPoint3D normal) : this(a, b, c, d, true)
        {
            SetNormal(normal);
        }

        public Rectangle(Point3D center, int height, int width, bool autoNormalDisabled)
        {
            // Todo: fix only X 
            Point3D a = new Point3D(center.X - width / 2, center.Y, center.Z - height / 2);
            Point3D b = new Point3D(center.X + width / 2, center.Y, center.Z - height / 2);
            Point3D c = new Point3D(center.X + width / 2, center.Y, center.Z + height / 2);
            Point3D d = new Point3D(center.X - width / 2, center.Y, center.Z + height / 2);
            Build(a, b, c, d);
            if (!autoNormalDisabled)
            {
                GenNormal(a, b, c);
            }
        }

        public Rectangle(Point3D center, int height, int width) : this(center, height, width, false)
        {
            
        }

        new public Rectangle ColorFill(Color color)
        {
            base.ColorFill(color);
            return this;
        }

        private void Build(Point3D a, Point3D b, Point3D c, Point3D d)
        {
            ActualPoints[0] = a;
            ActualPoints[1] = b;
            ActualPoints[2] = c;
            ActualPoints[3] = d;
            AddVertex(a);
            AddVertex(b);
            AddVertex(d);
            AddVertex(b);
            AddVertex(c);
            AddVertex(d);
        }

    }
}

