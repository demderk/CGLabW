using System;
namespace CGLab3.Primitives
{
    public class Triangle : Shape
    {
        public Triangle(Point3D a, Point3D b, Point3D c) : this(a, b, c, false)
        {

        }
        public Triangle(Point3D a, Point3D b, Point3D c, bool autoNormalDisabled)
        {
            AddVertex(a);
            AddVertex(b);
            AddVertex(c);
            if (!autoNormalDisabled)
            {
                GenNormal(b, c, a);
            }
        }


    }
}

