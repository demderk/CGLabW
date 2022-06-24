using System;
using System.Collections.Generic;

namespace CGLab3.Primitives
{
    public class Polygon : Shape
    {
        List<Point3D> actualPoints = new List<Point3D>();

        private bool _autoNormalDisabled = false;

        public Polygon(Point3D a, Point3D b, Point3D c) : this(a, b, c, false)
        {

        }
        public Polygon(Point3D a, Point3D b, Point3D c, bool autoNormalDisabled)
        {
            _autoNormalDisabled = autoNormalDisabled;
            Build(a, b, c);
        }
        public Polygon(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c)
        {
            AddVertex()
        }
        private void Build(Point3D a, Point3D b, Point3D c)
        {
            AddVertex(a);
            AddVertex(b);
            AddVertex(c);
            if (!_autoNormalDisabled)
            {
                GenNormal(b, c, a); // Тут точно что-то не то...
            }
        }



        public void Add(Point3D newPoint)
        {
            var verts = GetVertices();
            Build(verts[verts.Length - 3].Position, verts[verts.Length - 1].Position, newPoint);
        }
    }
}

