using System;
namespace CGLab3.Primitives
{
    public class Triangle : Shape
    {
        public Triangle(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c) : this(a, b, c, false)
        {

        }
        public Triangle(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c, bool autoNormalDisabled)
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

