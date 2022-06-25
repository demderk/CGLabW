using System;
using System.Drawing;
using OpenTK.Core;
using OpenTK.Graphics.OpenGL4;

namespace CGLab3.Primitives
{
    public class Rectangle : Shape
    {
        public FloatPoint3D[] ActualPoints { get; private set; } = new FloatPoint3D[4]; 

        public Rectangle(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c, FloatPoint3D d) : this(a, b, c, d, false)
        {

        }

        public Rectangle(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c, FloatPoint3D d, bool autoNormalDisabled)
        {
            Build(a, b, c, d);
            if (!autoNormalDisabled)
            {
                GenNormal(a, b, c);
            }
        }

        public Rectangle(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c, FloatPoint3D d, FloatPoint3D normal) : this(a, b, c, d, true)
        {
            SetNormal(normal);
        }

        public Rectangle(FloatPoint3D center, float height, float width, bool autoNormalDisabled)
        {
            // Todo: fix only X 
            FloatPoint3D a = new FloatPoint3D(center.X - width / 2, center.Y, center.Z - height / 2);
            FloatPoint3D b = new FloatPoint3D(center.X + width / 2, center.Y, center.Z - height / 2);
            FloatPoint3D c = new FloatPoint3D(center.X + width / 2, center.Y, center.Z + height / 2);
            FloatPoint3D d = new FloatPoint3D(center.X - width / 2, center.Y, center.Z + height / 2);
            Build(a, b, c, d);
            if (!autoNormalDisabled)
            {
                GenNormal(a, b, c);
            }
        }

        public Rectangle(FloatPoint3D center, int height, int width) : this(center, height/100, width/100, false) // DANGER
        {
            
        }

        new public Rectangle ColorFill(Color color)
        {
            base.ColorFill(color);
            return this;
        }

        private void Build(FloatPoint3D a, FloatPoint3D b, FloatPoint3D c, FloatPoint3D d)
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

